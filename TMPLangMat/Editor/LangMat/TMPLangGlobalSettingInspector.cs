using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using TMPro;
using TMPro.EditorUtilities;

namespace TMPLang
{

    [CustomEditor(typeof(TMPLangGlobalConfig))]
    public class TMPLangGlobalSettingInspector : Editor
    {
        TMPLangGlobalConfig globalCofig;

        private List<object> selectMats;
        public void OnEnable()
        {
            globalCofig = target as TMPLangGlobalConfig;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(@"Assets/ThirdParty/TMPLangMat/Editor/LangMat/TMPLangGlobalConfigUXml.uxml");
            VisualElement customElem = asset.CloneTree();
            StyleSheet ss = AssetDatabase.LoadAssetAtPath<StyleSheet>(@"Assets/ThirdParty/TMPLangMat/Editor/LangMat/TMPLangGlobalConfigUSS.uss");
            customElem.styleSheets.Add(ss);

            {
                SerializedProperty prop = serializedObject.FindProperty("originFont");
                PropertyField pf = customElem.Q<PropertyField>("OriginAsset");
                pf.BindProperty(prop);
                
            }

            {
                SerializedProperty prop = serializedObject.FindProperty("allAsset");
                PropertyField pf = customElem.Q<PropertyField>("AllAsset");
                pf.BindProperty(prop);
            }

            {
                SerializedProperty prop = serializedObject.FindProperty("Sum");
                PropertyField pf = customElem.Q<PropertyField>("SumList");
                pf.BindProperty(prop);
            }


            {
                SerializedProperty prop = serializedObject.FindProperty("Wll");
                PropertyField pf = customElem.Q<PropertyField>("WllList");
                pf.BindProperty(prop);
            }

            ListView lView;
            {
                lView = customElem.Q<ListView>("MatList");

                VisualElement makeItem()
                {
                    Label l = new Label();
                    return l;
                }

                void bindItem(VisualElement e, int i)
                {
                    Label l = e as Label;
                    l.text = globalCofig.Wll.matName[i].source.name;
                }

                lView.makeItem = makeItem;
                lView.bindItem = bindItem;
                lView.itemHeight = 36;
                lView.selectionType = SelectionType.Single;
                lView.itemsSource = globalCofig.Wll.matName;
                lView.onSelectionChanged += LView_onSelectionChanged;
            }

            {
                Button btn = customElem.Q<Button>("CreatePreset");
                btn.clickable.clicked += () => {
                    
                    List<TMP_FontAsset> ls = globalCofig.allAsset;

                    TMP_FontAsset originFontAsset = globalCofig.originFont;
                    Material originDuplcate = CreateLangMaterialPreset(originFontAsset.material);

                    foreach (TMP_FontAsset _asset in ls)
                    {
                        if (!EditorUtility.IsPersistent(asset))
                        {
                            Debug.LogErrorFormat("{0} is not asset resource ", _asset.name);
                            continue;
                        }

                        Material duplacate = CreateLangMaterialPreset(_asset.material);

                        SyncMaterilaReplace(duplacate, originDuplcate, _asset);
                        lView.Refresh();
                    }

                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                    
                };
            }

            {
                Button btn = customElem.Q<Button>("DeletePreset");
                btn.clickable.clicked += () =>
                {
                    if (selectMats == null)
                    {
                        return;
                    }
                    foreach (var ob in selectMats)
                    {
                        MatMapPair mPair = ob as MatMapPair;
                        RemoveMatsName(globalCofig.Wll.matName, mPair, false);
                        RemoveMatsName(globalCofig.Sum.matName, mPair, true);

                        EditorUtility.SetDirty(globalCofig.Wll);
                        EditorUtility.SetDirty(globalCofig.Sum);
                    }

                    lView.Refresh();
                    EditorUtility.SetDirty(globalCofig);
                    AssetDatabase.SaveAssets();

                };
            }

            return customElem;
        }

        private void LView_onSelectionChanged(List<object> obj)
        {
            selectMats = obj;
        }
     

        private void SyncMaterilaReplace(Material replaceMat, Material originMat, TMP_FontAsset fontAsset)
        {
            LangMatListPreset preset = null;
            switch ( fontAsset.name)
            {
                case "WllSukin":
                    {
                        preset = globalCofig.Sum;

                    }
                    break;
                case "WllSum":
                    {
                        preset = globalCofig.Wll;
                    }
                    break;
            }

            if (preset == null)
            {
                return;
            }
            MatMapPair p = new MatMapPair();
            p.source = originMat;
            p.replace = replaceMat;
            preset.matName.Add(p);

            EditorUtility.SetDirty(preset);
        }

        private Material CreateLangMaterialPreset(Material sourceMat)
        {
            string assetPath = AssetDatabase.GetAssetPath((Object)(object)sourceMat).Split('.')[0];
            Material duplicate = new Material(sourceMat);
            duplicate.shaderKeywords = sourceMat.shaderKeywords;
            string duplicatePath = AssetDatabase.GenerateUniqueAssetPath(assetPath + ".mat");
            AssetDatabase.CreateAsset((Object)(object)duplicate, duplicatePath);
            Material assetOutMaterial = AssetDatabase.LoadAssetAtPath<Material>(duplicatePath);
            return assetOutMaterial;
        }

        private void RemoveMatsName(List<MatMapPair> ls, MatMapPair mPair, bool isLast)
        {
            ls.RemoveAll((_mat) =>
            {
                bool ret = _mat.source.name == mPair.source.name;
                if (ret)
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_mat.replace));
                return ret;
            });

            if (isLast)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(mPair.source));
            }
        }
    }
}
