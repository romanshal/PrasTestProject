namespace PrasTestProject.Models.Errors
{
    public record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new(ErrorCodes.NullValue, "A null value was provided.");
        public static Error CantCreate(string message) => new(ErrorCodes.CantCreate, message);
        public static Error CantUpdate(string message) => new(ErrorCodes.CantUpdate, message);
        public static Error NotFound(string message) => new(ErrorCodes.NotFound, message);
    }
}
