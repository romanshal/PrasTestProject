using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Features.News.Commands.Create;
using PrasTestProject.Features.News.Commands.Delete;
using PrasTestProject.Features.News.Commands.Edit;
using PrasTestProject.Features.News.Queries.GetDetails;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class AdminController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public IActionResult Create() => View("Edit", new NewsEditViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsEditViewModel viewModel, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) return View(viewModel);

            byte[]? bytes = null;
            string? fname = null;
            if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await viewModel.ImageFile.CopyToAsync(ms, cancellationToken);
                bytes = ms.ToArray();
                fname = viewModel.ImageFile.FileName;
            }

            var command = new CreateCommand 
            { 
                Title = viewModel.Title,
                Subtitle = viewModel.Subtitle,
                Body = viewModel.Text,
                Image = viewModel.ImageFile
            };

            var result = await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(NewsController.Details), "News", new { id = result.Value });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken = default)
        {
            var news = await _mediator.Send(new GetDetailsQuery(id), cancellationToken);
            if (news == null)
                return NotFound();

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsEditViewModel viewModel, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var command = new EditCommand
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Subtitle = viewModel.Subtitle,
                Body = viewModel.Text,
                Image = viewModel.ImageFile
            };

            var result = await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(NewsController.Details), "News", new { id = result.Value});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCommand(id), cancellationToken);
            return RedirectToAction(nameof(NewsController.Index), "News");
        }
    }
}
