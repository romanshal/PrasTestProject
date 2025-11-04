using MediatR;
using PrasTestProject.Models.CQRS;
using PrasTestProject.Models.Results;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Features.News.Queries.GetDetails
{
    public record GetDetailsQuery(Guid Id) : IQuery<Result<NewsDetailsViewModel>>;
}
