using Validator.Model;

namespace Validator.ValidatorHandler
{
    public interface IValidatorHandler
    {
        ValidationResult ValidFile(string[] rows, bool hasHeader, bool hasTrailer);
        ValidationResult ValidFile(string folder, bool hasHeader, bool hasTrailer);
    }
}
