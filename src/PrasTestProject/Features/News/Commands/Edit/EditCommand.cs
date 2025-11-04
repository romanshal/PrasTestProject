using PrasTestProject.Models.CQRS;
using PrasTestProject.Models.Results;

namespace PrasTestProject.Features.News.Commands.Edit
{
    public record EditCommand : ICommand<Result<Guid>>
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = null!;
        public string Subtitle { get; init; } = null!;
        public string Body { get; init; } = null!;
        public IFormFile? Image { get; init; }
    }
}
