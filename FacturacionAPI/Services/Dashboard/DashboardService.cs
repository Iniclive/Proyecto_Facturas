using FacturacionAPI.Application.Dashboard;
using FacturacionAPI.Application.DashboardSum.Dtos;
using FacturacionAPI.Shared.Abstractions;
using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Proyecto_Facturas.Data;

public class DashboardService : IDashboardService
{
    private readonly DashboardSummaryRepository _dashboardRepository;
    private readonly FacturaRepository _facturaRepository;
    private readonly ICurrentUserService _currentUser;

    public DashboardService(FacturacionDataService dataService, ICurrentUserService currentUser)
    {
        _dashboardRepository = dataService.DashboardSummaryRepository;
        _facturaRepository = dataService.FacturaRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<DashboardPayloadDto>> GetSummaryAsync()
    {
        var userId = _currentUser.UserId;

        var summaryTask = _dashboardRepository.Query(DashboardSummaryProjections.BaseTable)
            .GetAsync(DashboardSummaryFields.IdUser, userId);

        var facturasTask = _facturaRepository.Query(FacturaProjections.Basic)
            .Where(FacturaFields.CreadoPor, userId)
            .OrderByDesc(FacturaFields.Modificado)
            .ToListAsync(0, 4);

        await Task.WhenAll(summaryTask, facturasTask);

        var summary = await summaryTask;
        if (summary is null)
            return Result<DashboardPayloadDto>.Failure(new Error(
                Code: "DashboardNotFound",
                Message: "Dashboard information not found",
                Type: ErrorType.NotFound
            ));

        if (!_currentUser.IsAdmin)
            summary.TotalUsers = null;

        return Result<DashboardPayloadDto>.Success(new DashboardPayloadDto
        {
            TotalClients = summary.TotalClients ?? 0,
            TotalFacturado = summary.TotalFacturado,
            TotalUsers = _currentUser.IsAdmin ? summary.TotalUsers : null,
            FacturasRecientes = (await facturasTask).ToArray()
        });
    }
}