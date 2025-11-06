using System.ComponentModel.DataAnnotations;

namespace PrasTestProject.Models.ViewModels
{
    public class NewsEditViewModel : NewsCreateViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? ExistingImagePath { get; set; }
    }
}
