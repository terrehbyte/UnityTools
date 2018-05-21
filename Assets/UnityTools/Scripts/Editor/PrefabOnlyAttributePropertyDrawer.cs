using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

namespace UnityTools
{
    [CustomPropertyDrawer(typeof(PrefabOnlyAttribute))]
    public class PrefabOnlyAttributePropertyDrawer : PropertyDrawer
    {
        private static float messageHeight = 20.0f;
        private static float spacing = 5.0f;

        private enum PrefabOnlyMessageType
        {
            Missing,
            Mismatch,
            None
        }

        private PrefabOnlyMessageType ShouldWarn(SerializedProperty property)
        {
            var suppliedObject = property.objectReferenceValue;
            var suppliedGameObject = suppliedObject as GameObject;

            var prefabAttribute = attribute as PrefabOnlyAttribute;

            // if required but missing
            if (suppliedObject == null && prefabAttribute.required)
            {
                return PrefabOnlyMessageType.Missing;
            }
            else
            {
                // if not required but missing
                if (suppliedObject == null)
                {

                }
                // if present but not a prefab
                else if (!(PrefabUtility.GetPrefabParent(suppliedGameObject) == null && PrefabUtility.GetPrefabObject(suppliedGameObject.transform) != null))
                {
                    return PrefabOnlyMessageType.Mismatch;
                }
            }

            return PrefabOnlyMessageType.None;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + (ShouldWarn(property) != PrefabOnlyMessageType.None ? spacing + messageHeight : 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string outputText = string.Empty;
            var result = ShouldWarn(property);
            switch (result)
            {
                case PrefabOnlyMessageType.None:
                    break;
                case PrefabOnlyMessageType.Missing:
                    outputText = "A prefab is required.";
                    break;
                case PrefabOnlyMessageType.Mismatch:
                    outputText = "The provided GameObject must be a prefab.";
                    break;
                default:
                    Debug.LogError("An unknown warn-level was provided for this prefab.", property.serializedObject.targetObject);
                    break;
            }

            if (outputText != string.Empty)
            {
                position.height = messageHeight;
                EditorGUI.HelpBox(position, outputText, MessageType.Error);
                position.y += position.height + spacing;
            }

            position.height = base.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}