using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;



public class PropertyDrawerGFPro : PropertyDrawer
{
    public static GUIStyle HeaderOpenedWindowStyle;
    public static GUIStyle HeaderClosedWindowStyle;
    public static GUIStyle PositiveInnerBoxWindow;
    public static GUIStyle asd;


    public static GUIContent emptyLabel = new GUIContent("");

    public bool CheckDifferent(SerializedProperty property, string fieldName, object valueToCompare)
    {
        object obtainedValue = GetFieldValue(property, fieldName);
        if (obtainedValue.ToString() != valueToCompare.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckEqual(SerializedProperty property, string fieldName, object valueToCompare)
    {
        object obtainedValue = GetFieldValue(property, fieldName);
        if (obtainedValue.ToString() != valueToCompare.ToString())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public object GetFieldValue(SerializedProperty SibilingProperty, string fieldName)
    {
        object parent = SibilingProperty.GetParent();
        return GetFieldValue(parent, fieldName);
    }

    public object GetFieldValue(object parent, string fieldName)
    {
        if (parent == null)
            return null;

        FieldInfo field = parent.GetType().GetField(fieldName);
        if (field == null)
            return null;
        object obtainedValue = field.GetValue(parent);
        return obtainedValue;
    }

    public void DrawPropertyResponsive(ResponsivePropertyAttribute drawer, Rect position, SerializedProperty property, GUIContent label, float LabelWidth = 190f)
    {
        float TotalWidthGUI = position.width - EditorGUI.indentLevel * 16.8f;
        if (TotalWidthGUI < LabelWidth * 2f)
        {
            Rect LabelRect = new Rect(position);
            LabelRect.width = LabelWidth;
            LabelRect.x = ((float)EditorGUI.indentLevel) * 16.8f;
            GUI.Label(LabelRect, label.text+":");

            position.y += 16f;
            drawer.LabelUseTwoLines = true;
           // position.width = TotalWidthGUI;
            EditorGUI.PropertyField(position, property, new GUIContent(""), true);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label, true);
        }


        position.height = 32f;
    }

    public SerializedProperty GetSiblingProperty (SerializedProperty property, string Name)
	{
		if(property.name==Name)
			return property;

		SerializedProperty prop = property.Copy ();
		int depth = prop.depth;

		while (prop.Next(false) && prop.depth == depth) {
			if (prop.name == Name) {
				return prop;
			}
		}
		Debug.LogError("Field not found");
		return null;
    }
}


public class HidesBaseDrawer : PropertyDrawerGFPro
{
    Hidesbase hidesBase { get { Debug.Log(attribute.GetType()); return ((Hidesbase)attribute); } }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (hidesBase.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label);
    }
}
