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
    public class UIButtonToButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [InputAxesListDropdown]
        public string ButtonName;
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
                string scriptName = typeof(UIButtonToButton).Name;

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

        public void Update()
        {
            if (pressed)
                Input.PressButtonMobile(ButtonName);
        }
        public void FixedUpdate()
        {
            if (pressed)
                Input.PressButtonMobile(ButtonName);
        }
        public virtual void OnPointerDown(PointerEventData data)
        {
            // print("UI PointerEventData Button Pressed" + Time.time);
            Input.PressButtonDownMobile(ButtonName);
            pressed = true;
        }

        public virtual void OnPointerUp(PointerEventData data)
        {
            pressed = false;
            Input.PressButtonUpMobile(ButtonName);
        }

    }

}