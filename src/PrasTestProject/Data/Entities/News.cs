namespace PrasTestProject.Data.Entities
{
    public sealed class News : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public string Body { get; set; } = null!;

        // Относительный путь к файлу в wwwroot
        public string? ImagePath { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
