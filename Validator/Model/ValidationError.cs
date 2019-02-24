namespace Validator.Model
{
    public class ValidationError
    {
        public string ErrorMessage { get; set; }
        public string FieldName { get; set; }
        public ErrorTypeDef ErrorType { get; set; }
    }

    public enum ErrorTypeDef
    {
        Warning,
        Error,
        Fatal
    }
}
