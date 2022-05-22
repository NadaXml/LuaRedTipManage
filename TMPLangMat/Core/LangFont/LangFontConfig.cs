using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPLang
{
    [CreateAssetMenu(fileName = "LangFontConfig", menuName = "LangFontConfig")]
    public class LangFontConfig : ScriptableObject
    {
        public List<LangFontPreset> fontAssets = new List<LangFontPreset>();
    }
}
