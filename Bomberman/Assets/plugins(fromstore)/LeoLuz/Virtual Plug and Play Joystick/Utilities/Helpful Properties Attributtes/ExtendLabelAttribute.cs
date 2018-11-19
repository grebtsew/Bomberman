using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExtendLabelAttribute : Hidesbase
{
    public float size;
    public float valueWidth;
    public ExtendLabelAttribute(float size, float valueWidth=32f)
    {
        this.size = size;
        this.valueWidth = valueWidth;
    }
}
