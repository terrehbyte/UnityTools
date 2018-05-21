using System;
using UnityEngine;

namespace UnityTools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public string condMethod;

        public ReadOnlyAttribute() { }
        public ReadOnlyAttribute(string condMethod) { this.condMethod = condMethod; }
    }
}