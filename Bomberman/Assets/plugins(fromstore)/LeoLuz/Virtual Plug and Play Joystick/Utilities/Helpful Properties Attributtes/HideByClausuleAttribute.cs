using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HideByClausule : Hidesbase
{
    public string varName;
    public string varName2;
    public object value;
    public object value2;
    public bool skipLine;
    public bool readOnly;
    public bool hideLabel;
    public Clausule FirstClausule;
    public Clausule SecondClausule;
    public Operator _Operator;
    public enum Clausule { equal, different}
    public enum Operator { or, and }

    public HideByClausule(string varToCheck, Clausule FirstClausule, object ValueToCheck, Operator _Operator, string secondVarToCheck, Clausule SecondClausule, object SecondValueToCheck, bool skipLine = false, bool readOnly = false, bool hideLabel = false)
    {
        this.varName = varToCheck;
        this.varName2 = secondVarToCheck;
        this.value = ValueToCheck;
        this.value2 = SecondValueToCheck;
        this.skipLine = skipLine;
        this.readOnly = readOnly;
        this.hideLabel = hideLabel;
        this.FirstClausule = FirstClausule;
        this.SecondClausule = SecondClausule;
        this._Operator = _Operator;
    }
}
