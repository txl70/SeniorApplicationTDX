using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Validator.Model;
using Validator.Validations;

namespace SeniorApplicationTest.Validator
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void ValidString_with_Position_OK()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(string),
                Name = "Name",
                Position = 5
            };

            var result = Validation.ValidString("12345", def);
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void ValidString_with_Position_Error()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(string),
                Name = "Name",
                Position = 4
            };

            var result = Validation.ValidString("12345", def);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ErrorMessage.StartsWith("The field has more than"));
            Assert.IsTrue(result.FieldName.Contains(def.Name));
        }

        [TestMethod]
        public void ValidString_without_position()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(string),
                Name = "Name"
            };

            var result = Validation.ValidString("12345",def);
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void ValidInt_Ok()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(int),
                Name = "Quantity"
            };

            var result = Validation.ValidInt("12345", def);
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void ValidInt_Error()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(int),
                Name = "Quantity"
            };

            var result = Validation.ValidInt("1234a", def);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ErrorMessage.Contains("is not integer! Value:"));
            Assert.IsTrue(result.FieldName.Contains(def.Name));
        }

        [TestMethod]
        public void ValidDouble_Ok()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(double),
                Name = "Price"
            };

            var result = Validation.ValidDouble("123.45", def);
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void ValidDouble_Error()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(double),
                Name = "Price"
            };

            var result = Validation.ValidDouble("12.34a", def);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ErrorMessage.Contains("is not double! Value:"));
            Assert.IsTrue(result.FieldName.Contains(def.Name));
        }

        [TestMethod]
        public void ValidDate_Ok()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(DateTime),
                Name = "Date"
            };

            var result = Validation.ValidDateTime("01/03/2019", def);
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void ValidDate_Error()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(DateTime),
                Name = "Date"
            };

            var result = Validation.ValidDateTime("01/13/2019", def);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ErrorMessage.Contains("is not DateTime! Value"));
            Assert.IsTrue(result.FieldName.Contains(def.Name));
        }

        [TestMethod]
        public void ValidGuid_Ok()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(Guid),
                Name = "Id"
            };

            var result = Validation.ValidGuid("5e0a83da-2bcc-4197-8fef-01a424500001", def);
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public void ValidGuid_Error()
        {
            var def = new ObjectDefinition()
            {
                FieldType = typeof(Guid),
                Name = "Id"
            };

            var result = Validation.ValidGuid("111111", def);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.ErrorMessage.Contains("is not Guid! Value"));
            Assert.IsTrue(result.FieldName.Contains(def.Name));
        }
    }
}
