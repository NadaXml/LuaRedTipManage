using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class CodeCreateor : MonoBehaviour
{
    static CodeTemplateCreator creator = new CodeTemplateCreator();
    [ContextMenu("ExportPanel")]
    public void create()
    {

    }
    [ContextMenuItem("ExportItem", "createPanel")]
    public int a = 5;

    public void createPanel()
    {

    }
    [MenuItem("Assets/Create/LuaCode/GuideBaseItem")]
    public static void createGuideBaseItem()
    {
        GameObject go = Selection.activeObject as GameObject;
        if ( go == null )
        {
            Debug.LogError("请选择一个Prefab");
            return;
        }

        // TODO 其他Check


        creator.CreateCode("GuideBaseItemT", go.name);
    }
}

public class CodeReaplceCtx
{
    public const string LuaClassName = "#LuaClassName#";

    private Dictionary<string, string> replaceMap = new Dictionary<string, string>();

    public void AddLuaClassNameReplace(string value)
    {
        replaceMap[LuaClassName] = value;
    }

    public string replaceString(string str)
    {
        foreach ( var pair in replaceMap )
        {
            str = str.Replace(pair.Key, pair.Value);
        }
        return str;
    }
}


public class CodeTemplateCreator
{

    public void CreateCode(string typeName, string prefabName)
    {
        string scriptAssetPath = "Assets/ThirdParty/LuaCodeTeamplate/LuaCodeTemplate.asset";
        ConfigScriptObject asset = AssetDatabase.LoadAssetAtPath<ConfigScriptObject>(scriptAssetPath);

        foreach (var single in asset.exports)
        {
            if ( typeName == single.go.name )
            {
                string path = AssetDatabase.GetAssetPath(single.go.GetInstanceID());
                CodeReaplceCtx ctx = GetReplaceCtx(prefabName);
                string sourceStr = GetTeamplateStrFromFile(path);
                sourceStr = ctx.replaceString(sourceStr);

                string outFilePath = AssetDatabase.GetAssetPath(single.outputPath) + "/" + prefabName + ".txt";
                SaveDstLuaFile(sourceStr, outFilePath);
                AssetDatabase.Refresh();
                break;
            }
            Debug.Log("   " + single.go.name);
        }
    }

    public string GetTeamplateStrFromFile(string filePath)
    {
        StreamReader sr = new StreamReader(filePath);
        string str = sr.ReadToEnd();
        sr.Close();
        return str;
    }

    public void SaveDstLuaFile(string content, string dstPath)
    {
        StreamWriter sw = new StreamWriter(dstPath);
        sw.Write(content);
        sw.Close();
    }

    public CodeReaplceCtx GetReplaceCtx(string prefabName)
    {
        CodeReaplceCtx ctx = new CodeReaplceCtx();
        ctx.AddLuaClassNameReplace(prefabName);
        return ctx;
    }

}
