namespace PrasTestProject.Services
{
    public interface IImageStorage
    {
        Task<string?> SaveImageAsync(IFormFile? file, CancellationToken cancellationToken = default);
        Task DeleteAsync(string? relativePath);
    }
}
