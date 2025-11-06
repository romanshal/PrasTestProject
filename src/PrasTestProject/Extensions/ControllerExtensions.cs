using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Models.Errors;
using PrasTestProject.Models.Results;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult MapError(this Controller controller, Result result) =>
            HandleError(result.Error.Code, result.Error.Description, controller);

        public static IActionResult MapError<T>(this Controller controller, Result<T> result) =>
            HandleError(result.Error.Code, result.Error.Description, controller);

        private static ViewResult HandleError(string errorCode, string errorDescription, Controller controller)
        {
            var vm = new ErrorViewModel
            {
                Description = errorDescription,
 
            };

            return errorCode switch
            {
                ErrorCodes.NotFound => controller.View("NotFound", vm),
                ErrorCodes.CantCreate
                or ErrorCodes.CantUpdate
                or ErrorCodes.CantDelete => controller.View("ServiceUnavailable", vm),
                _ => controller.View("Error", vm)
            };
        }
    }
}
