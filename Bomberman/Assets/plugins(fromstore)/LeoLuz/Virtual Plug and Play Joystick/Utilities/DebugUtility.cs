using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
public static class d
{
    public static void l(object txt)
    {
        UnityEngine.Debug.Log(txt.ToString());
    }
    public static void le(string txt)
    {
        UnityEngine.Debug.LogError(txt.ToString());
    }
}
public class DebugUtility : MonoBehaviour {

	/// <summary>
	/// Dump an object class
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="depth">Depth.</param>
    public static void PrintDatabase(object obj, int depth=1, bool dumpReferencedClassObjects=false)
    {
        string DBString = "<color=red>OBJECT DUMPING</color> OF <color=green>" + obj.GetType().ToString()+"</color> <color=blue>Id: "+ obj .GetHashCode()+ "</color>";
		DBString += GetDatabaseString(obj,0,"",depth, dumpReferencedClassObjects);
        Debug.Log(DBString+"\n\n\n\n");
    }
	public static string GetObjectDump(object obj, int depth=5, bool dumpReferencedClassObjects = true)
    {
        string DBString = "DATABASE OF <color=green>"+obj.GetType().ToShortString()+"</color>";
		DBString += GetDatabaseString(obj,0,"",depth);
       return (DBString+"\n\n\n\n");
    }
    public static string GetDatabaseString (object obj, int identLevel, string name = "", int maxLevel = 6, bool dumpReferencedClassObjects = true)

    {

		if (identLevel > maxLevel)
			return "";
		if (obj == null) {
			// return name+" Null";
			return "";
		}
		string db = "";
		if (obj.GetType ().IsArray || obj.GetType().IsList()) {
			if (obj != null) {				
				object[] arrayObject = ArrayUtility.ConvertToObjectArray (obj);

				for (int ic = 0; ic < arrayObject.Length; ic++) {
					db += "\n" + GetIdentSpaces (identLevel) + ("Element: " + ic+" id: <color=blue>"+arrayObject [ic].GetHashCode()+"</color>");
					if (!arrayObject [ic].GetType ().IsValueType && arrayObject [ic].GetType ()!=typeof(string)) {
						db += GetDatabaseString (arrayObject [ic], identLevel + 1, "", maxLevel);
					} else {
						db += "\n<color=magenta>"+GetIdentSpaces(identLevel+1)+arrayObject [ic].ToString()+"</color>";
					}
				}
			}
		} else
		if (!obj.GetType ().IsGenericType && obj.GetType ().IsClass) {
			FieldInfo[] _PropertyInfos = obj.GetType ().GetFields ();
            
			foreach (var fieldInfo in _PropertyInfos) {
				var value = fieldInfo.GetValue (obj);
				var valueStr = value != null ? fieldInfo.FieldType.IsValueType || fieldInfo.FieldType == typeof(string) ? "<color=magenta>" + value.ToString () + "</color>" : "<color=green>" + fieldInfo.FieldType.ToShortString () + "</color> Id: <color=blue>" + value.GetHashCode () + "</color>" : "<color=red>Null</color>";

				db += "\n" + GetIdentSpaces (identLevel) + ((fieldInfo.FieldType.IsClass ? "" : "") + fieldInfo.Name + ": " + valueStr);
				if (value != null && dumpReferencedClassObjects) {
					if (fieldInfo.FieldType.IsClass || obj.GetType ().IsArray || (obj.GetType ().IsGenericType && (obj.GetType ().GetGenericTypeDefinition () == typeof(List<>))))
						db += GetDatabaseString (value, identLevel + 1, "", maxLevel);

				}
			}
		} else {
			Debug.Log("Fail: "+obj.GetType ());

		}

        return db;    
    }
    static string GetIdentSpaces(int identLevel)
    {
        string str=""; 
        for (int i = 0; i < identLevel; i++)
        {
            str += "          ";
        }
        return str;
    }
}
