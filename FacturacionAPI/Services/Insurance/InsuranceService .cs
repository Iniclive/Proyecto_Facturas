using FacturacionAPI.Shared.Results;
using inercya.EntityLite;
using Proyecto_Facturas.Data;

namespace FacturacionAPI.Application.Insurances
{
    public class InsuranceService : IInsuranceService
    {
        private readonly InsuranceRepository _repository;

        public InsuranceService(FacturacionDataService dataService)
        {
            _repository = dataService.InsuranceRepository;
        }


        public async Task<Result<List<Insurance>>> GetAllAsync()
        {
            var insurances = await _repository
                .Query(InsuranceProjections.BaseTable)
                .ToListAsync();

            return Result<List<Insurance>>.Success(insurances);
        }

        public async Task<Result<List<Insurance>>> GetBySearchStringAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return Failure<List<Insurance>>("InsuranceBadRequest", "El texto de búsqueda no puede estar vacío.", ErrorType.BadRequest);

            var insurances = await _repository
                .Query(InsuranceProjections.BaseTable)
                .Where(InsuranceFields.Name, OperatorLite.Contains, searchString)
                .OrderBy()
                .ToListAsync();

            return Result<List<Insurance>>.Success(insurances);
        }


        private static Result<T> Failure<T>(string code, string message, ErrorType type)
            => Result<T>.Failure(new Error(code, message, type));
    }
}