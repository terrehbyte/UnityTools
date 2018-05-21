using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PrefabOnlyAttribute : PropertyAttribute
    {
        public bool required = false;

        public PrefabOnlyAttribute() { }
        public PrefabOnlyAttribute(bool required) { this.required = required; }
    }
}