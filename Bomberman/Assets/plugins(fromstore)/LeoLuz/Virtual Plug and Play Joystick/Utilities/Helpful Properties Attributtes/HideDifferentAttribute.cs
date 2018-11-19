using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class HideDifferent : Hidesbase
{
    public string varName;
    public object value;
    public bool skipLine;
    public bool readOnly;
    public bool hideLabel;
    public int DisplaceLevel;
    public bool HidedOnLastFrame;
    public float labelWidth;
    public float valueWidth;
    public bool withMold;


    public HideDifferent(string varToCheck, object ValueToCheck, bool skipLine = false, bool readOnly = false, bool hideLabel=false)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.skipLine = skipLine;
        this.readOnly = readOnly;
        this.hideLabel = hideLabel;


    }

    public HideDifferent(string varToCheck, object ValueToCheck, bool withMold)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.withMold = withMold;

    }
    public HideDifferent(string varToCheck, object ValueToCheck, float ExtendLabel, float valueWidth, bool skipLine = false, bool readOnly = false, bool hideLabel = false)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.skipLine = skipLine;
        this.readOnly = readOnly;
        this.hideLabel = hideLabel;
        this.labelWidth = ExtendLabel;
        this.valueWidth = valueWidth;
    }

    public HideDifferent(string varToCheck, object ValueToCheck, int DisplaceLevel, bool skipLine = false, bool readOnly = false, bool hideLabel = false)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.skipLine = skipLine;
        this.readOnly = readOnly;
        this.hideLabel = hideLabel;
        this.DisplaceLevel = DisplaceLevel;
    }

}
