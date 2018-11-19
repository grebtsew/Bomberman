using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using LeoLuz;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LeoLuz
{
    [RequireComponent(typeof(Rect))]
    public class UIButtonToAxis : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [InputAxesListDropdown]
        public string AxisName="Horizontal";
        [Range(-1f, 1f)]
        public float Value;
        private float CurrentValue;
        private bool pressed;
#if UNITY_EDITOR
        private bool OrderOfScriptChanged;
#endif
        public void OnDrawGizmosSelected()
        {
            Input.Autoconfigure();
#if UNITY_EDITOR
            if (!OrderOfScriptChanged)
            {
                // Get the name of the script we want to change it's execution order
                string scriptName = typeof(UIButtonToAxis).Name;

                // Iterate through all scripts (Might be a better way to do this?)
                foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
                {
                    // If found our script
                    if (monoScript.name == scriptName)
                    {
                        MonoImporter.SetExecutionOrder(monoScript, -2000);
                    }
                }
                OrderOfScriptChanged = true;
            }
#endif
        }

        void Start()
        {
            Input.RegisterAxisMobile(AxisName);
        }

        public void FixedUpdate()
        {
            if (pressed)
            {
                Input.PressButtonMobile(AxisName);
                CurrentValue = Value;
                Input.SetAxisMobile(AxisName, CurrentValue);
            }
        }

        public virtual void OnPointerDown(PointerEventData data)
        {
            Input.PressButtonDownMobile(AxisName);
            CurrentValue = Value;
            Input.SetAxisMobile(AxisName, CurrentValue);
            pressed = true;
        }

        public virtual void OnPointerUp(PointerEventData data)
        {
            pressed = false;
            Input.PressButtonUpMobile(AxisName);
            CurrentValue = 0f;
            Input.SetAxisMobile(AxisName, CurrentValue);
        }
    }

}