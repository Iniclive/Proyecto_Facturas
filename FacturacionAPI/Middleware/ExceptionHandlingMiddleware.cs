using System.Data;
using FacturacionAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada en {Path}", context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (404, "Recurso no encontrado"),
                BadRequestException => (400, "Solicitud inválida"),
                ForbiddenException => (403, "Acceso denegado"),
                ConcurrencyException or DBConcurrencyException => (409, "Conflicto de concurrencia"),
                InvalidStatusTransitionException => (409, "Transición de estado inválida"),
                UnauthorizedAccessException => (401, "No autorizado"),
                _ => (500, "Error interno del servidor")
            };

            // En producción, ocultamos el detalle de errores 500
            var detail = statusCode == 500
                ? "Ha ocurrido un error inesperado. Contacta al administrador."
                : exception.Message;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
