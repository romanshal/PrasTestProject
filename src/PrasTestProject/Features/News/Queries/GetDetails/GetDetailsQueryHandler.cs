using MediatR;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Models.Errors;
using PrasTestProject.Models.Results;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Features.News.Queries.GetDetails
{
    public class GetDetailsQueryHandler(
        INewsDetailsStorage storage) : IRequestHandler<GetDetailsQuery, Result<NewsDetailsViewModel>>
    {
        private readonly INewsDetailsStorage _storage = storage;

        public async Task<Result<NewsDetailsViewModel>> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
        {
            var details = await _storage.Handle(request.Id, cancellationToken);
            if (details is null)
            {
                return Result.Failure<NewsDetailsViewModel>(Error.NotFound("News not found."));
            }

            return details;
        }
    }
}
