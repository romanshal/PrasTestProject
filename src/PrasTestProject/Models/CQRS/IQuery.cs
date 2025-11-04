using MediatR;

namespace PrasTestProject.Models.CQRS
{
    public interface IQuery<TResponse> : IRequest<TResponse>;
}
