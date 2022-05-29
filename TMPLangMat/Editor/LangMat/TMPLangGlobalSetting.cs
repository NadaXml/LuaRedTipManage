using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace TMPLang
{
    [CreateAssetMenu(fileName = "TMPLangGlobalConfig", menuName = "TMPLangGlobalConfig")]
    public class TMPLangGlobalConfig : ScriptableObject
    {
        public TMP_FontAsset originFont;

        public List<TMP_FontAsset> allAsset;

        public LangMatListPreset Sum;
        public LangMatListPreset Wll;

        public static TMPLangGlobalConfig GetGlobalConfig()
        {
            return AssetDatabase.LoadAssetAtPath<TMPLangGlobalConfig>(@"Assets/ThirdParty/TMPLangMat/Config/TMPLangGlobalConfig.asset");
        }
    }

}

