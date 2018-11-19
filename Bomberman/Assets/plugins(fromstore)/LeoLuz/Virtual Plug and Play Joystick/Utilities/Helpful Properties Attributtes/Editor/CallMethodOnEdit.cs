using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class CallMethodOnViewAttribute : Hidesbase
{
    public string method;
    public bool Hide;

    public CallMethodOnViewAttribute(string method, bool Hide)
    {
        this.method = method;
        this.Hide = Hide;
    }
}
public class CallMethodOnEditAttribute : Hidesbase
{
    public string method;
    public string hideIfVariable;
    public object Equal;
    public bool InRootComponent;

    public CallMethodOnEditAttribute(string method = "", bool InRootComponent = true, string hideIfVariable = "", object equal = null)
    {
        this.method = method;
        this.InRootComponent = InRootComponent;
        this.hideIfVariable = hideIfVariable;
        this.Equal = equal;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(CallMethodOnEditAttribute))]
public class CallMethodOnEditDrawer : PropertyDrawer
{

    CallMethodOnEditAttribute target { get { return ((CallMethodOnEditAttribute)attribute); } }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object TargetObjectParentClass = property.GetParent();
        if (TargetObjectParentClass == null)
            return;

        if (target.hideIfVariable != "")
        {
            FieldInfo fieldToCheck = TargetObjectParentClass.GetType().GetField(target.hideIfVariable);
            if (fieldToCheck == null)
                return;

            object objToCheck = fieldToCheck.GetValue(TargetObjectParentClass);

            if (objToCheck == null || (objToCheck.ToString() == target.Equal.ToString()))
            {

                target.hide(property.GetParent(), true);
            }
            else
            {
                target.hide(property.GetParent(), false);
                EditorGUI.PropertyField(position, property, label, true);
            }
        } else
        {
            EditorGUI.PropertyField(position, property);
        }
        if(GUI.changed)
        {
            //property.serializedObject.ApplyModifiedProperties();
            if (!target.InRootComponent)
            {
                MethodInfo MethodField = TargetObjectParentClass.GetType().GetMethod(target.method);
                if (MethodField != null)
                {
                    MethodField.Invoke(TargetObjectParentClass, null);
                }
            } else
            {
                MonoBehaviour component = (MonoBehaviour)property.serializedObject.targetObject;
                if (component == null)
                    return;
                MethodInfo MethodField = component.GetType().GetMethod(target.method);
                if (MethodField != null)
                {
                    MethodField.Invoke(component, null);
                }
            }
        }        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (target.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label);
    }

}

[CustomPropertyDrawer(typeof(CallMethodOnViewAttribute))]
public class CallMethodOnViewDrawer : PropertyDrawer
{

    CallMethodOnViewAttribute target { get { return ((CallMethodOnViewAttribute)attribute); } }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object parent = property.GetParent();

        if (!target.Hide)
            EditorGUI.PropertyField(position, property);
        else
            target.hide(parent, true);


        if (parent == null)
            return;

        MethodInfo methodInfo= parent.GetType().GetMethod(target.method);
        if(methodInfo!=null)
        {
            methodInfo.Invoke(parent, null);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (target.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label);
    }

}
#endif