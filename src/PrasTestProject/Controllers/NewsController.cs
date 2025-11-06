using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Features.News.Queries.GetDetails;
using PrasTestProject.Features.News.Queries.GetList;

namespace PrasTestProject.Controllers
{
    public class NewsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = 10, 
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetListQuery(page, pageSize), cancellationToken);

            return View(result.Value.items);
        }

        [HttpGet]
        public async Task<IActionResult> Page(
            int page = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetListQuery(page, pageSize), cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }

            if (!result.Value.items.Any())
            {
                return Content(string.Empty);
            }

            return PartialView("_NewsListPartial", result.Value.items);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetDetailsQuery(id), cancellationToken);

            if (result.IsFailure)
            {
                return NotFound(result.Error.Description);
            }

            return View(result.Value);
        }
    }
}
