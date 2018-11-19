using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using GenericFunctionsPro;


public class ReflectMethods : Hidesbase {

    public string targetName;
    public bool ignoreParameters;

    public ReflectMethods(string target, bool GetParameters=false)
    {
        this.targetName = target;
        this.ignoreParameters = GetParameters;
    }

    public IDictionary<object, float> HeightList;

    public void SetHeight(object parent, float value)
    {
        //  Debug.Log("value" + value);
        if (HeightList == null)
            HeightList = new Dictionary<object, float>();
        if (HeightList.ContainsKey(parent))
            HeightList[parent] = value;
        else
            HeightList.Add(parent, value);
    }
    public float GetHeight(object parent)
    {
        if (HeightList == null)
            return 0f;

        if (parent != null && HeightList.ContainsKey(parent))
            return HeightList[parent];
        else
            return 0f;
    }
}



public class ReflectFields : Hidesbase
{

    public string AnalyzedObjectName;
    public string AnalizedSubClassName;

    public float space;
    public bool Hided;

    public IDictionary<object, float> HeightList;

    public ReflectFields(string InputObjectName, string InputSubClassName ="")
    {
        this.AnalyzedObjectName = InputObjectName;
        this.AnalizedSubClassName = InputSubClassName;
    }

    public void SetHeight(object parent, float value)
    {
      //  Debug.Log("value" + value);
        if (HeightList == null)
            HeightList = new Dictionary<object, float>();
        if (HeightList.ContainsKey(parent))
            HeightList[parent] = value;
        else
            HeightList.Add(parent, value);
    }
    public float GetHeight(object parent)
    {
        if (HeightList == null)
            return 0f;

        if (parent!= null && HeightList.ContainsKey(parent))
            return HeightList[parent] + 3f;
        else
            return 0f;
    }
}

public class ReflectEnum : Hidesbase
{

    public string AnalyzedObjectName;
    public ReflectEnum(string InputObject)
    {
        this.AnalyzedObjectName = InputObject;
    }
}

public class GenericConditionCustomInspector : PropertyAttribute
{
    public bool Hided;
    public float height;
    public bool fold;
    public GenericConditionCustomInspector()
    {

    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReflectMethods))]
public class ReflectMethodsDrawer : PropertyDrawerGFPro
{

