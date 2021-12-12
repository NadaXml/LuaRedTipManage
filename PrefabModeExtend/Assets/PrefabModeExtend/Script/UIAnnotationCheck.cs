using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace PrefabModeExtend
{
    
    public struct CheckSt
    {
        string luaUIName;
        public CheckSt(string luaUIName)
        {
            this.luaUIName = luaUIName;
        }
    }

    public class UIAnnotationCheck
    {

        public static string markFileRegex(string fieldName, string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"---@{0} (\w*) {1} *\w*", fieldName, typeName);

            return sb.ToString();
        }

        public static string fieldRegex;
        public static string FieldRegex
        {
            get
            {
                fieldRegex = markFileRegex("Field", "LuaUI");
                return fieldRegex;
            }
        }

        public Dictionary<string, CheckSt> FormatUIAnnotationString(string uiannotation)
        {
            Dictionary<string, CheckSt> checkList = new Dictionary<string, CheckSt>();
            RegexOptions options = RegexOptions.Multiline;

            MatchCollection matchs = Regex.Matches(uiannotation, UIAnnotationCheck.FieldRegex, options);
            foreach ( Match match in matchs)
            {
                Debug.Assert(match.Groups.Count != 0);
                if (match.Groups.Count > 0)
                {
                    string luaUIName = match.Groups[1].Value;
                    CheckSt st = new CheckSt(luaUIName);
                    checkList.Add(luaUIName, st);
                }
            }
            return checkList;
        }

        public List<string> getLuaUIName(string prefabName)
        {
            //TODO 从LuaUI中的引用得到所有luaUI的名称李彪
            List<string> luaUINames = new List<string>();
            luaUINames.Add("cc");
            luaUINames.Add("bb");
            return luaUINames;
        }

        public bool checkPrefabLuaUIComplete(string prefabName)
        {
            //TODO 传入真正的Lua引用
            List<string> luaUINames = getLuaUIName(prefabName);

            string uiannotationStr = UIAnnotationReader.GetUIAnnotation(prefabName);

            Dictionary<string, CheckSt> checkStList = FormatUIAnnotationString(uiannotationStr);
            bool isSuit = true;
            foreach ( var luaUIName in luaUINames)
            {
                if (!checkStList.ContainsKey(luaUIName))
                {
                    isSuit = false;
                    break;
                }
            }
            return isSuit;
        }
    }
}


