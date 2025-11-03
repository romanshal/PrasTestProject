using MediatR;
using PrasTestProject.Models;

namespace PrasTestProject.Features.News.Queries.GetDetails
{
    public record GetDetailsQuery(string Culture, Guid Id) : IRequest<DetailsViewModel>;
}
