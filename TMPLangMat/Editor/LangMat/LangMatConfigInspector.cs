using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace TMPLang
{
    [CustomEditor(typeof(LangMatConfig))]
    public class LangMatConfigInspector : Editor
    {
        private LangMatConfig config;

        private const float L_Offset = 10;

        private const string Str_mats = "mats";
        private const string Str_langKey = "langKey";

        private string[] Array_Keys = {"TR", "JP", "TH"};

        private TMPLangMatToLuaConfig exporter = new TMPLangMatToLuaConfig();

        public void OnEnable()
        {
            config = target as LangMatConfig;

            TMPLangUtility.FillList<LangMatPreset>(config.mats,
                Array_Keys,
                (preset, key) =>
                {
                    return string.Compare(preset.langKey, key) == 0;
                },
                (key) =>
                {
                    LangMatPreset preset = new LangMatPreset();
                    preset.langKey = key;
                    return preset;
                });
        }

        public void SaveConfig()
        {
            string finalStr = exporter.GetLuaConfigStr(config);
            Debug.Log(finalStr);

            string testFilePath = Path.Combine(Application.dataPath, "ThirdParty/TMPLangMat/Test/testSourceLua.txt");
            StreamReader sr = new StreamReader(testFilePath);
            string testSourceCode = sr.ReadToEnd();
            sr.Close();
            string replaceStr = exporter.ReplaceForLuaFile(testSourceCode, finalStr);

            Debug.Log(replaceStr);

            StreamWriter sw = new StreamWriter(testFilePath);
            sw.Write(replaceStr);
            sw.Close();
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualTreeAsset vAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ThirdParty/TMPLangMat/Editor/LangMat/LangMatConfigInspectorUXml.uxml");
            VisualElement elem = vAsset.CloneTree();
            StyleSheet ss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ThirdParty/TMPLangMat/Editor/LangMat/LangMatConfigInspectorUSS.uss");
            elem.styleSheets.Add(ss);

            Label l = elem.Q<Label>("L1");
            l.text = "Material Preset和多语言的对应关系";

            ListView ls = elem.Q<ListView>("ListView1");
            VisualElement makeItem()
            {
                PropertyField pf = new PropertyField();
                return pf;
            }

            SerializedProperty listProp = serializedObject.FindProperty(Str_mats);

            void bindItem(VisualElement e, int i)
            {
                PropertyField pf = e as PropertyField;
                pf.BindProperty(listProp.GetArrayElementAtIndex(i));
            }

            ls.makeItem = makeItem;
            ls.bindItem = bindItem;
            ls.itemHeight = 60;
            ls.selectionType = SelectionType.Single;
            ls.itemsSource = config.mats;

            Button btnSave = elem.Q<Button>("BtnSave");
            btnSave.text = "保存材质对应关系到Lang.txt";
            btnSave.clickable.clicked += () =>
            {
                SaveConfig();
            };

            return elem;
        }
    }
}


