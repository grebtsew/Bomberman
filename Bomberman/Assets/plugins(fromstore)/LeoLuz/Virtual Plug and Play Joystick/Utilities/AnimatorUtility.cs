#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimatorUtility : MonoBehaviour {

    public static List<string> GetStatesNames(Animator animator, int layer = -1)
    {
        UnityEditor.Animations.AnimatorController animatorInternal = (UnityEditor.Animations.AnimatorController)animator.runtimeAnimatorController;

        List<string> AnimStatesName=new List<string>();

        if (animatorInternal != null)
        {
            AnimatorControllerLayer[] layers = animatorInternal.layers;
            if (layer == -1)
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    AnimatorStateMachine _stateMachine = layers[i].stateMachine;

                    ChildAnimatorState[] sates = _stateMachine.states;
                    for (int ib = 0; ib < sates.Length; ib++)
                    {
                        AnimStatesName.Add(sates[ib].state.name);
                    }
                }
            } else
            {
                AnimatorStateMachine _stateMachine = layers[layer].stateMachine;
                ChildAnimatorState[] sates = _stateMachine.states;
                for (int ib = 0; ib < sates.Length; ib++)
                {
                    AnimStatesName.Add(sates[ib].state.name);
                }
            }
            return AnimStatesName;
        }
        return null;
    }

    public static AnimatorStateMachine GetStateMachine(Animator animator, int layerId = 0)
    {
        UnityEditor.Animations.AnimatorController animatorInternal = (UnityEditor.Animations.AnimatorController)animator.runtimeAnimatorController;

        if (animatorInternal != null)
        {
            AnimatorControllerLayer[] layers = animatorInternal.layers;
            return layers[layerId].stateMachine;
        }
        return null;
    }

    public static AnimatorState AddState(string sateName, AnimationClip Motion,  Animator animator, int layerId = 0)
    {
        UnityEditor.Animations.AnimatorController animatorInternal = (UnityEditor.Animations.AnimatorController)animator.runtimeAnimatorController;

        if (animatorInternal != null)
        {
            AnimatorControllerLayer[] layers = animatorInternal.layers;
            UnityEditor.Animations.AnimatorState newState = layers[layerId].stateMachine.AddState(sateName);
            newState.motion = Motion;
            return newState;
        }
        return null;
    }

    public static AnimatorState[] GetStates(Animator animator, int layer = 0)
    {
        ChildAnimatorState[] childStates = ((UnityEditor.Animations.AnimatorController)animator.runtimeAnimatorController).layers[layer].stateMachine.states;
        UnityEditor.Animations.AnimatorState[] animatorStates = new UnityEditor.Animations.AnimatorState[childStates.Length];

        for (int i = 0; i < childStates.Length; i++)
        {
            animatorStates[i] = childStates[i].state;
        }
        return animatorStates;
    }

    public static AnimatorState GetState(Animator animator, string stateName, int layer = 0)
    {
        ChildAnimatorState[] childStates = ((UnityEditor.Animations.AnimatorController)animator.runtimeAnimatorController).layers[layer].stateMachine.states;

        for (int i = 0; i < childStates.Length; i++)
        {
            if(childStates[i].state.name==stateName)
            {
                UnityEditor.Animations.AnimatorState tes = childStates[i].state;
                return childStates[i].state;
            }
        }
        return null;
    }
}
#endif