using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{

    private static FloatingText popupText;
    private static GameObject canvas;


    public static void Initialize()
    {

        canvas = GameObject.Find("GameCanvas");
        if (!popupText)
        {
            popupText = Resources.Load<FloatingText>("PopupTexts/PopupTextParent");
        }

    }

    public static void CreateFloatingText(string text, Transform location, Color c)
    {

        Initialize();

        FloatingText instance = Instantiate(popupText);

        instance.setText(text, c);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = location.position;

    }



}
