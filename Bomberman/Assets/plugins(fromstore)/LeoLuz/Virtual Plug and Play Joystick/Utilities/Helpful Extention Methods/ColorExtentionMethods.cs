using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ExtensionMethods
{
    public static Color ToHex(this Color color, string Hex)
    {
        Color newColor;
        ColorUtility.TryParseHtmlString(Hex, out newColor);
        return newColor;
    }
}