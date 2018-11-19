using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HideIfAnyIsDifferent : Hidesbase
{
    public string varName;
    public string varName2;
    public object value;
    public object value2;
    public bool skipLine;
    public bool readOnly;
    public bool hideLabel;
    public enum teste {  oi, eita }

    public HideIfAnyIsDifferent(string varToCheck, object ValueToCheck, string secondVarToCheck, object SecondValueToCheck, bool skipLine = false, bool readOnly = false, bool hideLabel = false)
    {
        this.varName = varToCheck;
        this.varName2 = secondVarToCheck;
        this.value = ValueToCheck;
        this.value2 = SecondValueToCheck;
        this.skipLine = skipLine;
        this.readOnly = readOnly;
        this.hideLabel = hideLabel;
    }
}

