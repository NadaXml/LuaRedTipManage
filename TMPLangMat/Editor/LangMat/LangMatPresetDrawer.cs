using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace TMPLang
{
    [CustomPropertyDrawer(typeof(LangMatPreset))]
    public class LangMatPresetDrawer : PropertyDrawer
    {
        public VisualElement TryLoadElemt()
        {
            VisualElement elem = new VisualElement();
            VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ThirdParty/TMPLangMat/Editor/LangMat/LangMatPresetDrawerUXml.uxml");
            asset.CloneTree(elem);
            StyleSheet ss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ThirdParty/TMPLangMat/Editor/LangMat/LangMatPresetDrawerUSS.uss");
            elem.styleSheets.Add(ss);
            return elem;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement elem = TryLoadElemt();

            SerializedProperty langKeyProp = property.FindPropertyRelative("langKey");
            Label langKey = elem.Q<Label>("LangKey");
            langKey.text = langKeyProp.stringValue;

            SerializedProperty matListProp = property.FindPropertyRelative("matList");
            ObjectField listPreset = elem.Q<ObjectField>("MatListPreset");
            listPreset.objectType = typeof(LangMatListPreset);
            listPreset.BindProperty(matListProp);

            return elem;
        }
    }

}

