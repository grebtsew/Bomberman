using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
using UnityEditor.Animations;
#endif

public class GetAnimationLegthAttribute : PropertyAttribute
{
    public string AnimatorVariableName;
    public int layer;
    public string variableNameWithStateName;
    public bool readOnly;
    public string cacheFieldName;

    public GetAnimationLegthAttribute(string variableNameWithStateName,string animatorVariableName = "", int layer=0, bool force = false)
    {
        this.variableNameWithStateName = variableNameWithStateName;
        this.AnimatorVariableName = animatorVariableName;
        this.layer = layer;
        this.readOnly= force;
    }
                                                                            //OBS: force make that value will be changed always. Without it, you shoud erase string to update length
    public GetAnimationLegthAttribute(string variableNameWithStateName, bool readOnly, string cacheFieldName="", string animatorVariableName = "", int layer = 0)
    {
        this.variableNameWithStateName = variableNameWithStateName;
        this.AnimatorVariableName = animatorVariableName;
        this.layer = layer;
        this.readOnly = readOnly;
        this.cacheFieldName = cacheFieldName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(GetAnimationLegthAttribute))]
public class AnimatorGetStateLengthDrawer : PropertyDrawerGFPro
{
    GetAnimationLegthAttribute target { get { return ((GetAnimationLegthAttribute)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object parent = property.GetParent();
        //Get Animator
        Animator animator = null;
        if (target.AnimatorVariableName == "")
        {
            MonoBehaviour targetgameObject = (MonoBehaviour)property.serializedObject.targetObject;
            animator = targetgameObject.gameObject.GetComponentInChildren<Animator>();
        }
        else
        {
            if (parent == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }
            //Get animator reference
            FieldInfo AnimatorField = parent.GetType().GetField(target.AnimatorVariableName);
            if (AnimatorField != null)
                animator = (Animator)AnimatorField.GetValue(parent);
        }

        if (animator != null && parent != null)
        {
            //Get string field
            FieldInfo stateNameField = parent.GetType().GetField(target.variableNameWithStateName);

            if (stateNameField == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }
            //Get String value
            object value = stateNameField.GetValue(parent);

            if (value == null)
                return;

            string stateName = value.ToString();
            if (stateName == "")
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            if (target.cacheFieldName != "")
            {
                FieldInfo cacheFieldNameInfo = parent.GetType().GetField(target.cacheFieldName);
                if (cacheFieldNameInfo != null)
                {
                    string cachedState = (string)cacheFieldNameInfo.GetValue(parent);
                    if (cachedState != stateName)
                    {
                        //Get animator state
                        AnimatorState animState = AnimatorUtility.GetState(animator, stateName, target.layer);
                        if (animState == null)
                        {
                            EditorGUI.PropertyField(position, property, label, true);
                            return;
                        }
                        //Get animator length
                        Motion AnimMotion = animState.motion;
                        AnimationClip clip = (AnimationClip)AnimMotion;
                        float Length = clip.length;
                        property.floatValue = Length;
                        cacheFieldNameInfo.SetValue(parent, stateName);
                    }
                }
            }

            if (target.readOnly)
            {
                GUI.enabled = false;
            }
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            Debug.Log("3b");
            Debug.Log("Animator not Found: varname: " + target.AnimatorVariableName);
            EditorGUI.PropertyField(position, property, label, true);
        }
        GUI.enabled = true;

    }
}
#endif