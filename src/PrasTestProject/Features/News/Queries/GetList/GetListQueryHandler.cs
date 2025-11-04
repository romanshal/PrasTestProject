using MediatR;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Models.Results;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Features.News.Queries.GetList
{
    public class GetListQueryHandler(
        INewsListStorage storage) : IRequestHandler<GetListQuery, Result<(IReadOnlyList<NewsViewModel> items, int total)>>
    {
        private readonly INewsListStorage _storage = storage;

        public async Task<Result<(IReadOnlyList<NewsViewModel> items, int total)>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var (items, total) = await _storage.Handle(request.Page, request.PageSize, cancellationToken);

            return (items, total);
        }
    }
}