    ReflectMethods target { get { return ((ReflectMethods)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {


        object parent = property.GetParent();


        if (parent == null)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        FieldInfo fieldInfoOfClass = parent.GetType().GetField(target.targetName);

        object classObtained = fieldInfoOfClass.GetValue(parent);

        if (classObtained == null)
        {
            target.SetHeight(property.GetParent(), -EditorGUIUtility.standardVerticalSpacing);
            return;
        }

        UnityEngine.Object convertedClassObtained = classObtained as UnityEngine.Object;
        if (convertedClassObtained == null)
        {
            target.SetHeight(property.GetParent(), -EditorGUIUtility.standardVerticalSpacing);
            return;
        }
        target.SetHeight(property.GetParent(), 16f);


        // Debug.Log(classObtained.GetType().ToString());
        MethodInfo[] methods = classObtained.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
        MethodInfo[] monoBehavMethods = typeof(MonoBehaviour).GetMethods(BindingFlags.Instance | BindingFlags.Public);
        List<string> methodList = new List<string>();

        int IgnoreLastMethods = monoBehavMethods.Length;
        for (int i = 0; i < methods.Length - IgnoreLastMethods; i++)
        {
            if (!target.ignoreParameters)
            {
                ParameterInfo[] ParameterInfo = methods[i].GetParameters();
                if(ParameterInfo.Length==0)
                {
                    object[] atrr = methods[i].GetCustomAttributes(typeof(HideMethod), false);
                    if (atrr.Length == 0)
                    {
                        methodList.Add(methods[i].Name);
                    }
                }
            }
            else
            {
                object[] atrr = methods[i].GetCustomAttributes(typeof(HideMethod),false);
                if (atrr.Length == 0)
                {
                    methodList.Add(methods[i].Name);
                }
            }

        }

        int choiceIndex = methodList.Contains(property.stringValue)? methodList.IndexOf(property.stringValue) : 0;

        //GetParameter List




        choiceIndex = EditorGUI.Popup(position, property.name, choiceIndex, methodList.ToArray());
        if (choiceIndex == -1)
            choiceIndex = 0;

        if (choiceIndex < methodList.Count())
        {
            property.stringValue = methodList[choiceIndex];
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //Debug.Log(attributeTarget.GetHeight(property.GetParent()));
            return target.GetHeight(property.GetParent());
    }

}
public class HideMethod: Attribute
{

}

[CustomPropertyDrawer(typeof(ReflectFields))]
public class ReflectFieldsDrawer : HidesBaseDrawer
{
    ReflectFields attributeTarget { get { return ((ReflectFields)attribute); } }
    int Depth = 0;
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		Depth = 0;

		if (property.arraySize < 10) {
			property.arraySize = 10;
			return;
		}


        //! Get parent object
        Debug.Log(0);
        object parent = property.GetParent();
        if (parent == null)
        {
            EditorGUI.PropertyField(position, property, label, true);
            return;
        }
		Debug.Log(1);
        //! Get Analyzed object
        FieldInfo AnalyzedObjectField = parent.GetType().GetField(attributeTarget.AnalyzedObjectName);
        if (AnalyzedObjectField == null)
        {
            return;
        }
        object AnalyzedObject = AnalyzedObjectField.GetValue(parent);
		Debug.Log(2);
        //! check check if analyzed object is not null, then draw de field popup (obs: this strange method was used because unity considers empty as UnityEngine.Object, then verification clausules is not the same as common object.
        UnityEngine.Object convertedAnalyzedObject = AnalyzedObject as UnityEngine.Object;
        if (convertedAnalyzedObject != null)
        {
			Debug.Log(3);

            //! Get field info of property, because SerializedProperty property do not work with recursive cadence of fields

            DrawRecursiveMembersDropdown(position, property, AnalyzedObject, parent);
        }
        else
        {
            attributeTarget.hide(parent, true);
        }

        attributeTarget.SetHeight(parent, ((float)Depth) * 16f);
    }

    void DrawRecursiveMembersDropdown(Rect position, SerializedProperty main, /*FieldInfo property, */object AnalyzedObject, object RootParent)
    {
    	//Get prop
		SerializedProperty arrayElement = main.GetArrayElementAtIndex(Depth);
//        if (AnalyzedObject == null)
//        {
//            attributeTarget.hide(RootParent, true);
//            return;
//        }
        attributeTarget.hide(RootParent, false);
        //! Get Fields list to popup
        FieldInfo[] fields = AnalyzedObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
        FieldInfo[] monoBehavFields = typeof(MonoBehaviour).GetFields(BindingFlags.Instance | BindingFlags.Public);
        var RawFieldList = new List<FieldInfo>();
        var fieldListDetailed = new List<string>();
        int IgnoreLastFields = monoBehavFields.Length;
        IgnoreLastFields = 0;
        //! Create list of fields, ignoring monobehaviour fields
        for (int i = 0; i < fields.Length - IgnoreLastFields; i++)
        {
            if (fields[i].FieldType.IsArray || fields[i].FieldType.IsGenericType || (fields[i].FieldType.IsClass && !fields[i].FieldType.IsSerializable))
                continue;

                RawFieldList.Add(fields[i]);

                string typepath = fields[i].FieldType.ToString();
                string[] type = typepath.Split('.');
                //! Format list on popUp
                fieldListDetailed.Add(fields[i].Name + ": " + type[type.Length - 1]);
        }

        //! Get previous variable name changed in popup
		string oldValue = arrayElement.stringValue;
        int choiceIndex = 0;
        for (int i = 0; i < RawFieldList.Count; i++)
        {
            if (oldValue == RawFieldList[i].Name)
            {
                choiceIndex = i;
                break;
            }
        }

        //! SHOW POP UP
        position.height = 18f;
		choiceIndex = EditorGUI.Popup(position, "", choiceIndex, fieldListDetailed.ToArray());
        Depth++; //this will make that inspector identify Spacing

        //! Apply changes
        if (choiceIndex == -1)
            choiceIndex = 0;
        if (choiceIndex > -1 && choiceIndex < RawFieldList.Count())
        {
			arrayElement.stringValue=RawFieldList[choiceIndex].Name;
          //  property.SetValue(RootParent, RawFieldList[choiceIndex].Name);
        }

        //! store variable type
        FieldInfo varTypeOfTarget = RootParent.GetType().GetField("varType");

        if (varTypeOfTarget != null && RawFieldList.Count > 0 && choiceIndex>-1)
        {
            varTypeOfTarget.SetValue(RootParent, RawFieldList[choiceIndex].FieldType);

            if(RawFieldList[choiceIndex].FieldType.IsClass && RawFieldList[choiceIndex].FieldType != typeof(string))
            {
              //  Debug.Log("IsClass: " + fields[choiceIndex].FieldType);
                position.y += position.height;
                object newAnalyzedObject = RawFieldList[choiceIndex].GetValue(AnalyzedObject);

                    DrawRecursiveMembersDropdown(position, main, newAnalyzedObject, RootParent);

            } else
            {
                //Clean SubFields (this has made because is necessary identify if subfields were used)
                main.GetArrayElementAtIndex(Depth).stringValue="";
//                FieldInfo NewProperty = RootParent.GetType().GetField("Sub" + property.Name);
//                if (NewProperty != null)
//                {
//                    NewProperty.SetValue(RootParent, "");
//                }
            }
        }    
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //Debug.Log(attributeTarget.GetHeight(property.GetParent()));
        if (attributeTarget.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return attributeTarget.GetHeight(property.GetParent());
    }
}


[CustomPropertyDrawer(typeof(ReflectEnum))]
public class ReflectEnumDrawer : HidesBaseDrawer
{

    ReflectEnum attributeTarget { get { return ((ReflectEnum)attribute); } }
    public enum eitaKct { oioi, aiai, eiei }
    public eitaKct EitaKct;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //! Get parent object
        object parent = property.GetParent();
        if (parent == null)
        {
            EditorGUI.PropertyField(position, property, label, true);
            return;
        }

        //! Get Analyzed object
        FieldInfo AnalyzedObjectField = parent.GetType().GetField(attributeTarget.AnalyzedObjectName);

        if (AnalyzedObjectField == null)
        {
            Debug.LogError("Field not found in ReflectEnumDrawer");
            attributeTarget.hide(parent, true); //set to hide
        }

        object AnalyzedObject = AnalyzedObjectField.GetValue(parent);

        //Verify if field type is not enum then hide enumPopup and abort operation.
        if (AnalyzedObject == null || !((System.Type)AnalyzedObject).IsEnum)
        {
            attributeTarget.hide(parent, true); //set to hide
            return;
        }

        attributeTarget.hide(parent, false);
        //Getting enumerator members of enum
        Array EnumArray = System.Enum.GetValues((System.Type)AnalyzedObject);
        string[] displayedOptionsList = new string[EnumArray.Length];
        //! Get previous variable name changed in popup
        int choiceIndex = -1;
        for (int i = 0; i < EnumArray.Length; i++)
        {
            displayedOptionsList[i] = EnumArray.GetValue(i).ToString();
            if (displayedOptionsList[i] == property.stringValue)
            {
                choiceIndex = i;
            }
           // Debug.Log("Values: " + EnumArray.GetValue(i).GetType());
        }

        //! SHOW POP UP
        position.height = 18f;
        choiceIndex = EditorGUI.Popup(position, property.name, choiceIndex, displayedOptionsList);

        // SAVE ENUM VALUE
        if (choiceIndex > -1 && choiceIndex < displayedOptionsList.Length)
            property.stringValue = displayedOptionsList[choiceIndex];
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (attributeTarget.CheckHided(property.GetParent()))
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return EditorGUI.GetPropertyHeight(property, label);
    }

}

[CustomPropertyDrawer(typeof(GenericConditionCustomInspector))]
public class GenericConditionCustomInspectorDrawer : HidesBaseDrawer
{

    GenericConditionCustomInspector attributeTarget { get { return ((GenericConditionCustomInspector)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        attributeTarget.fold = EditorGUI.Foldout(position, attributeTarget.fold, "Condition");
        if (attributeTarget.fold)
        {

            //SerializedProperty VariableNameToCheck = property.FindPropertyRelative("VariableNameToCheck");
            //SerializedProperty ObjectToCheckCondition = property.FindPropertyRelative("ObjectToCheckCondition");
            //SerializedProperty varType = property.FindPropertyRelative("varType");
            //SerializedProperty _boolean = property.FindPropertyRelative("_boolean");
            //SerializedProperty Enum = property.FindPropertyRelative("Enum");
            //SerializedProperty _String = property.FindPropertyRelative("_String");
            //SerializedProperty _Float = property.FindPropertyRelative("_Float");
            //SerializedProperty _int = property.FindPropertyRelative("_int");
            //position.y += 20f;
            //VariableNameToCheck.EditorGUI.PropertyField(position, VariableNameToCheck, true);
            //position.y += 20f;
            //EditorGUI.PropertyField(position, ObjectToCheckCondition, true);

        }
        attributeTarget.height = 100f;

    }
public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //  Debug.Log(property.name+" AttributeTarget.Hided: " + AttributeTarget.Hided);

        if (attributeTarget.Hided)
            return -EditorGUIUtility.standardVerticalSpacing;
        else
            return attributeTarget.height;
    }

}
#endif

