using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Validator.Model;
using Validator.Validations;

namespace Validator.ValidatorHandler
{
    public class CsvValidatorHandler: IValidatorHandler
    {
        private readonly List<ObjectDefinition> _objectDefinitions;

        public CsvValidatorHandler(List<ObjectDefinition> obj)
        {
            _objectDefinitions = obj;
        }

        public ValidationResult ValidFile(string[] rows, bool hasHeader, bool hasTrailer)
        {
            var validRows = new List<string>();
            var result = new ValidationResult();
            var length = hasTrailer ? rows.Length - 1 : rows.Length;
            var start = hasHeader ? 1 : 0;
            var validationErrors = new List<ValidationError>();
            for (var i = start; i < rows.Length; i++)
            {
                var fields = rows[i].Split(',');

                if (fields.Length != _objectDefinitions.Count)
                {
                    result.ValidationError = new ValidationError[]
                    {
                        new ValidationError(){ ErrorMessage = $"Number of fields in row {i} is wrong", ErrorType = ErrorTypeDef.Fatal}, 
                    };
                    return result;
                }

                var isValid = true;
                for (int j = 0; j < fields.Length; j++)
                {
                    var valid = ValidField(fields[j], _objectDefinitions[j]);
                    if (valid != null)
                    {
                        validationErrors.Add(valid);
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    validRows.Add(rows[i]);
                }
            }

            result.ValidationError = validationErrors.ToArray();
            result.Rows = validRows.ToArray();
            return result;
        }

        public ValidationResult ValidFile(string path, bool hasHeader, bool hasTrailer)
        {
            var lines = File.ReadLines(path);
            return ValidFile(lines.ToArray(), hasHeader, hasTrailer);
        }

        public ValidationError ValidRow(string[] fields)
        {
            for (int i = 0; i < _objectDefinitions.Count; i++)
            {
                var val = ValidField(fields[i], _objectDefinitions[i]);
                if (val != null)
                {
                    return val;
                }
            }

            return null;
        }

        private ValidationError ValidField(string field, ObjectDefinition objectDefinition)
        {
            if (objectDefinition.FieldType == typeof(string))
                return Validation.ValidString(field, objectDefinition);
            if (objectDefinition.FieldType == typeof(int))
                return Validation.ValidInt(field, objectDefinition);
            if (objectDefinition.FieldType == typeof(double))
                return Validation.ValidDouble(field, objectDefinition);
            if (objectDefinition.FieldType == typeof(DateTime))
                return Validation.ValidDateTime(field, objectDefinition);
            if (objectDefinition.FieldType == typeof(Guid))
                return Validation.ValidGuid(field, objectDefinition);

            return new ValidationError()
            {
                ErrorMessage = $"Error in the Field Type! Type {objectDefinition.FieldType} not implemented!",
                ErrorType = ErrorTypeDef.Fatal, 
            };
        }
    }
}
