using AutoMapper;
using MediatR;
using PrasTestProject.Interfaces.Storages;
using PrasTestProject.Interfaces.UnitOfWork;
using PrasTestProject.Models.Errors;
using PrasTestProject.Models.Results;

namespace PrasTestProject.Features.News.Commands.Create
{
    public class CreateCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CreateCommandHandler> logger) : IRequestHandler<CreateCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CreateCommandHandler> _logger = logger;

        public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await using var scope = await _unitOfWork.StartScope(cancellationToken);
            var newsStorage = scope.GetStorage<ICreateNewsStorage>();
            var imageStorage = scope.GetStorage<IImageStorage>();

            var imagePath = await imageStorage.SaveAsync(request.Image, cancellationToken);
            if (string.IsNullOrEmpty(imagePath))
            {
                return Result.Failure<Guid>(Error.CantCreate("Error occurred while saving news."));
            }

            try
            {
                var news = _mapper.Map<Data.Entities.News>(request);
                news.ImagePath = imagePath;

                await newsStorage.CreateAsync(news, cancellationToken);

                await scope.Commit(cancellationToken);

                return news.Id;
            }
            catch(Exception ex)
            {
                await imageStorage.DeleteAsync(imagePath);

                _logger.LogError("Error occurred while saving news: {message}", ex.Message);

                return Result.Failure<Guid>(Error.CantCreate("Error occurred while saving news."));
            }
        }
    }
}
