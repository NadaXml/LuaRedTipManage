using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPLang
{
    [CreateAssetMenu(menuName = "TMPLangMatConfig", fileName = "TMPLangMatConfig")]
    [System.Serializable]
    public class LangMatConfig : ScriptableObject
    { 
        public List<LangMatPreset> mats = new List<LangMatPreset>();
    }

}


