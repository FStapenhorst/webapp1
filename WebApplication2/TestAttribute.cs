
using System;
using System.Collections.Generic;

namespace WebApplication2
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestTypeAttribute : Attribute
    {
        public RequestTypeAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute : Attribute
    {
    }
}