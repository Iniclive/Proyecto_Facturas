namespace FacturacionAPI.Shared.Results
{
    public enum ErrorType
    {
        Validation,
        BadRequest,
        Unauthorized,
        Forbidden,
        NotFound,
        Conflict,
        InternalServer,
        BadGateway
    }

    public sealed record Error(
        string Code,
        string Message,
        ErrorType Type,
        IReadOnlyDictionary<string, string[]>? Details = null
    );

    // ─── Result (sin valor) ───────────────────────────────────────────────────

    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        // Constructor privado: nadie puede instanciar Result directamente
        private Result(bool isSuccess, Error? error)
        {
            // Invariante: éxito ↔ sin error
            if (isSuccess && error is not null)
                throw new InvalidOperationException("Un resultado exitoso no puede tener error.");
            if (!isSuccess && error is null)
                throw new InvalidOperationException("Un resultado fallido debe tener error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(Error error) => new(false, error);

        // Conversión implícita desde Error → Result fallido
        // Permite escribir: return someError;  en lugar de: return Result.Failure(someError);
       // public static implicit operator Result(Error error) => Failure(error);
    }

    // ─── Result<T> (con valor) ────────────────────────────────────────────────

    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public Error? Error { get; }

        private Result(bool isSuccess, T? value, Error? error)
        {
            if (isSuccess && error is not null)
                throw new InvalidOperationException("Un resultado exitoso no puede tener error.");
            if (!isSuccess && error is null)
                throw new InvalidOperationException("Un resultado fallido debe tener error.");

            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static Result<T> Failure(Error error) => new(false, default, error);

        public static implicit operator Result<T>(Result result)
        {
            return Failure(result.Error!);
        }

        // Conversión implícita desde T → Result<T> exitoso
        // Permite escribir: return product;  en lugar de: return Result<T>.Success(product);
        // public static implicit operator Result<T>(T value) => Success(value);

        // Conversión implícita desde Error → Result<T> fallido
        // Permite escribir: return someError;  en lugar de: return Result<T>.Failure(someError);
        //public static implicit operator Result<T>(Error error) => Failure(error);
    }
}