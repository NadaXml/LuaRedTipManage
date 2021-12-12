using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PrefabModeExtend
{
    public sealed class CheckLuaUIEditor
    {
        [MenuItem("Assets/PrefabMode/RunCheck")]
        public static void RunCheck()
        {
            UIAnnotationCheck checker = new UIAnnotationCheck();
            string prefabName = "MonopolyHook";
            bool check = checker.checkPrefabLuaUIComplete(prefabName);
            Debug.Log(" check " + check);
        }
    }
}