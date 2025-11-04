using PrasTestProject.Models.CQRS;
using PrasTestProject.Models.Results;

namespace PrasTestProject.Features.News.Commands.Create
{
    public record CreateCommand : ICommand<Result<Guid>>
    {
        public string Title { get; init; } = null!;
        public string Subtitle { get; init; } = null!;
        public string Body { get; init; } = null!;
        public IFormFile? Image { get; init; }
    }
}
