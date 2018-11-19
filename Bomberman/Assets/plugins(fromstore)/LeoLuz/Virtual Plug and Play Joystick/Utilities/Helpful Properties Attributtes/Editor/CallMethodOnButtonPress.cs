using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class CallFunctionOnButtonPressAttribute : Hidesbase
{
    public string ButtonText;
    public string ToolTip;
    public float Height;
    public string function;
    public string If;
    public object value;
    public bool InRootComponent;
    public bool useParentAsArgument;
    public enum _Operator {  equal, different }
    public _Operator Operator;

    public CallFunctionOnButtonPressAttribute(string ButtonText, string ToolTip, float Height=32f, string function = "", bool InRootComponent = true, string If = "", _Operator Operator = _Operator.equal, object value = null)
    {
        this.function = function;
        this.InRootComponent = InRootComponent;
        this.If = If;
        this.value = value;
        this.Operator = Operator;
        this.ButtonText = ButtonText;
        this.ToolTip = ToolTip;
        this.Height = Height;
    }
    public CallFunctionOnButtonPressAttribute(string ButtonText, float Height = 32f, string function = "", bool InRootComponent = true, string If = "", _Operator Operator = _Operator.equal, object value = null)
    {
        this.function = function;
        this.InRootComponent = InRootComponent;
        this.If = If;
        this.value = value;
        this.Operator = Operator;
        this.ButtonText = ButtonText;
        this.Height = Height;
    }
    public CallFunctionOnButtonPressAttribute(string ButtonText, float Height, string function, bool InRootComponent, bool useParentAsArgument, string If = "", _Operator Operator = _Operator.equal, object value = null)
    {
        this.function = function;
        this.InRootComponent = InRootComponent;
        this.If = If;
        this.value = value;
        this.Operator = Operator;
        this.ButtonText = ButtonText;
        this.Height = Height;
        this.useParentAsArgument = useParentAsArgument;
    }
}



[CustomPropertyDrawer(typeof(CallFunctionOnButtonPressAttribute))]
public class CallMethodOnButtonPressAttributeDrawer : PropertyDrawer
{

    CallFunctionOnButtonPressAttribute target { get { return ((CallFunctionOnButtonPressAttribute)attribute); } }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object parent = property.GetParent();
        if (parent == null)
            return;

        if (target.If != "")
        {
            FieldInfo fieldToCheck = parent.GetType().GetField(target.If);
            if (fieldToCheck == null)
                return;

            object objToCheck = fieldToCheck.GetValue(parent);

            if (target.Operator == CallFunctionOnButtonPressAttribute._Operator.equal)
            {
                if (objToCheck == null || (objToCheck.ToString() == target.value.ToString()))
                {
                    target.hide(property.GetParent(), false);                
                }
                else
                {
                    target.hide(property.GetParent(), true);
                    return;
                }
            } else
            {
                if (objToCheck == null || (objToCheck.ToString() != target.value.ToString()))
                {
                    target.hide(property.GetParent(), false);
                }
                else
                {
                    target.hide(property.GetParent(), true);
                    return;
                }
            }
        }

        position.x += EditorGUI.indentLevel * 15f;
        position.width -= EditorGUI.indentLevel * 15f;
        position.height = target.Height;
        if (GUI.Button(position, new GUIContent(target.ButtonText, target.ToolTip)))
        {
            if (!target.InRootComponent)
            {
                MethodInfo MethodField = parent.GetType().GetMethod(target.function);
                if (MethodField != null)
                {
                    MethodField.Invoke(parent, null);
                }
            }
            else
            {
                MonoBehaviour component = (MonoBehaviour)property.serializedObject.targetObject;
                Debug.Log("0f");
                if (component == null)
                    return;
                Debug.Log("1");
                MethodInfo MethodField = component.GetType().GetMethod(target.function);
                if (MethodField != null)
                {
                    Debug.Log("2");
                    object[] parameters = new object[1];
                    parameters[0] = parent;
                    MethodField.Invoke(component, target.useParentAsArgument? parameters : null);
                }
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (target.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label)+ target.Height - 16f;
    }

}

