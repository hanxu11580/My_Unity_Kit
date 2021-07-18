#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace XhO_OKit
{
    [CustomPropertyDrawer(typeof(MinMax))]
    public class MinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //记录初始标签宽度和缩进级别(用于恢复)
            float originLabelWidth = EditorGUIUtility.labelWidth;
            int originIndentLevel = EditorGUI.indentLevel;
            //开始绘制
            EditorGUI.BeginProperty(position, label, property);
            //标签 控件最前面的 FocusType.Passive不接受键盘焦点 也就是鼠标点击 键盘Tab 没反应
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.width = position.width * 0.5f;
            EditorGUIUtility.labelWidth = position.width * 0.5f; //设置标签宽度
            EditorGUI.indentLevel += 1;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("min"));
            position.x += position.width; //设置属性max起始位置
            EditorGUI.PropertyField(position, property.FindPropertyRelative("max"));
            EditorGUI.EndProperty(); //结束绘制
        
            //恢复
            EditorGUIUtility.labelWidth = originLabelWidth;
            EditorGUI.indentLevel = originIndentLevel;
        }
    }    
}


#endif