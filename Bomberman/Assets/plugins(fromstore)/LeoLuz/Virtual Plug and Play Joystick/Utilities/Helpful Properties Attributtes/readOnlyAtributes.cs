using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
//public class readOnlyAtributes : MonoBehaviour {

//    [ReadOnly]
//    public string a;
//    [ReadOnly]
//    public int b;
//    [Header("sadsa")]
//    public Material c;
//    [ReadOnly]
//    public List<int> d = new List<int>();
//}

public class ReadOnlyAttribute : PropertyAttribute
{

}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif
//#if !UNITY_EDITOR
//public class ReadOnly : System.Attribute
//{

//}
//#endif
