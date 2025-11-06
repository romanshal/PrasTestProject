using System.ComponentModel.DataAnnotations;

namespace PrasTestProject.Attributes
{
    public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
    {
        private readonly string[] _extensions = extensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"Недопустимый формат файла. Разрешены: {string.Join(", ", _extensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
