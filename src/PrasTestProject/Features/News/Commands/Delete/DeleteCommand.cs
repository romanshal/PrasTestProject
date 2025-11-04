using MediatR;
using PrasTestProject.Models.CQRS;

namespace PrasTestProject.Features.News.Commands.Delete
{
    public record DeleteCommand(Guid Id) : ICommand<Unit>;
}
