using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPLang
{
    [System.Serializable]
    public class MatMapPair
    {
        public Material source;
        public Material replace;
    }

    [CreateAssetMenu(menuName = "LangMatListPreset", fileName = "LangMatListPreset")]
    [System.Serializable]
    public class LangMatListPreset : ScriptableObject
    {
        [SerializeField]
        public List<MatMapPair> matName;
    }
}
