using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtention {
    public static bool Contains<T>(this T[] array, T Element)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(array[i], Element))
            {
                return true;
            }
        }
        return false;
    }
    public static bool Contains(this string[] array, string Element)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i]==Element)
            {
                return true;
            }
        }
        return false;
    }


}
public class ArrayUtility : MonoBehaviour {

	public static T[] Remove<T>(T[] array, T element) {
		List<T> lst= new List<T>();
		lst.AddRange(array);
		lst.Remove(element);
		return lst.ToArray();
	}
    public static void Remove<T>(ref T[] array, T element)
    {
        List<T> lst = new List<T>();
        lst.AddRange(array);
        lst.Remove(element);
        array = lst.ToArray();
    }

    public static void Add<T>(ref T[] array, T element) {
		List<T> lst= new List<T>();
		lst.AddRange(array);
		lst.Add(element);
        array = lst.ToArray();
	}


    public static T[] Insert<T>(T[] array, T element, int at) {
		List<T> lst= new List<T>();
		lst.AddRange(array);
		lst.Insert(at, element);
		return lst.ToArray();
	}
	public static List<T> ArrayToList<T>(T[] array) {
		List<T> lst= new List<T>();
		lst.AddRange(array);
		return lst;
	}
    public static bool Contains<T>(T[] array, T Element)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(array[i], Element))
            {
                return true;
            }
        }
        return false;
    }
    public static int IndexOf<T>(T[] array, T Element)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(array[i], Element))
            {
                return i;
            }
        }
        return -1;
    }
    public static int IndexOf(string[] array, string Element)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i]==Element)
            {
                return i;
            }
        }
        return -1;
    }
    public static int findIndex<T> (T[] array, T value)
	{
		for (int i = 0; i < array.Length; i++) {
			if (array [i].ToString() == value.ToString()) {
				return i;
			}
		}
		return -1;
	}
	public static object[] ConvertToObjectArray (object obj)
	{
		try {
			object[] arrayObject = null;
            if (obj.GetType() == typeof(float[]))
            {
                float[] floats = (float[])obj;
                arrayObject = new object[floats.Length];
                for (int i = 0; i < arrayObject.Length; i++)
                {
                    arrayObject[i] = floats[i];
                }
                return arrayObject;
            }
            else if (obj.GetType().IsList())
            {
                IList collection = (IList)obj;
                arrayObject = new object[collection.Count];
                for (int i = 0; i < collection.Count; i++)
                {
                    arrayObject[i] = collection[i];
                }
                return arrayObject;
            }
            else
            {
                return (object[])obj;
            }
		} catch {
			Debug.LogError("Fail in conversion: "+obj.GetType());
		}
		return null;
	}
    public static string[] ConvertToStringArray(object[] obj)
    {
        string[] arrayOfStrings = new string[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            arrayOfStrings[i] = obj[i].ToString();
        }
        return arrayOfStrings;
    }
}
