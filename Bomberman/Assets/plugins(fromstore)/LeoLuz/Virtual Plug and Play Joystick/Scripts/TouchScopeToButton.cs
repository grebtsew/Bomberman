using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericFunctionsPro;
using LeoLuz;
/// <summary>
/// Input Mobile Simple Tap Scope
/// </summary>
/// 
namespace TouchToPcInput
{
    public class TouchScopeToButton : MonoBehaviour
    {

        [LargeHeader("Simple Tap Scope")]
        [Tooltip("0f is lef, 1 is right, 0 is down, 1 is up")]
        public Vector2 StartScopeNormal;
        [Tooltip("0f is lef, 1 is right, 0 is down, 1 is up")]
        public Vector2 EndScopeNormal;
        [ReadOnly]
        public bool TouchDownOnLastFrame;
        [ReadOnly]
        public bool TouchStationaryOnLastFrame;
        [ReadOnly]
        public bool MoveTouchEndToNextFrame;
        [ReadOnly]
        public bool disableSlide;
        [ReadOnly]
        public Vector2 TouchBeganPosition;
        [ReadOnly]
        public Vector2 TouchMovedPosition;
        [ReadOnly]
        public Vector2 SwipeDirection;
        [ReadOnly]
        public string TouchBeganButton;
        public float StationaryDeadZone = 3;
        [InputAxesListDropdown]
        public string TouchBeganConvertTo;
        public string TouchStationaryButton;
        [InputAxesListDropdown]
        public string TouchStationaryConvertTo;
        public string TouchEndedButton;
        [InputAxesListDropdown]
        public string TouchEndedConvertTo;
        [InputAxesListDropdown]
        public string SlideAxisHorizontal = "Horizontal";
        [InputAxesListDropdown]
        public string SlideAxisVertical = "Vertical";
        public float PixelsPerAxisUnit = 50f;

        void Start()
        {

        }

        void Update()
        {
#if (!UNITY_ANDROID && !UNITY_IOS) || UNITY_EDITOR
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                TouchDownOnLastFrame = true;
                TouchBeganPosition = UnityEngine.Input.mousePosition;
                disableSlide = false;
            }
            else if (UnityEngine.Input.GetMouseButton(0))
            {
                TouchMovedPosition = UnityEngine.Input.mousePosition;
                SwipeDirection = TouchMovedPosition - TouchBeganPosition;

                if (SwipeDirection.magnitude < StationaryDeadZone)
                {
                    TouchStationaryOnLastFrame = true;
                    if (TouchDownOnLastFrame)
                    {
                        Input.PressButtonDownMobile(TouchBeganConvertTo);
                        //   print("Began");
                        TouchDownOnLastFrame = false;
                        disableSlide = true;
                    }
                    else
                    {
                        if (!disableSlide)
                        Input.PressButtonMobile(TouchStationaryConvertTo);
                        //  print("Stationary");
                    }
                }
                else
                {
                    Input.AxisUpdateMobileOld(SlideAxisHorizontal, SwipeDirection.x / PixelsPerAxisUnit);
                    Input.AxisUpdateMobileOld(SlideAxisVertical, SwipeDirection.y / PixelsPerAxisUnit);
                }
            }
            else
            {
                if (TouchStationaryOnLastFrame)
                {
                    if (TouchDownOnLastFrame)
                    {
                        TouchDownOnLastFrame = false;
                        Input.PressButtonUpMobile(TouchEndedConvertTo);
                    }
                    else
                    {
                        Input.PressButtonUpMobile(TouchEndedConvertTo);
                    }
                }
            }
#endif
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            if (UnityEngine.Input.touchCount>0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TouchDownOnLastFrame = true;
            TouchBeganPosition = UnityEngine.Input.GetTouch(0).position;
        }
        else if (UnityEngine.Input.touchCount > 0 && (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved || UnityEngine.Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            TouchMovedPosition = UnityEngine.Input.GetTouch(0).position;
            SwipeDirection = TouchMovedPosition - TouchBeganPosition;

            if (SwipeDirection.magnitude < StationaryDeadZone)
            {
                TouchStationaryOnLastFrame = true;
                if (TouchDownOnLastFrame)
                {
                    Input.PressButtonDownMobile(TouchBeganConvertTo);
                    //   print("Began");
                    TouchDownOnLastFrame = false;
                }
                else
                {
                    Input.PressButtonMobile(TouchStationaryConvertTo);
                    //  print("Stationary");
                }
            }
            else
            {
                Input.AxisUpdateMobileOld(SlideAxisHorizontal, SwipeDirection.x / PixelsPerAxisUnit);
                Input.AxisUpdateMobileOld(SlideAxisVertical, SwipeDirection.y / PixelsPerAxisUnit);
            }
        }
        else
        {
            if (TouchStationaryOnLastFrame)
            { 
                if (TouchDownOnLastFrame)
                {
                    TouchDownOnLastFrame = false;
                    Input.PressButtonUpMobile(TouchEndedConvertTo);
                    //     print("Began");
                }
                else
                {
                    Input.PressButtonUpMobile(TouchEndedConvertTo);
                    TouchStationaryOnLastFrame = false;
                    //     print("Ended");
                }
            }
        }
#endif
        }
    }
}