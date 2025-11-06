using PrasTestProject.Data.Entities;

namespace PrasTestProject.Interfaces.Storages
{
    public interface IEditNewsStorage : IStorage
    {
        Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<News> HandleAsync(News news, CancellationToken cancellationToken = default);
    }
}
