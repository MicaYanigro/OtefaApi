using System;

namespace Otefa.Infrastructure.IoC
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectableAttribute : Attribute
    {
    }
}