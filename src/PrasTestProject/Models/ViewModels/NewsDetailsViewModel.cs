namespace PrasTestProject.Models.ViewModels
{
    public record NewsDetailsViewModel : NewsViewModel
    {
        public string Text { get; set; } = null!;
    }
}
