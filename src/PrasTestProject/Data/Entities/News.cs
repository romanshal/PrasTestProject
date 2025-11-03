namespace PrasTestProject.Data.Entities
{
    public sealed class News
    {
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
        public string TitleRu { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public string SubtitleRu { get; set; } = null!;
        public string SubtitleEn { get; set; } = null!;
        public string BodyRu { get; set; } = null!;
        public string BodyEn { get; set; } = null!;

        // Относительный путь к файлу в wwwroot
        public string? ImagePath { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
