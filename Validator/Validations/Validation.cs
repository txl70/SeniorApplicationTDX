using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Model;

namespace Validator.Validations
{
    public static class Validation
    {
        public static ValidationError ValidString(string fieldValue, ObjectDefinition definition)
        {

            if (definition.Position > 0 && fieldValue.Length > definition.Position)
            {
                return new ValidationError()
                {
                    ErrorMessage = $"The field has more than ${definition.Position} characters!",
                    ErrorType = ErrorTypeDef.Error,
                    FieldName = definition.Name
                };
            }

            return null;
        }

        public static ValidationError ValidInt(string fieldValue, ObjectDefinition definition)
        {
            int ret;
            if (!int.TryParse(fieldValue, out ret))
            {
                return new ValidationError()
                {
                    ErrorMessage = $"The {definition.Name} is not integer! Value: {fieldValue}",
                    ErrorType = ErrorTypeDef.Error,
                    FieldName = definition.Name
                };
            }

            return null;
        }

        public static ValidationError ValidDouble(string fieldValue, ObjectDefinition definition)
        {
            double ret;
            if (!double.TryParse(fieldValue, out ret))
            {
                return new ValidationError()
                {
                    ErrorMessage = $"The {definition.Name} is not double! Value: {fieldValue}",
                    ErrorType = ErrorTypeDef.Error,
                    FieldName = definition.Name
                };
            }

            return null;
        }

        public static ValidationError ValidDateTime(string fieldValue, ObjectDefinition definition)
        {
            DateTime ret;
            if (!DateTime.TryParse(fieldValue, out ret))
            {
                return new ValidationError()
                {
                    ErrorMessage = $"The {definition.Name} is not DateTime! Value: {fieldValue}",
                    ErrorType = ErrorTypeDef.Error,
                    FieldName = definition.Name
                };
            }

            return null;
        }

        public static ValidationError ValidGuid(string fieldValue, ObjectDefinition definition)
        {
            Guid ret;
            if (!Guid.TryParse(fieldValue, out ret))
            {
                return new ValidationError()
                {
                    ErrorMessage = $"The {definition.Name} is not Guid! Value: {fieldValue}",
                    ErrorType = ErrorTypeDef.Error,
                    FieldName = definition.Name
                };
            }

            return null;
        }

    }
}
