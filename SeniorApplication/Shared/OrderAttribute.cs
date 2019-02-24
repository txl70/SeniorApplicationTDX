using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace SeniorApplication.Shared
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int _order;
        public OrderAttribute([CallerLineNumber]int order = 0)
        {
            _order = order;
        }

        public int Order { get { return _order; } }
    }
}