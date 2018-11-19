using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadOnlyIfDifferent : Hidesbase
{
    public string varName;
    public object ValueToCheck;
    public float labelWidth;
    public float valueWidth;

    public ReadOnlyIfDifferent(string varToCheck, object ValueToCheck, float labelWidth=-1f, float valueWidth=32f)
    {
        this.varName = varToCheck;
        this.ValueToCheck = ValueToCheck;
        this.labelWidth = labelWidth;
        this.valueWidth = valueWidth;
    }
    public ReadOnlyIfDifferent(string varToCheck, object ValueToCheck)
    {
        this.varName = varToCheck;
        this.ValueToCheck = ValueToCheck;
    }

}
