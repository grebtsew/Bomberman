using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer(typeof(hideIf))]
public class HideIfDrawer : PropertyDrawerGFPro
{

    hideIf target { get { return ((hideIf)attribute); } }

    public static float LastUpdate;
    public static float LastPosition;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (LastUpdate == Time.time && LastPosition == position.y)
            position.y += position.height;

        object parent = property.GetParent();
        if (parent == null)
            return;

        if (target.varName==null)
        {


            if (property.floatValue.ToString() != target.value.ToString())
            {
                if (target.labelWidth == 0f)
                {
                        EditorGUI.PropertyField(position, property, label, true);

                }
                else
                    ResponsiveDrawer.ResponsivePropertyField(position, property, label, target.labelWidth, target.valueWidth);
                target.skipLine = false;
                target.hide(parent, false);
            } else
            {
                target.skipLine = true;
                target.hide(parent, true);
            }
            return;
        }

        FieldInfo fieldToCheck = parent.GetType().GetField(target.varName);
        object obj = fieldToCheck.GetValue(parent);

        if (obj.ToString() == target.value.ToString())
        {
            if (target.skipLine)
                target.hide(parent, true);
        }
        else
        {
            target.hide(parent, false);

            if (target.readOnly)
                GUI.enabled = false;

                EditorGUI.PropertyField(position, property, label, true);

        }
     //   Debug.Log(property.name+" - "+Time.time);  
        LastUpdate = Time.time;
        LastPosition = position.y;
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if(target.skipLine && target.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label)+ 3f;

    }
}

//[CustomPropertyDrawer(typeof(HideIf))]
//public class RenameIfDrawer : PropertyDrawerGFPro
//{

//    HideIf target { get { return ((HideIf)attribute); } }

//    public static float LastUpdate;
//    public static float LastPosition;

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        //if (LastUpdate == Time.time && LastPosition == position.y)
//        //    position.y += position.height;

//        //object parent = property.GetParent();
//        //if (parent == null)
//        //    return;

//        //if (target.varName == null)
//        //{
//        //    FieldInfo selfField = parent.GetType().GetField(property.name);
//        //    object selfFieldObj = selfField.GetValue(parent);
//        //    if (property.floatValue.ToString() != target.value.ToString())
//        //    {
//        //        EditorGUI.PropertyField(position, property, label, true);
//        //    }
//        //    return;
//        //}

//        //FieldInfo fieldToCheck = parent.GetType().GetField(target.varName);
//        //object obj = fieldToCheck.GetValue(parent);

//        //if (obj.ToString() == target.value.ToString())
//        //{
//        //    if (target.skipLine)
//        //        target.hide(property.GetParent(), true);
//        //}
//        //else
//        //{
//        //    target.hide(property.GetParent(), false);

//        //    if (target.readOnly)
//        //        GUI.enabled = false;

//        //    EditorGUI.PropertyField(position, property, label, true);
//        //}
//        ////   Debug.Log(property.name+" - "+Time.time);  
//        //LastUpdate = Time.time;
//        //LastPosition = position.y;
//    }
//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        if (target.skipLine && target.CheckHided(property.GetParent()))
//            return -EditorGUIUtility.standardVerticalSpacing;
//        else
//            return EditorGUI.GetPropertyHeight(property, label);

//    }
//}

[CustomPropertyDrawer(typeof(ExtendLabelAttribute))]
public class ResponsiveDrawer : PropertyDrawerGFPro
{

    ExtendLabelAttribute target { get  { return ((ExtendLabelAttribute)attribute); } }

    public static float LastUpdate;
    public static float LastPosition;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ResponsivePropertyField(position, property, label, target.size, target.valueWidth);
    }
    public static void ResponsivePropertyField(Rect position, SerializedProperty property, GUIContent label,  float labelWidth, float valueWidth = 32f)
    {
        if (label.text==null)
            return;
        if (labelWidth==-1f) //Automatic calculate width
        {
            labelWidth = label.text.Length * 8f;
        }
        Rect labelRect = new Rect(position);
        Rect valueRect = new Rect(position);
        float indentMargin = EditorGUI.indentLevel * 14f;
        float realWidth = position.width - indentMargin;
        //position.x -= indentMargin;

        valueRect.x += labelWidth;
        valueRect.width -= labelWidth;
        if (valueRect.width < valueWidth + indentMargin)
        {
            valueRect.x = realWidth - valueWidth;
            valueRect.width = valueWidth + indentMargin;
            labelRect.width = position.width - valueWidth+6f;
        }

        if (valueRect.x + indentMargin < position.width * 0.39f)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            EditorGUI.LabelField(labelRect, label.text);
            EditorGUI.PropertyField(valueRect, property, new GUIContent(""), true);
        }
        //EditorGUI.PropertyField(position, property, label, true);
        return;
    }
}
[CustomPropertyDrawer(typeof(HideDifferent))]
public class HideDifferentDrawer : PropertyDrawerGFPro
{

