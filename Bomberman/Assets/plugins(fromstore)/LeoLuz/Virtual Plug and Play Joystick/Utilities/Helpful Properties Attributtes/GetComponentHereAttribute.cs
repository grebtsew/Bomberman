using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetComponentHereAttribute : PropertyAttribute {

    public bool force;
    public float valueWidth;
    public float labelWidth;
    public GetComponentHereAttribute(bool force = false)
    {
        this.force = force;
    }
    public GetComponentHereAttribute(bool force, float labelWidth, float valueWidth=0f)
    {
        this.force = force;
        this.labelWidth = labelWidth;
        this.valueWidth = valueWidth;
    }
}

