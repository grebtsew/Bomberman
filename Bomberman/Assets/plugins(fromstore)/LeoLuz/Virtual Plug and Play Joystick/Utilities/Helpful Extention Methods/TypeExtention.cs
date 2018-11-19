using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TypeExtensions
{
    /// <summary>
    /// Compare if this layerNumber is in layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static string ToShortString(this Type type)
    {
		string str=type.ToString();
		string[] splited=str.Split('.');
		string[] splited2=splited[splited.Length-1].Split('+');
		return splited2[splited2.Length-1]; 
    }

    public static bool IsList(this Type type)
    {
        return typeof(IList).IsAssignableFrom(type);
    }
}