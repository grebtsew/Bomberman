using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LeoLuz;

[CustomPropertyDrawer(typeof(InputAxesListDropdownAttribute))]
public class InputAxisListDropdownDrawer : PropertyDrawerGFPro
{
    InputAxesListDropdownAttribute inputAxisDropdownAttribute { get { return ((InputAxesListDropdownAttribute)attribute); } }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object[] axesPackage = InputAxes.GetAxes();
        List<string> AxisLabelList = (List<string>)axesPackage[0];


        //find choice Index
        var choiceIndex = AxisLabelList.IndexOf(property.stringValue);

        if (inputAxisDropdownAttribute.hideLabel)
            choiceIndex = EditorGUI.Popup(position, choiceIndex, AxisLabelList.ToArray());
        else
            choiceIndex = EditorGUI.Popup(position, label.text, choiceIndex, AxisLabelList.ToArray());
        if (choiceIndex == -1)
            choiceIndex = 0;
        property.stringValue = AxisLabelList[choiceIndex];


    }
}