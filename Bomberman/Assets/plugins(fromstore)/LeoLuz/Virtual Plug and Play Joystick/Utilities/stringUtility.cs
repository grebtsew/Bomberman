using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtility : MonoBehaviour {

	public static string Concat(string[] array, string separator="")
    {
        string newStr="";
        for (int i = 0; i < array.Length; i++)
        {
            if (i > 0 && array[i]!="")
            {
                newStr += separator;
            }
            newStr += array[i];
        }
        return newStr;
    }
    public static bool Contains(string[] array, string element = "")
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == element)
                return true;
        }
        return false;
    }
}
