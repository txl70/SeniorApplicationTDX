using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Model
{
    public class ValidationResult
    {
        public string[] Rows { get; set; }
        public ValidationError[] ValidationError { get; set; }
    }
}
