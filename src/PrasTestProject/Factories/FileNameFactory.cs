using PrasTestProject.Interfaces.Factories;

namespace PrasTestProject.Factories
{
    public class FileNameFactory : IFileNameFactory
    {
        public string Generate(string fullName)
        {
            var ext = Path.GetExtension(fullName).ToLowerInvariant();
            return $"{Guid.NewGuid():N}{ext}";
        }
    }
}