    HideDifferent target { get { return ((HideDifferent)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
       // Debug.Log(0);
        object parent = property.GetParent();
        if (parent == null)
        {
            Debug.Log("ERROR Null: " + property.displayName);
            return;
        }
     //   Debug.Log(1+ "target.varName"+ target.varName);
        FieldInfo fieldToCheck = parent.GetType().GetField(target.varName);
        if (fieldToCheck == null)
        {
            Debug.Log("ERROR Null: " + property.displayName);
            return;
        }
        if(target.DisplaceLevel!=0)
        {
            position.y += 18f* target.DisplaceLevel;
        }

        object objToCheck = fieldToCheck.GetValue(parent);
  //      Debug.Log(2+"value: "+ objToCheck.ToString());
        if (objToCheck == null || (objToCheck.ToString() != target.value.ToString()))
        {
            if (!target.skipLine)
            {
                target.hide(property.GetParent(), true);
            }
    //        Debug.Log(3);
        }
        else
        {
        //    Debug.Log(4+ label.text);
            target.hide(property.GetParent(), false);
            if (target.hideLabel)
            {

                    EditorGUI.PropertyField(position, property, new GUIContent(""), true);

               // Debug.Log(5 + label.text);
            }
            else
            {
                if(target.labelWidth!=0f && target.labelWidth != 0f)
                {
                    ResponsiveDrawer.ResponsivePropertyField(position, property, label, target.labelWidth, target.valueWidth);
                } else
                {
                        EditorGUI.PropertyField(position, property, label, true);
                }

            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (target.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label)+ target.DisplaceLevel * 18f+3f;



    }



}
[CustomPropertyDrawer(typeof(ReadOnlyIfDifferent))]
public class ReadOnlyIfDifferentDrawer : PropertyDrawerGFPro
{

    ReadOnlyIfDifferent AttributeTarget { get { return ((ReadOnlyIfDifferent)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object value = GetFieldValue(property, AttributeTarget.varName);
        bool _guiEnabled = GUI.enabled;
        if (value.ToString() != AttributeTarget.ValueToCheck.ToString())
        {
            GUI.enabled = false;
        }
        if(AttributeTarget.labelWidth==0f)
            EditorGUI.PropertyField(position, property, true);
        else
            ResponsiveDrawer.ResponsivePropertyField(position, property, label, AttributeTarget.labelWidth, AttributeTarget.valueWidth);

        GUI.enabled = _guiEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
            return EditorGUI.GetPropertyHeight(property, label);
    }
}
[CustomPropertyDrawer(typeof(RangeReadOnlyIfDifferent))]
public class RangeReadOnlyIfDifferentDrawer : PropertyDrawerGFPro
{

    RangeReadOnlyIfDifferent AttributeTarget { get { return ((RangeReadOnlyIfDifferent)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object value = GetFieldValue(property, AttributeTarget.varName);
        bool _guiEnabled = GUI.enabled;
        if (value.ToString() != AttributeTarget.ValueToCheck.ToString())
        {
            GUI.enabled = false;
        }
        EditorGUI.Slider(position, property, AttributeTarget.min, AttributeTarget.max, label);
        GUI.enabled = _guiEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label);
    }
}
[CustomPropertyDrawer(typeof(RangeHideIfDifferent))]
public class RangeHideIfDifferentDrawer : PropertyDrawerGFPro
{
    RangeHideIfDifferent AttributeTarget { get { return ((RangeHideIfDifferent)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object value = GetFieldValue(property, AttributeTarget.varName);

        if (value.ToString() != AttributeTarget.ValueToCheck.ToString())
        {
            AttributeTarget.hide(property.GetParent(), true);
            return;
        }
        AttributeTarget.hide(property.GetParent(), false);
        if (AttributeTarget.upperLabel)
        {
            EditorGUI.LabelField(position, label.text + ":");
            position.y += 16f;
            position.height = 16f;
            EditorGUI.Slider(position, property, AttributeTarget.min, AttributeTarget.max, new GUIContent());
        }
        else
        {
            EditorGUI.Slider(position, property, AttributeTarget.min, AttributeTarget.max, label);
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (AttributeTarget.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label)*2f;
    }
}
[CustomPropertyDrawer(typeof(HideIfAnyIsDifferent))]
public class HideBothDifferentDrawer : PropertyDrawerGFPro
{

    HideIfAnyIsDifferent AttributeTarget { get { return ((HideIfAnyIsDifferent)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object TargetObjectParentClass = property.GetParent();
        if (TargetObjectParentClass == null)
        {
            return;
        }

        FieldInfo fieldToCheck = TargetObjectParentClass.GetType().GetField(AttributeTarget.varName);
        FieldInfo field2ToCheck = TargetObjectParentClass.GetType().GetField(AttributeTarget.varName2);
        if (fieldToCheck == null || field2ToCheck == null)
        {
            return;
        }

        object objToCheck = fieldToCheck.GetValue(TargetObjectParentClass);
        object obj2ToCheck = field2ToCheck.GetValue(TargetObjectParentClass);

        if (objToCheck == null || obj2ToCheck == null || (objToCheck.ToString() != AttributeTarget.value.ToString() || obj2ToCheck.ToString() != AttributeTarget.value2.ToString()))
        {
            if (!AttributeTarget.skipLine)
            {
                AttributeTarget.hide(property.GetParent(), true);
            }
        }
        else
        {

            AttributeTarget.hide(property.GetParent(), false);
            if (AttributeTarget.hideLabel)
                EditorGUI.PropertyField(position, property, new GUIContent(""), true);
            else
                EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (AttributeTarget.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label);
    }



}

[CustomPropertyDrawer(typeof(HideByClausule))]
public class HideByClausuleDrawer : PropertyDrawerGFPro
{

    HideByClausule AttributeTarget { get { return ((HideByClausule)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object TargetObjectParentClass = property.GetParent();
        if (TargetObjectParentClass == null)
            return;
        
        FieldInfo fieldToCheck = TargetObjectParentClass.GetType().GetField(AttributeTarget.varName);
        FieldInfo field2ToCheck = TargetObjectParentClass.GetType().GetField(AttributeTarget.varName2);
        if (fieldToCheck == null || field2ToCheck == null)
            return;

        object objToCheck = fieldToCheck.GetValue(TargetObjectParentClass);
        object obj2ToCheck = field2ToCheck.GetValue(TargetObjectParentClass);

        bool firstClausuleApproved = false;
        if(AttributeTarget.FirstClausule== HideByClausule.Clausule.different)
        {
            if (objToCheck == null || (objToCheck.ToString() != AttributeTarget.value.ToString()))
            {
                firstClausuleApproved = true;
            }
        } else
        {
            if (objToCheck == null || (objToCheck.ToString() == AttributeTarget.value.ToString()))
            {
                firstClausuleApproved = true;
            }
        }

        bool SecondClausuleApproved = false;
        if (AttributeTarget.SecondClausule == HideByClausule.Clausule.different)
        {
            if (obj2ToCheck == null || (obj2ToCheck.ToString() != AttributeTarget.value2.ToString()))
            {
                SecondClausuleApproved = true;
            }
        }
        else
        {
            if (obj2ToCheck == null || (obj2ToCheck.ToString() == AttributeTarget.value2.ToString()))
            {
                SecondClausuleApproved = true;
            }
        }


        if ((AttributeTarget._Operator == HideByClausule.Operator.and && firstClausuleApproved && SecondClausuleApproved) ||
            (AttributeTarget._Operator == HideByClausule.Operator.or && firstClausuleApproved || SecondClausuleApproved))
        {
            if (!AttributeTarget.skipLine)
            {
                AttributeTarget.hide(property.GetParent(), true);
            }
        }
        else
        {
            AttributeTarget.hide(property.GetParent(), false);
            if (AttributeTarget.hideLabel)
                EditorGUI.PropertyField(position, property, new GUIContent(""), true);
            else
                EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (AttributeTarget.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label);
    }



}

//[CustomPropertyDrawer(typeof(HitInfoGuideButtonsAttribute))]
//public class HitInfoGuideButtonsDrawer : PropertyDrawerGFPro
//{

//    HitInfoGuideButtonsAttribute AttributeTarget { get { return ((HitInfoGuideButtonsAttribute)attribute); } }

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        position.x += EditorGUI.indentLevel * 15f;
//        position.width -= EditorGUI.indentLevel * 15f;

//        object TargetObjectParentClass = property.GetParent();
//        if (TargetObjectParentClass == null)
//            return;

//        FieldInfo fieldToCheck = TargetObjectParentClass.GetType().GetField(AttributeTarget.targetName);
//        if (fieldToCheck == null)
//            return;

//        object objToCheck = fieldToCheck.GetValue(TargetObjectParentClass);

//        if (objToCheck == null || (objToCheck.ToString() != AttributeTarget.value.ToString()))
//        {
//            AttributeTarget.SetHeight(property.GetParent(), -EditorGUIUtility.standardVerticalSpacing);
//        }
//        else
//        {
//            Texture tex = Resources.Load("InfoIcon") as Texture;
//            position.height = 32f;
//            if (GUI.Button(position, new GUIContent("How it works?", "The HitInfo is the component with the hit information that will be made available on the hit collider that runs along with the animation. You can capture the HitInfo when your enemy receives the hit. Code sample:\n\nvoid OnTriggerEnter2D(Collider2D col) {\n   HitInfo hitInfo = col.GetComponent<HitInfo>();\n   if(HitInformation!=null)\n      TakeDamage(hitInfo.damageValue);\n}\n\nTake a look at the HitInfo component, and note that there are several variables that can be used to capture information about the hit.") /*new GUIContent(tex)*/))
//            {
//                EditorUtility.OpenWithDefaultApp("HitInfoHelp");
//            }
//            position.y += 32f;
//            if (GUI.Button(position, new GUIContent("Select Hit Info")))
//            {
//                Selection.activeGameObject = ((Collider2D)property.GetObjectValue()).gameObject;
//            }
//            AttributeTarget.SetHeight(property.GetParent(), 64f);
//        }
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return AttributeTarget.GetHeight(property.GetParent());
//    }



//}

