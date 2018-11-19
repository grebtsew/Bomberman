using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof (MinMaxSliderAttribute))]
class MinMaxSliderDrawer : PropertyDrawerGFPro {
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        bool _guiEnabled = GUI.enabled;
        MinMaxSliderAttribute attr = attribute as MinMaxSliderAttribute;
       
        if (CheckDifferent(property, attr.varToCheckHideDiferent, attr.value))
        {
            GUI.enabled = false;
        }

		if (property.propertyType == SerializedPropertyType.Vector2) {
			Vector2 range = property.vector2Value;
			float min = range.x;
			float max = range.y;

			EditorGUI.BeginChangeCheck ();
			EditorGUI.MinMaxSlider (position, label, ref min, ref max, attr.min, attr.max);
			if (EditorGUI.EndChangeCheck ()) {
				range.x = min;
				range.y = max;
				property.vector2Value = range;
			}
		} else {
			EditorGUI.LabelField (position, label, "Use only with Vector2");
		}

        GUI.enabled = _guiEnabled;
    }
}
