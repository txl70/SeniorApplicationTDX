using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeniorApplication.Shared;

namespace SeniorApplicationTest.Shared
{
    [TestClass]
    public class DefinitionsTest
    {
        [TestMethod]
        public void DefinitionOrderTest()
        {
            var listProperties = Definitions.GetProperties<OrderExample>().ToArray();
            Assert.IsTrue(listProperties[0].Name.Contains("Name"));
            Assert.IsTrue(listProperties[0].FieldType == typeof(string));
            
            Assert.IsTrue(listProperties[1].Name.Contains("Age"));
            Assert.IsTrue(listProperties[1].FieldType == typeof(int));

            Assert.IsTrue(listProperties[2].Name.Contains("Birthdate"));
            Assert.IsTrue(listProperties[2].FieldType == typeof(DateTime));
        }
    }

    public class OrderExample
    {
        [Order]
        public string Name { get; set; }

        [Order]
        public int Age { get; set; }

        [Order]
        public DateTime Birthdate { get; set; }
    }
}
