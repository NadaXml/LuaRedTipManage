using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

namespace TMPLang
{
    [CustomEditor(typeof(LangFontConfig))]
    public class LangFontConfigInspector : Editor
    {
        private LangFontConfig config;
        private ReorderableList d;

        private const float L_Offset = 10;

        private const string Str_mats = "mats";
        private const string Str_langKey = "langKey";

        private string[] Array_Keys = { "TR", "JP", "TH" };

        private TMPLangMatToLuaConfig exporter = new TMPLangMatToLuaConfig();

        public void OnEnable()
        {
            config = target as LangFontConfig;

            SerializedProperty listProp = serializedObject.FindProperty(Str_mats);
            d = new ReorderableList(serializedObject, listProp, false, true, false, false);

            d.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = d.serializedProperty.GetArrayElementAtIndex(index);
                var posRect_label = new Rect(rect)
                {
                    x = rect.x + L_Offset,
                    height = EditorGUIUtility.singleLineHeight
                };

                var langKey = element.FindPropertyRelative(Str_langKey);
                element.isExpanded = EditorGUI.Foldout(posRect_label, element.isExpanded, $"{langKey.stringValue}", true);
                if (element.isExpanded)
                {
                    var posRect_prop = new Rect(rect)
                    {
                        x = rect.x + L_Offset,
                        y = rect.y + EditorGUIUtility.singleLineHeight,
                        height = rect.height - EditorGUIUtility.singleLineHeight
                    };
                    EditorGUI.PropertyField(posRect_prop, element);
                }
            };

            d.elementHeightCallback = (int index) =>
            {
                var element = d.serializedProperty.GetArrayElementAtIndex(index);
                var h = EditorGUIUtility.singleLineHeight;
                if (element.isExpanded)
                    h += EditorGUI.GetPropertyHeight(element);
                return h;
            };

            d.drawHeaderCallback = (Rect rect) =>
            {
                GUI.Label(rect, "多语言FontAsset替换关系");
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            serializedObject.Update();
            d.DoLayoutList();
            serializedObject.ApplyModifiedProperties();

            //if (GUILayout.Button("保存配置"))
            //{
            //    string finalStr = exporter.GetLuaConfigStr(config);
            //    Debug.Log(finalStr);

            //    string testFilePath = Path.Combine(Application.dataPath, "TMPLangMat/Test/testFontSourceLua.txt");
            //    StreamReader sr = new StreamReader(testFilePath);
            //    string testSourceCode = sr.ReadToEnd();
            //    sr.Close();
            //    string replaceStr = exporter.ReplaceForLuaFile(testSourceCode, finalStr);

            //    Debug.Log(replaceStr);

            //    StreamWriter sw = new StreamWriter(testFilePath);
            //    sw.Write(replaceStr);
            //    sw.Close();

            //}
        }
    }
}

