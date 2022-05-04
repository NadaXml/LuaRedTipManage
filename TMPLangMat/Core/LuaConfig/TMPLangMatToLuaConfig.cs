using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaConfig;
using System.Text.RegularExpressions;

namespace TMPLang
{
    public class TMPLangMatToLuaConfig
    {
        public string GetLuaConfigStr(LangMatConfig config)
        {
            StringOutput output = new StringOutput();
            output.AppendLine("Lang.matMap = {", true);
            WriteLangMat(output, config);
            output.AppendLine("}", true);
            return output.sb.ToString();
        }

        public string ReplaceForLuaFile(string luaSourceCode, string confgCode)
        {
            return Regex.Replace(luaSourceCode, @"(-- begin auto genre lang mat)([\s\S]+)(--end auto)", "${1}" + "\r\n" + confgCode + "${3}");
        }


        public void WriteLangMat(StringOutput output, LangMatConfig config)
        {
            int count = config.mats.Count;
            int i = 0;
            output.PushIndent();
            foreach (var matPreset in config.mats)
            {
                string langKey = matPreset.langKey;
                
                output.AppendFormat("[LangEnum.{0}] = {{\n", langKey, true);

                WriteLangMatsMap(output, matPreset.matList);

                output.Append("}", true);
                output.PushDot(count != 0 && i + 1 != count);
                i++;
               
            }
            output.PopIndent();
        }
        
        public void WriteLangMatsMap(StringOutput output, LangMatListPreset matList)
        {
            List<MatMapPair> matName = matList.matName;

            output.PushIndent();
            int count = matName.Count;
            int i = 0;
            foreach ( var matPair in matName)
            {
                output.AppendFormat("[\"{0}\"] = \"{1}\"", matPair.source.name, matPair.replace.name, true);
                output.PushDot(count != 1 && i + 1 != count);
                i++;
            }
            output.PopIndent();
        }
    }

}