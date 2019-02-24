using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SeniorApplication.Models;
using Validator.Model;

namespace SeniorApplication.Shared
{
    public static class Conversion
    {
        public static T GetValue<T>(string[] fields, List<ObjectDefinition> obj, string fieldName)
        {
            var position = GetPosition(obj, fieldName);
            var value = fields[position].ToUpper();
            if(typeof(T) == typeof(DateTime))
                return (T) Convert.ChangeType(value, typeof(T));
            else
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
        }

        public static int GetPosition(List<ObjectDefinition> obj, string fieldName)
        {
            return obj.FindIndex(O => O.Name.StartsWith(fieldName));
        }
    }
}