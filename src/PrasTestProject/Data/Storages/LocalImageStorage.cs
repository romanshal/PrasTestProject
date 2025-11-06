using PrasTestProject.Interfaces.Factories;
using PrasTestProject.Interfaces.Storages;

namespace PrasTestProject.Data.Storages
{
    public sealed class LocalImageStorage(
        IFileNameFactory fileNameFactory,
        IFilePathFactory filePathFactory) : IImageStorage
    {
        private readonly IFileNameFactory _fileNameFactory = fileNameFactory;
        private readonly IFilePathFactory _filePathFactory = filePathFactory;

        public Task DeleteAsync(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            var path = _filePathFactory.GenerateAbsolute(relativePath);

            if (File.Exists(path))
                File.Delete(path);

            return Task.CompletedTask;
        }

        public async Task<string?> SaveAsync(IFormFile? file, CancellationToken cancellationToken = default) 
        { 
            if (file == null || file.Length == 0) 
                return null; 
            
            var fileName = _fileNameFactory.Generate(file.FileName);
            var relative = _filePathFactory.GenerateRelative(fileName);
            var absolute = _filePathFactory.GenerateAbsolute(relative); 

            Directory.CreateDirectory(Path.GetDirectoryName(absolute)!); 

            await using var fs = new FileStream(absolute, FileMode.Create); 
            await file.CopyToAsync(fs, cancellationToken); 

            return relative; 
        }
    }
}
