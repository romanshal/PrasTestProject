
namespace PrasTestProject.Services
{
    public sealed class LocalImageStorage(IWebHostEnvironment env) : IImageStorage
    {
        private readonly IWebHostEnvironment _env = env;

        public Task DeleteAsync(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) 
                return Task.CompletedTask;

            var path = Path.Combine(_env.WebRootPath, relativePath!.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(path)) 
                File.Delete(path);

            return Task.CompletedTask;
        }

        public async Task<string?> SaveImageAsync(IFormFile? file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                return null;

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowed.Contains(ext)) 
                throw new InvalidOperationException("Unsupported image type");

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var relative = Path.Combine("uploads", "news", fileName);
            var absolute = Path.Combine(_env.WebRootPath, relative);

            Directory.CreateDirectory(Path.GetDirectoryName(absolute)!);

            await using var fs = new FileStream(absolute, FileMode.Create);

            await file.CopyToAsync(fs, cancellationToken);

            return "/" + relative.Replace('\\', '/');
        }
    }
}
