

using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;


[CustomPropertyDrawer(typeof(GetComponentHereAttribute))]
public class GetComponentHereDrawer : PropertyDrawer
{

    GetComponentHereAttribute target { get { return ((GetComponentHereAttribute)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue == null)
        {
            string tipo = property.type;
            tipo = tipo.Replace("PPtr<$", "");
            tipo = tipo.Replace(">", "");

            MonoBehaviour inspectedObject = (MonoBehaviour)property.serializedObject.targetObject;
            property.objectReferenceValue = inspectedObject.GetComponent(tipo);
            if (property.objectReferenceValue == null)
            {
                property.objectReferenceValue = GetComponentInChildren(inspectedObject.transform, tipo);
            }
            else
            {
                // Debug.Log("Component Obtained: " + tipo);
            }
            if (property.objectReferenceValue == null)
            {
                //Debug.Log("Component not found: " + tipo);
            }
        }
        if (target.force)
            GUI.enabled = false;


        if (target.labelWidth == 0f)
            EditorGUI.PropertyField(position, property, label, true);
        else
            ResponsiveDrawer.ResponsivePropertyField(position, property, label, target.labelWidth, target.valueWidth);
    }
    Component GetComponentInChildren(Transform transform, string typeName)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Component Component = transform.transform.GetChild(i).GetComponent(typeName);
            if (Component == null)
            {
                Component = GetComponentInChildren(transform.transform.GetChild(i), typeName);
            }
            if (Component != null)
            {
                Debug.Log("Component Obtained: " + typeName);
                return Component;
            }
        }
        Debug.Log("Component not found in it or children: " + typeName);
        return null;
    }
}