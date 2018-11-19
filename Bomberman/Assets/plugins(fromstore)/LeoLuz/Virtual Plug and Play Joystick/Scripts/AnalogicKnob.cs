using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeoLuz
{
    [RequireComponent(typeof(Canvas))]
    public class AnalogicKnob : MonoBehaviour
    {
        [InputAxesListDropdown]
        public string HorizontalAxis = "Horizontal";
        [InputAxesListDropdown]
        public string VerticalAxis = "Vertical";
        public float _sensitivity = 60f;      
        public RectTransform AnalogicKnobObject;
        public RectTransform RootCanvas;
        [HideInInspector]
        public float XNormalScope = 0.5f;
        [HideInInspector]
        public float YNormalScope = 0.6f;

        private Vector2 StartPosition;
        private Vector2 CurrentKnobPosition;
        private Vector2 RawAxis;
        [ReadOnly]
        public Vector2 NormalizedAxis;

        private float ReturnSpeed = 10f;

        private Vector2 ScreenPixels;
        private Vector2 CanvasSize;
        private Vector2 ProportionPercent;
        private Vector2 AnalogicStartPosition;
        private Touch AnalogTouch;
        private bool Released = false;

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
                string scriptName = typeof(AnalogicKnob).Name;

                // Iterate through all scripts (Might be a better way to do this?)
                foreach (UnityEditor.MonoScript monoScript in UnityEditor.MonoImporter.GetAllRuntimeMonoScripts())
                {
                    // If found our script
                    if (monoScript.name == scriptName)
                    {
                        UnityEditor.MonoImporter.SetExecutionOrder(monoScript, -2000);
                    }
                }
                OrderOfScriptChanged = true;
            }
#endif
        }
        void Start()
        {
            Input.RegisterAxisMobile(VerticalAxis);
            Input.RegisterAxisMobile(HorizontalAxis);

            if(AnalogicKnobObject==null)
            {
                Debug.Log("Specify the object of the knob");
            }
            if(RootCanvas == null)
            {
                Debug.Log("Specify the object of the knob");
            } 

            RectTransform CanvasRect = RootCanvas.GetComponent<RectTransform>();

            AnalogicStartPosition = AnalogicKnobObject.anchoredPosition;
            ScreenPixels = new Vector2(Screen.width, Screen.height);
        
            CanvasSize = CanvasRect.sizeDelta;
            ProportionPercent = new Vector2(CanvasSize.x / ScreenPixels.x, CanvasSize.y / ScreenPixels.y);
            XNormalScope = ScreenPixels.x * XNormalScope;
            YNormalScope = ScreenPixels.y * YNormalScope;
        }
        Vector2 lastFrameNormalizedAxis;
        void Update()
        {
            lastFrameNormalizedAxis = NormalizedAxis;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

            //SIMULATED MOBILE VIRTUAL JOYSTICK KNOB ON EDITOR
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (UnityEngine.Input.mousePosition.x < XNormalScope && UnityEngine.Input.mousePosition.y < YNormalScope)
                {
                    Released = false;
                    StartPosition = UnityEngine.Input.mousePosition;
                    AnalogicKnobObject.anchoredPosition = UnityEngine.Input.mousePosition * ProportionPercent.y;
                }
            }
            else if (UnityEngine.Input.GetMouseButton(0))
            {
                if (UnityEngine.Input.mousePosition.x < XNormalScope && UnityEngine.Input.mousePosition.y < YNormalScope)
                {
                    CurrentKnobPosition = UnityEngine.Input.mousePosition;
                    RawAxis = CurrentKnobPosition - StartPosition;
                    NormalizedAxis = Vector2.ClampMagnitude(RawAxis / _sensitivity, 1f);
                    AnalogicKnobObject.anchoredPosition = UnityEngine.Input.mousePosition * ProportionPercent.y;
                }
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                Released = true;
                NormalizedAxis = new Vector2(0f, 0f);
            }
            if (Released == true)
            {
                AnalogicKnobObject.anchoredPosition = Vector2.Lerp(AnalogicKnobObject.anchoredPosition, AnalogicStartPosition, ReturnSpeed * Time.deltaTime);
            }

#endif
            //EFFETIVE MOBILE VIRTUAL JOYSTICK KNOB
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS || UNITY_WP_8 || UNITY_WP_8_1)
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.position.y < YNormalScope && touch.position.x < YNormalScope)
                    {
                        AnalogTouch = touch;
                    }
                }
                //verifica touchs
                if (AnalogTouch.phase == TouchPhase.Began)
                {
                    Released = false;
                    StartPosition = AnalogTouch.position;
                    AnalogicKnobObject.anchoredPosition = AnalogTouch.position * ProportionPercent.y;

                }
                else if (AnalogTouch.phase == TouchPhase.Moved)
                {
                    CurrentKnobPosition = AnalogTouch.position;
                    RawAxis = CurrentKnobPosition - StartPosition;
                    NormalizedAxis = Vector2.ClampMagnitude(RawAxis / 60f, 1f);
                    AnalogicKnobObject.anchoredPosition = AnalogTouch.position * ProportionPercent.y;
                }
                if (AnalogTouch.phase == TouchPhase.Ended)
                {
                    Released = true;
                    NormalizedAxis = new Vector2(0f, 0f);
                }
            }
            if (Released == true)
            {
                AnalogicKnobObject.anchoredPosition = Vector2.Lerp(AnalogicKnobObject.anchoredPosition, AnalogicStartPosition, ReturnSpeed * Time.deltaTime);
            }
#endif
            Input.SetAxisMobile(HorizontalAxis, NormalizedAxis.x);
            Input.SetAxisMobile(VerticalAxis, NormalizedAxis.y);


            if (Mathf.Abs(lastFrameNormalizedAxis.x) < 0.2f && NormalizedAxis.x != 0f)
            {
                Input.PressButtonDownMobile(HorizontalAxis);
            }
            if (Mathf.Abs(lastFrameNormalizedAxis.y) < 0.2f && NormalizedAxis.y != 0f)
            {
                Input.PressButtonDownMobile(VerticalAxis);
            }
        }
    }
}
