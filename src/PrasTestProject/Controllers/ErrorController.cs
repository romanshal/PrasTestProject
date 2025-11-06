using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Controllers
{
    public class ErrorController(
        ILogger<ErrorController> logger) : Controller
    {
        private readonly ILogger<ErrorController> logger = logger;

        [Route("Error")]
        public IActionResult HandleError()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var vm = new ErrorViewModel
            {
                Description = "Произошла внутренняя ошибка. Мы уже работаем над этим.",
            };

            // Логирование через ILogger (подключи DI)
            // _logger.LogError(feature?.Error, "Ошибка на {Path}", feature?.Path);

            return View("Error", vm);
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleStatusCode(int statusCode)
        {
            var vm = new ErrorViewModel
            {
                Description = statusCode switch
                {
                    StatusCodes.Status404NotFound => "Страница не найдена",
                    StatusCodes.Status403Forbidden => "Доступ запрещён",
                    StatusCodes.Status503ServiceUnavailable => "Сервис временно недоступен",
                    _ => "Произошла ошибка"
                }
            };

            return statusCode switch
            {
                StatusCodes.Status404NotFound => View("NotFound", vm),
                StatusCodes.Status403Forbidden => View("Forbidden", vm),
                StatusCodes.Status503ServiceUnavailable => View("ServiceUnavailable", vm),
                _ => View("Error", vm)
            };
        }
    }
}
