using FacturacionAPI.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    public class BaseController: ControllerBase
    {
        protected IActionResult FromResult(Result result)
        {
            if (result.IsSuccess) return NoContent();

            var error = result.Error!;
            return error.Type switch
            {
                ErrorType.NotFound => NotFound(ToProblemDetails(error)),
                ErrorType.Validation => ValidationProblem(ToValidationProblemDetails(error)),
                ErrorType.Conflict => Conflict(ToProblemDetails(error)),
                ErrorType.Unauthorized => Unauthorized(ToProblemDetails(error)),
                ErrorType.Forbidden => Forbid(),
                ErrorType.BadRequest => BadRequest(ToProblemDetails(error)),
                _ => Problem
                (
                    detail: error.Message,
                    title: error.Code,
                    statusCode: StatusCodes.Status500InternalServerError
                )
            };
        }

        protected IActionResult FromResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return result.Value is not null ? Ok(result.Value) : NoContent();
            }

            var error = result.Error!;
            return error.Type switch
            {
                ErrorType.NotFound => NotFound(ToProblemDetails(error)),
                ErrorType.Validation => ValidationProblem(ToValidationProblemDetails(error)),
                ErrorType.Conflict => Conflict(ToProblemDetails(error)),
                ErrorType.Unauthorized => Unauthorized(ToProblemDetails(error)),
                ErrorType.Forbidden => Forbid(),
                ErrorType.BadRequest => BadRequest(ToProblemDetails(error)),
                _ => Problem
                (
                    title: error.Code,
                    detail: error.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                )
            };
        }

        private static ProblemDetails ToProblemDetails(Error error)
        {
            return new ProblemDetails()
            {
                Title = error.Code,
                Detail = error.Message,
            };
        }

        private static ValidationProblemDetails ToValidationProblemDetails(Error error)
        {
            var details = error.Details is null
                ? new Dictionary<string, string[]>
                {
            { string.Empty, new[] { error.Message } }
                }
                : new Dictionary<string, string[]>(error.Details);

            return new ValidationProblemDetails(details)
            {
                Title = error.Code,
                Detail = error.Message,
                Status = StatusCodes.Status400BadRequest,
            };
        }
    }
}
