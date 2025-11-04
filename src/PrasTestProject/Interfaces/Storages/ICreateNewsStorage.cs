using PrasTestProject.Data.Entities;

namespace PrasTestProject.Interfaces.Storages
{
    public interface ICreateNewsStorage : IStorage
    {
        Task<News> CreateAsync(News news, CancellationToken cancellationToken = default);
    }
}
