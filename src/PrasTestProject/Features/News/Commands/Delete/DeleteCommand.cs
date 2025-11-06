using MediatR;
using PrasTestProject.Models.CQRS;
using PrasTestProject.Models.Results;

namespace PrasTestProject.Features.News.Commands.Delete
{
    public record DeleteCommand(Guid Id) : ICommand<Result>;
}
