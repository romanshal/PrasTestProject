using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Features.News.Queries.GetList;

namespace PrasTestProject.Controllers
{
    public class HomeController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetListQuery(1, 3), cancellationToken);

            return View(result.Value.items);
        }
    }
}
