using System.ComponentModel.DataAnnotations;

namespace PrasTestProject.Models.ViewModels
{
    public class NewsEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(300)]
        public string Subtitle { get; set; } = null!;

        [Required]
        [MinLength(10)]
        public string Text { get; set; } = null!;

        public IFormFile? ImageFile { get; set; }

        public string? ExistingImagePath { get; set; }
    }
}
