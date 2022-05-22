using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPLang
{

    [System.Serializable]
    public class FontMapping
    {
        public string fontAssetName;
        public TMPro.TMP_FontAsset replaceFontAsset;
    }

    [CreateAssetMenu(menuName = "LangFontListPreset", fileName = "LangFontListPreset")]
    [System.Serializable]
    public class LangFontListPreset
    {
        public List<FontMapping> matName;
    }
}

