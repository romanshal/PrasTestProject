using PrasTestProject.Interfaces.Factories;

namespace PrasTestProject.Factories
{
    public class FilePathFactory(
        IWebHostEnvironment env) : IFilePathFactory
    {
        private readonly IWebHostEnvironment _env = env;

        public string GenerateRelative(string fileName) => Path.Combine("uploads", "news", fileName);
        public string GenerateAbsolute(string relative) => Path.Combine(_env.WebRootPath, relative);
    }
}
