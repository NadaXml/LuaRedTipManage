using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LuaCodeTemplate", menuName = "CreateConfig")]
public class ConfigScriptObject : ScriptableObject
{
    [Space(10)]
    public Object templatePath;
    [Space(10)]
    public List<ConfigSingle> exports = new List<ConfigSingle>();
}

[System.Serializable]
public class ConfigSingle
{
    public Object go;
    public Object outputPath;
}