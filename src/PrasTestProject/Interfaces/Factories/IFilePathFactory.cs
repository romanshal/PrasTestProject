namespace PrasTestProject.Interfaces.Factories
{
    public interface IFilePathFactory
    {
        string GenerateRelative(string fileName);
        string GenerateAbsolute(string relative);
    }
}
