namespace PrasTestProject.Interfaces.Storages
{
    public interface IImageStorage : IStorage
    {
        Task<string?> SaveAsync(IFormFile? file, CancellationToken cancellationToken = default);

        Task DeleteAsync(string? relativePath);
    }
}
