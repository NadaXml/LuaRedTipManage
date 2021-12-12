using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace PrefabModeExtend
{
    public class UIAnnotationReader
    {
        public static string uiannotationPath = Application.dataPath + "/PrefabModeExtend/Test/";

        public static string PrefabNamToUIAnnotationName(string prefabName)
        {
            string suffix = "UIAnnotation.txt";
            return uiannotationPath + prefabName + suffix;
        }

        public static string ReadUIAnnotation(string uiAnnotationName)
        {
            if (!File.Exists(uiAnnotationName))
            {
                Debug.Log(" uiannotation 不存在 " + uiAnnotationName);
                return "";
            }

            StreamReader sr = new StreamReader(uiAnnotationName);
            return sr.ReadToEnd();
        }

        public static string GetUIAnnotation(string prefabName)
        {
            string uiannotationName = PrefabNamToUIAnnotationName(prefabName);
            return ReadUIAnnotation(uiannotationName);
        }
    }
}
