using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RangeHideIfDifferent : Hidesbase
{
    public string varName;
    public object ValueToCheck;
    public float min;
    public float max;
    public bool upperLabel;

    public RangeHideIfDifferent(float min, float max, string varToCheck, object ValueToCheck, bool upperLabel=false)
    {
        this.varName = varToCheck;
        this.ValueToCheck = ValueToCheck;
        this.min = min;
        this.max = max;
        this.upperLabel = upperLabel;
    }
}
