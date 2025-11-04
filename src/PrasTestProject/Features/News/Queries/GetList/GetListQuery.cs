using PrasTestProject.Models.CQRS;
using PrasTestProject.Models.Results;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Features.News.Queries.GetList
{
    public record GetListQuery(
        int Page,
        int PageSize) : IQuery<Result<(IReadOnlyList<NewsViewModel>items, int total)>>;
}
