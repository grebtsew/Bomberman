using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class hideIf : Hidesbase {

    public string varName;
    public object value;
    public bool skipLine;
    public bool readOnly;
    public float labelWidth;
    public float valueWidth;
    public bool withMold;
    /// <summary>
    /// (string varToCheck, object ValueToCheck,drawNextInThisLine?, readOnly?)
    /// If skipline is true, inspector will discart the line and draw next field in same line
    /// </summary>
    public hideIf(string varToCheck, object ValueToCheck, bool drawNextInThisLine = true, bool readOnly = false, float labelWidth = 0, float valueWidth = 0)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.skipLine = drawNextInThisLine;
        this.readOnly = readOnly;
        this.labelWidth = labelWidth;
        this.valueWidth = valueWidth;
    }
    public hideIf(string varToCheck, object ValueToCheck, bool withMold)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.withMold = withMold;

    }
    public hideIf(string value, float labelWidth = 0, float valueWidth = 0)
    {
        this.value = value;
        this.labelWidth = labelWidth;
        this.valueWidth = valueWidth;
    }
    public hideIf(string varToCheck, object ValueToCheck, float labelWidth, float valueWidth, bool drawNextInThisLine = true, bool readOnly = false)
    {
        this.varName = varToCheck;
        this.value = ValueToCheck;
        this.skipLine = drawNextInThisLine;
        this.readOnly = readOnly;
        this.labelWidth = labelWidth;
        this.valueWidth = valueWidth;
    }
}

public class ResponsivePropertyAttribute : PropertyAttribute
{
    public bool LabelUseTwoLines;
    
}

public class Hidesbase : ResponsivePropertyAttribute
{
    public IDictionary<object, bool> hideList;
    public void hide(object prop, bool hided)
    {
        if (hideList == null)
            hideList = new Dictionary<object, bool>();

        if (hideList.ContainsKey(prop))
        {
            hideList[prop] = hided;
        }
        else
        {
            hideList.Add(prop, hided);
        }
    }

    public bool CheckHided(object parent)
    {
        if (parent == null)
            return false;

        if (hideList == null || !hideList.ContainsKey(parent))
        {
            return false;
        }
        return hideList[parent];
    }
}