using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TMPLang
{
    public class TMPLangUtility
    {
        public delegate T createT<out T>(string key);
        public delegate bool compT<in T>(T t, string key);

        public static void FillList<T>(List<T> tList, string[] keys, compT<T> compare, createT<T> creator)
        {
            int keyIndex = 0;
            foreach (string key in keys)
            {
                int findIndex = tList.FindIndex((T t)=>
                {
                    return compare(t, key);
                });

                if (findIndex != -1)
                {
                    if (findIndex != keyIndex)
                    {
                        T temp = tList[findIndex];
                        tList[findIndex] = tList[keyIndex];
                        tList[keyIndex] = temp;
                    }
                }
                else
                {
                    T preset = creator(key);
                    tList.Insert(keyIndex, preset);
                }
                keyIndex++;
            }

            for (int i = tList.Count - 1; i >= keys.Length; i--)
            {
                tList.RemoveAt(i);
            }
        }
    }
}


