using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validator.Model;

namespace SeniorApplicationTest.Handlers
{
    [TestClass]
    public class CsvValidatorHandlerTest
    {
        public List<ObjectDefinition> _def = new List<ObjectDefinition>()
        {
            GetObjectDefinition("name", typeof(string)),
            GetObjectDefinition("price", typeof(double)),
            GetObjectDefinition("id", typeof(Guid)),
        };

        private static ObjectDefinition GetObjectDefinition(string name, Type t)
        {
            return new ObjectDefinition()
            {
                Name = name,
                FieldType = t
            };
        }

        public void ValidationErrorTest()
        {

        }

    }
}
