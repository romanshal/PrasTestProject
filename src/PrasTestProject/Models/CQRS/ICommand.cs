using MediatR;

namespace PrasTestProject.Models.CQRS
{
    public interface ICommand<TResponse> : IRequest<TResponse>;
}
