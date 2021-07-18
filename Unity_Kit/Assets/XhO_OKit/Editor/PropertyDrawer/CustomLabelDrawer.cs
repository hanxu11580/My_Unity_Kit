using UnityEditor;
using UnityEngine;

namespace XhO_OKit
{
    [CustomPropertyDrawer(typeof(CustomLabelAttribute))]
    public class CustomLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            label.text = (attribute as CustomLabelAttribute).customLabelName;
            EditorGUI.PropertyField(position, property, label, true);

        }
    }
}