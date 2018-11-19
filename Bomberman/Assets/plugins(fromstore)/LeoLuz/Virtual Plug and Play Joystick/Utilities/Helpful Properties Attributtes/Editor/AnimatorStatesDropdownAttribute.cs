using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericFunctionsPro;
using UnityEditor.Animations;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
using System;
#endif

public class AnimatorStatesDropdownAttribute : PropertyAttribute
{
    public string _animatorVariableName;
    public int layer;

	public AnimatorStatesDropdownAttribute(string animatorVariableName, int layer = 0)
    {
        this._animatorVariableName = animatorVariableName;
        this.layer = layer;
    }
    public AnimatorStatesDropdownAttribute(int layer = 0)
    {
        this._animatorVariableName = "";
        this.layer = layer;
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(AnimatorStatesDropdownAttribute))]
    public class AnimatorStatesDropDownDrawer : PropertyDrawerGFPro
    {

        AnimatorStatesDropdownAttribute target { get { return ((AnimatorStatesDropdownAttribute)attribute); } }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get Animator
            Animator animator = null;
            string initialValue = property.stringValue;
            //    SerializedProperty test = new SerializedProperty();

            if (target._animatorVariableName == "")
            {
                MonoBehaviour targetgameObject = (MonoBehaviour)property.serializedObject.targetObject;
                animator = targetgameObject.gameObject.GetComponentInChildren<Animator>();
            }
            else
            {
                object parent = property.GetParent();
                if (parent == null)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                    //      EditorGUIUtility.
                    return;
                }

                FieldInfo AnimatorField = parent.GetType().GetField(target._animatorVariableName);
                if (AnimatorField != null)
                {
                    animator = (Animator)AnimatorField.GetValue(parent);
                }

            }

            if (animator != null)
            {
                List<string> stateNames = AnimatorUtility.GetStatesNames(animator, target.layer);


                int choiceIndex = stateNames.IndexOf(property.stringValue);
                if (choiceIndex == -1 && property.stringValue != "")
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
                else
                {
                    stateNames.Add("Custom");
                    choiceIndex = EditorGUI.Popup(position, label.text, choiceIndex, stateNames.ToArray());
                    if (choiceIndex == -1)
                        choiceIndex = 0;

                    property.stringValue = stateNames[choiceIndex];

                    if (initialValue != property.stringValue)
                    {
                        object parent = property.GetParent();
                        //IEnumerator asda = property.GetEnumerator();
                        FieldInfo durationField = parent.GetType().GetField("Duration");
                        if(durationField!=null)
                        {
                            AnimatorState animState = AnimatorUtility.GetState(animator, property.stringValue, 0);
                            if (animState == null)
                                return;

                            Motion AnimMotion = animState.motion;
                            AnimationClip clip = (AnimationClip)AnimMotion;
                            durationField.SetValue(parent, clip.length);
                        }
                    }
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }
        }

    }
#endif
}


