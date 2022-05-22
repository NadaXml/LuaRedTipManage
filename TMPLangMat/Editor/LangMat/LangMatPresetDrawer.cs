using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace TMPLang
{
    [CustomPropertyDrawer(typeof(LangMatPreset))]
    public class LangMatPresetDrawer : PropertyDrawer
    {
        private const float Space_Height = 2;
        private const float R_Offset = 10;


        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            //return base.CreatePropertyGUI(property);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty langKeyProp = property.FindPropertyRelative("langKey");
            SerializedProperty matListProp = property.FindPropertyRelative("matList");
            return EditorGUI.GetPropertyHeight(langKeyProp) + EditorGUI.GetPropertyHeight(matListProp) + Space_Height * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                SerializedProperty langKeyProp = property.FindPropertyRelative("langKey");
                var langKeyRect = position;
                langKeyRect.height = EditorGUI.GetPropertyHeight(langKeyProp);
                langKeyRect.width = position.width - R_Offset;
                langKeyRect.y += Space_Height;
                EditorGUI.PropertyField(langKeyRect, langKeyProp);

                SerializedProperty matListProp = property.FindPropertyRelative("matList");
                var matsRect = position;
                matsRect.height = EditorGUI.GetPropertyHeight(matListProp);
                matsRect.width = position.width - R_Offset; //右边距
                matsRect.y += Space_Height + EditorGUIUtility.singleLineHeight + Space_Height;
                EditorGUI.PropertyField(matsRect, matListProp);
            }

        }
    }

}

