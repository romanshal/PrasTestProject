using PrasTestProject.Data.Entities;

namespace PrasTestProject.Interfaces.Storages
{
    public interface IDeleteNewsStorage : IStorage
    {
        Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task HandleAsync(News news, CancellationToken cancellationToken = default);
    }
}
