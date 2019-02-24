using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeniorApplication.Shared;
using Validator.Model;

namespace SeniorApplicationTest.Shared
{
    [TestClass]
    public class ConversionTest
    {

        [TestMethod]
        public void GetPositionTest()
        {
            var account = Conversion.GetPosition(GetObjectDefinitions(), "Account");
            var name = Conversion.GetPosition(GetObjectDefinitions(), "Name");
            var createDate = Conversion.GetPosition(GetObjectDefinitions(), "CreateDate");

            Assert.IsTrue(account == 0);
            Assert.IsTrue(name == 1);
            Assert.IsTrue(createDate == 2);
        }

        [TestMethod]
        public void GetValueTest()
        {
            var fields = new List<string>()
            {
                "5e0a83da-2bcc-4197-8fef-01a424500001", "Name", "21/05/2019"
            };

            var account = Conversion.GetValue<Guid>(fields.ToArray(), GetObjectDefinitions(), "Account");
            var name = Conversion.GetValue<string>(fields.ToArray(), GetObjectDefinitions(), "Name");
            var createDate = Conversion.GetValue<DateTime>(fields.ToArray(), GetObjectDefinitions(), "CreateDate");

            Assert.IsTrue(account is Guid);
            Assert.IsTrue(name is string);
            Assert.IsTrue(createDate is DateTime);
        }

        public List<ObjectDefinition> GetObjectDefinitions()
        {
            return new List<ObjectDefinition>()
            {
                new ObjectDefinition()
                {
                    Name = "Account",
                    FieldType = typeof(Guid)
                },
                new ObjectDefinition()
                {
                    Name = "Name",
                    FieldType = typeof(string)
                },
                new ObjectDefinition()
                {
                    Name = "CreateDate",
                    FieldType = typeof(DateTime)
                },
            };
        }
    }


}
