using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LuaConfig
{
    public class StringOutput
    {
        public StringBuilder sb = new StringBuilder();

        public const string Indent = "    ";

        public int indentLevel = 0;
        bool indentDirty = false;

        public void SetIndentDirty(bool flag)
        {
            this.indentDirty = flag;
        }

        string curIndent = string.Empty;
        public string GetIndent()
        {
            if (!indentDirty)
            {
                return curIndent;
            }
            curIndent = "";
            for (int i = 0; i < indentLevel; i++)
            {
                curIndent = curIndent + Indent;
            }
            indentDirty = false;
            return curIndent;
        }

        public void PushIndent()
        {
            SetIndentDirty(true);
            indentLevel++;
        }

        public void PopIndent()
        {
            SetIndentDirty(true);
            indentLevel--;
            indentLevel = Mathf.Max(0, indentLevel);
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
            if (isNewLine) sb.Append(GetIndent());
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
}

