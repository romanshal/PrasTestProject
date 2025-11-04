using MediatR;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Interfaces.UnitOfWork;
using PrasTestProject.Models.Errors;
using PrasTestProject.Models.Results;

namespace PrasTestProject.Features.News.Commands.Edit
{
    public class EditCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<EditCommandHandler> logger) : IRequestHandler<EditCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<EditCommandHandler> _logger = logger;

        public async Task<Result<Guid>> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            await using var scope = await _unitOfWork.StartScope(cancellationToken);
            var newsStorage = scope.GetStorage<IEditNewsStorage>();
            var imageStorage = scope.GetStorage<IImageStorage>();

            var news = await newsStorage.GetByIdAsync(request.Id, cancellationToken);
            if (news is null)
            {
                return Result.Failure<Guid>(Error.NotFound("News not found."));
            }

            try
            {
                return news.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while editing news: {message}", ex.Message);

                return Result.Failure<Guid>(Error.CantUpdate("Error occurred while editing news."));
            }
        }
    }
}
