namespace PrasTestProject.Models.ViewModels
{
    public record NewsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public string? ImagePath { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }
    }
}
