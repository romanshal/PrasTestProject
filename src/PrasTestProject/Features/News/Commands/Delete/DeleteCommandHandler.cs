using MediatR;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Interfaces.UnitOfWork;
using PrasTestProject.Models.Errors;
using PrasTestProject.Models.Results;

namespace PrasTestProject.Features.News.Commands.Delete
{
    public class DeleteCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteCommandHandler> logger) : IRequestHandler<DeleteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DeleteCommandHandler> _logger = logger;

        public async Task<Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await using var scope = await _unitOfWork.StartScope(cancellationToken);
            var newsStorage = scope.GetStorage<IDeleteNewsStorage>();
            var imageStorage = scope.GetStorage<IImageStorage>();

            var news = await newsStorage.GetByIdAsync(request.Id, cancellationToken);
            if (news is null)
            {
                return Result.Failure(Error.NotFound("News not found."));
            }

            try
            {
                await newsStorage.HandleAsync(news, cancellationToken);

                await imageStorage.DeleteAsync(news.ImagePath);

                await scope.Commit(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting news: {message}", ex.Message);

                return Result.Failure(Error.CantDelete("Error occurred while deleting news."));
            }
        }
    }
}
