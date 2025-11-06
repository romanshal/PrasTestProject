using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Extensions;
using PrasTestProject.Features.News.Commands.Create;
using PrasTestProject.Features.News.Commands.Delete;
using PrasTestProject.Features.News.Commands.Edit;
using PrasTestProject.Features.News.Queries.GetDetails;
using PrasTestProject.Features.News.Queries.GetList;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetListQuery(page, pageSize), cancellationToken);
            if (result.IsFailure)
            {
                return this.MapError(result);
            }

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
                return this.MapError(result);
            }

            if (!result.Value.items.Any())
            {
                return Content(string.Empty);
            }

            return PartialView("_AdminNewsListPartial", result.Value.items);
        }

        [HttpGet]
        public IActionResult Create() => View(new NewsCreateViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsCreateViewModel viewModel, CancellationToken cancellationToken = default)
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
            if (result.IsFailure)
            {
                return this.MapError(result);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetDetailsQuery(id), cancellationToken);
            if (result.IsFailure)
            {
                return this.MapError(result);
            }

            var vm = new NewsEditViewModel
            {
                Id = result.Value!.Id,
                Title = result.Value!.Title,
                Subtitle = result.Value!.Subtitle,
                Text = result.Value!.Text,
                ExistingImagePath = result.Value!.ImagePath
            };

            return View(vm);
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
            if (result.IsFailure)
            {
                return this.MapError(result);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCommand(id), cancellationToken);
            if (result.IsFailure)
            {
                return this.MapError(result);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
