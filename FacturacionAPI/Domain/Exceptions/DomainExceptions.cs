namespace FacturacionAPI.Domain.Exceptions
{
    /// <summary>Recurso no encontrado → 404</summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, object key)
            : base($"'{entity}' con id '{key}' no fue encontrado.") { }
    }

    /// <summary>Conflicto de EntityRowVersion → 409</summary>
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
            : base("El registro ha sido modificado por otro proceso. Por favor, recarga la página.") { }
    }

    /// <summary>Estado de factura incorrecto para la transición → 409</summary>
    public class InvalidStatusTransitionException : Exception
    {
        public InvalidStatusTransitionException(string message)
            : base(message) { }
    }

    /// <summary>Usuario sin permiso sobre el recurso → 403</summary>
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "No tienes permiso para acceder a este recurso.")
            : base(message) { }
    }

    /// <summary>Datos de entrada inválidos → 400</summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
