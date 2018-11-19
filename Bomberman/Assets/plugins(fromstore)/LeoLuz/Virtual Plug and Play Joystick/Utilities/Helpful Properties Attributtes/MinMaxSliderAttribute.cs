using System;
using UnityEngine;

public class MinMaxSliderAttribute : PropertyAttribute {

	public readonly float max;
	public readonly float min;
    public readonly string varToCheckHideDiferent;
    public readonly object value;

    public MinMaxSliderAttribute (float min, float max, string varToCheckHideDiferent = "", object value = null) {
		this.min = min;
		this.max = max;
        this.varToCheckHideDiferent = varToCheckHideDiferent;
        this.value = value;

    }
}
