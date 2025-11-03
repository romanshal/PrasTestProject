using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PrasTestProject.Controllers
{
    public class NewsController(IMediator mediatr) : Controller
    {
        private readonly IMediator _mediatr = mediatr;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        }


        [HttpGet]
        public async Task<IActionResult> DetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var culture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var result = await _mediatr.Send(new GetDetailsQuery 
            { 
                Culture = culture,
                Id = id,
            });
        }
    }
}
