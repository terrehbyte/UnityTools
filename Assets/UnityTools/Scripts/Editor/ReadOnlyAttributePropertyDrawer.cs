using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

using System.Reflection;

namespace UnityTools
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // default to readonly
            bool isWritable = false;

            // check if a method is used to determine if writable
            var readonlyAttribute = attribute as ReadOnlyAttribute;
            if (!string.IsNullOrEmpty(readonlyAttribute.condMethod))
            {
                var condMethod = fieldInfo.DeclaringType.GetMethod(readonlyAttribute.condMethod, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                Assert.IsNotNull(condMethod, string.Format("Method name '{0}' supplied for condition did not match any methods on object!", readonlyAttribute.condMethod));
                isWritable = !(bool)condMethod.Invoke(property.serializedObject.targetObject, null);
            }

            GUI.enabled = isWritable;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}