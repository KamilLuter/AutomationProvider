using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AutomationProvider.Api.Controllers
{
    [ApiController]
    public class ApiController: ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if(errors.Count == 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }
            else if (errors.All(error => error.Type == ErrorType.NotFound))
            {
                return NotFoundProblem(errors);
            }
            else if (errors.All(error => error.Type == ErrorType.Conflict))
            {
                return ConflictProblem(errors);
            }

            HttpContext.Items["Errors"] = errors;

            var firstError = errors[0];

            return Problem(firstError);
        }

        private IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }

        private IActionResult NotFoundProblem(List<Error> errors)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource not found",
                Detail = "One or more resources were not found."
            };

            foreach (var error in errors)
            {
                problemDetails.Errors.Add(error.Code, new[] { error.Description });
            }

            return NotFound(problemDetails);
        }

        private IActionResult ConflictProblem(List<Error> errors)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict occurred",
                Detail = "One or more conflicts occurred."
            };

            foreach (var error in errors)
            {
                problemDetails.Errors.Add(error.Code, new[] { error.Description });
            }

            return Conflict(problemDetails);
        }
    }
}
