using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.Text;
using System.IO;

public class Teaplate
{
    [MenuItem("Assets/create")]
    public static void create()
    {
        //TMP_FontAsset fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/TextMesh Pro/Fonts/LiberationSans SDF");
        AssetDatabase.CopyAsset("Assets/TextMesh Pro/Fonts/LiberationSans SDF.asset", "Assets/TextMesh Pro/Fonts/LiberationSans SDF2.asset");
        AssetDatabase.Refresh();
    }
}

[CreateAssetMenu(fileName = "Assets/TextMesh Pro/Fonts//LangFontConfig", menuName ="LangFontConfig")]
public class LangFontScriptObj : ScriptableObject
{
    [Space(10)]
    [Header("导出路径")]
    public Object configOutFile;

    [Space(10)]
    public List<FontCompare> TH;
    public List<FontCompare> CN;
    public List<FontCompare> JP;
    public List<FontCompare> EN;

    public void SaveToLuaTable()
    {
        bool isDot = false;
        LuaSaveStringBuilder sb = new LuaSaveStringBuilder();
        sb.AppendLine("Lang.font = {", true);

        isDot = true;
        GetLang(sb, TH, "TH", isDot);
        

        GetLang(sb, CN, "CN", isDot);

        GetLang(sb, EN, "EN", isDot);
        isDot = false;
        GetLang(sb, JP, "JP", isDot);

        sb.AppendLine("}", true);

        string dir = AssetDatabase.GetAssetPath(configOutFile);
        StreamWriter sw = new StreamWriter(dir + "/lang.txt");
        sw.Write(sb.sb.ToString());
        sw.Close();
        AssetDatabase.Refresh();
    }

    public void GetLang(LuaSaveStringBuilder sb, List<FontCompare> fcList, string langName, bool isDot)
    {
        sb.PushIndent();
        sb.AppendFormat("[LangEnum.{0}] = {{\n", langName, true);
        GetListLuaString(sb, fcList);
        sb.Append("}", true);
        sb.PushDot(isDot);
        sb.PopIndent();
    }

    public void GetListLuaString(LuaSaveStringBuilder sb, List<FontCompare> fcList)
    {
        sb.PushIndent();
        int count = fcList.Count;
        for (int k = 0; k < count; k++)
        {
            GetLuaForList(sb, fcList[k]);
            sb.PushDot(count != 1 && k + 1 != count);
        }
        sb.PopIndent();
    }

    public void GetLuaForList(LuaSaveStringBuilder sb, FontCompare fc)
    {
        sb.AppendFormat("[\"{0}\"] = \"{1}\"", fc.key, fc.fontAsset.name, true);
    }
}

public class LuaSaveStringBuilder
{
    public StringBuilder sb = new StringBuilder();

    public const string Indent = "    ";

    public Stack<string> indentStack = new Stack<string>();

    bool flag = false;
    public void SetIndentDirty(bool flag)
    {
        this.flag = flag;
    }

    string curIndent = string.Empty;
    public string GetIndent()
    {
        if ( flag )
        {
            curIndent = "";
            foreach ( var indent in indentStack)
            {
                curIndent = curIndent + indent;
            }
            flag = false;
        }
        return curIndent;
    }

    public void PushIndent()
    {
        SetIndentDirty(true);
        indentStack.Push(Indent);
    }

    public void PopIndent()
    {
        SetIndentDirty(true);
        indentStack.Pop();
    }

    public void PushDot(bool isDot)
    {
        if (isDot)
        {
            sb.AppendLine(",");
        }
        else
        {
            sb.AppendLine();
        }
    }

    public void AppendLine(string value, bool isNewLine = false)
    {
        if ( isNewLine ) sb.Append(GetIndent());
        sb.AppendLine(value);
    }

    public void AppendFormat(string format, object obj1, bool isNewLine = false)
    {
        if (isNewLine) sb.Append(GetIndent());
        sb.AppendFormat(format, obj1);
    }

    public void AppendFormat(string format, object obj1, object obj2, bool isNewLine = false)
    {
        if (isNewLine) sb.Append(GetIndent());
        sb.AppendFormat(format, obj1, obj2);
    }

    public void AppendFormat(string format, object[] objs, bool isNewLine = false)
    {
        if (isNewLine) sb.Append(GetIndent());
        sb.AppendFormat(format, objs);
    }

    public void Append(string value, bool isNewLine = false)
    {
        if (isNewLine) sb.Append(GetIndent());
        sb.Append(value);
    }
}

[System.Serializable]
public class FontCompare
{
    public string key;
    public Object fontAsset;
}

[CustomEditor(typeof(LangFontScriptObj))]
public class LangFontScriptObjInspector : Editor
{
    LangFontScriptObj config;

    public void OnEnable()
    {
        config = target as LangFontScriptObj;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        if (GUILayout.Button("导出多语言配置"))
        {
            config.SaveToLuaTable();
        }
    }
}