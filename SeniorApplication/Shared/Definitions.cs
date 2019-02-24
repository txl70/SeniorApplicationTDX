using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Validator.Model;

namespace SeniorApplication.Shared
{
    public static class Definitions
    {
        public static List<ObjectDefinition> GetProperties<T>() where T : class
        {

            /*
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

             Array.Sort(propertyInfos,
                delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2) { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            */

            var propertyInfos = from property in typeof(T).GetProperties()
                where Attribute.IsDefined(property, typeof(OrderAttribute))
                orderby ((OrderAttribute)property
                    .GetCustomAttributes(typeof(OrderAttribute), false)
                    .Single()).Order
                select property;

            // return property names
            return propertyInfos.Select(propertyInfo =>
                new ObjectDefinition()
                {
                    Name = propertyInfo.Name,
                    FieldType = propertyInfo.PropertyType,
                }
                ).ToList();
        }
    }
}