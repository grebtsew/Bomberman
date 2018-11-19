using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// Dynamic Inspector GUI Layout
/// </summary>
public class DGUI : MonoBehaviour {

	public static bool Button (float width, float height, string text, float offsetX=0,float offsetY=0)
	{
		Rect button1=GUILayoutUtility.GetRect(width, height);
        button1.x += offsetX;
        button1.y += offsetY;

        if (GUI.Button (button1,text)) {
			return true;
		}
		return false;
	}
    public static bool Button(float width, float height, GUIContent text, float offsetX = 0, float offsetY = 0)
    {
        Rect button1 = GUILayoutUtility.GetRect(width, height);
        button1.x += offsetX;
        button1.y += offsetY;

        if (GUI.Button(button1, text))
        {
            return true;
        }
        return false;
    }
    public static void Space(float height)
    {
        EditorGUILayout.GetControlRect(false, height);
    }
    public static bool Toogle(SerializedProperty prop, float minLabelWidth=100f, float minValueWidth = 22f)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 16f);
        float fullwidth = rect.width;
        rect.width = minLabelWidth;
        EditorGUI.LabelField(rect, prop.displayName);
        rect.x += rect.width;
        rect.width = fullwidth - rect.width;
        if (rect.width < 22f)
        {
            rect.x -= 22f - rect.width;
            rect.width = 22f;
        }
        prop.boolValue = EditorGUI.Toggle(rect, prop.boolValue);
        return prop.boolValue;
    }
    public static string TextField(string label, string value, float labelwidth = 100f, float minValueWidth = 22f)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 16f);
        float fullwidth = rect.width;
        rect.width = labelwidth;
        EditorGUI.LabelField(rect, label);
        rect.x += rect.width;
        rect.width = fullwidth - rect.width;
        if (rect.width < minValueWidth)
        {
            rect.x -= minValueWidth - rect.width;
            rect.width = minValueWidth;
        }
        value = EditorGUI.TextField(rect, value);
        return value;
    }

    public static bool PropertyField(SerializedProperty property, float minLabelwidth = 100f, float minValueWidth=22f)
    {
        if (property.hasVisibleChildren)
        {
              return EditorGUILayout.PropertyField(property);
        }
        else
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 16f);
            float fullwidth = rect.width;
            rect.width = minLabelwidth;
            if (rect.width> fullwidth)
            {
                rect.width= fullwidth- minValueWidth;
            }
            GUI.skin.label.clipping=TextClipping.Clip;
            GUI.skin.label.wordWrap = false;
            EditorGUI.LabelField(rect, property.displayName, GUI.skin.label);
            rect.x += rect.width;
            rect.width = fullwidth - rect.width;
            //if (rect.width < minValueWidth)
            //{
            //    rect.x -= minValueWidth - rect.width;
            //    rect.width = minValueWidth;
            //}
            return EditorGUI.PropertyField(rect, property, new GUIContent(), false);
        }
    }
    //static ComboMasterInterfaceConfig.DepthTheme defaultTheme;
    //public static void BeginVerticalStylized(string themeName="")
    //{
    //    if(defaultTheme==null)
    //    {
    //        if (SerializedPropertyExtensions.InterfaceThemeConfig == null)
    //        {
    //            SerializedPropertyExtensions.InterfaceThemeConfig = FileUtility.LoadFile(SerializedPropertyExtensions.ThemeFileName) as ComboMasterInterfaceConfig;
    //        }
    //        defaultTheme = themeName == "" ? SerializedPropertyExtensions.InterfaceThemeConfig.CustomStyles[0] : SerializedPropertyExtensions.InterfaceThemeConfig.GetCustomStyle(themeName);
    //    }
    //    EditorGUILayout.BeginVertical(defaultTheme.WindowOpennedHeaderStyle);

    //}
    public static void EndVerticalStylized()
    {
        EditorGUILayout.EndVertical();
    }

    public static bool Button (float width, float height, string text, GUIStyle style)
	{
		Rect button1=GUILayoutUtility.GetRect(width, height);
		if (GUI.Button (button1,text,style)) {
			return true;
		}
		return false;
	}
	public static bool Button (string text)
	{
		Rect button1=EditorGUILayout.GetControlRect(false,17f);
		if (GUI.Button (button1, text)) {
			return true;
		}
		return false;
	}
    public static void ArrayIncreaseButton(SerializedProperty prop, float height=32f, float width=32f)
    {
        Rect buttons = EditorGUILayout.GetControlRect(false, height);
        //   buttons.height = 32f;
        //  buttons.y -= 38f;
        buttons.x += buttons.width * 0.5f - width;
        buttons.width = width;

        if (GUI.Button(buttons, "+"))
        {
            prop.arraySize += 1;
        }
        buttons.x += 32f;
        if (GUI.Button(buttons, "-") && prop.arraySize>0)
        {
            prop.arraySize -= 1;
        }
    }
	public static bool Button (float height, string text)
	{
		Rect button1=EditorGUILayout.GetControlRect(true,height);
		if (GUI.Button (button1, text)) {
			return true;
		}
		return false;
	}

	public static void BeginHorizontalCenter() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
	}
	public static void EndHorizontalCenter() {

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
	}
	public static void BeginVerticalCenter() {
		EditorGUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
	}
	public static void EndVerticalCenter() {

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndVertical();
	}
	public static System.Enum EnumPopup (Rect rect, System.Enum enumerator, string[] minus)
	{		
		System.Array optionsEnums=System.Enum.GetValues(enumerator.GetType());
		string[] options = System.Enum.GetNames (enumerator.GetType());
		string selectedName=System.Enum.GetName(enumerator.GetType(),enumerator);
		List<string> OriginalListOptions = ArrayUtility.ArrayToList (options);
		List<string> FilteredListOptions = ArrayUtility.ArrayToList (options);
		for (int i = 0; i < minus.Length; i++) {
			if(FilteredListOptions.Contains(minus[i]))
				FilteredListOptions.Remove(minus[i]);
		}
		int selectedIndex=FilteredListOptions.IndexOf(selectedName);

		selectedIndex=EditorGUI.Popup(rect,selectedIndex,FilteredListOptions.ToArray());
		if(selectedIndex<0)
			selectedIndex=0;
		selectedName=FilteredListOptions[selectedIndex];
		int nonfilteredSelectedIndex=OriginalListOptions.IndexOf(selectedName);
		return (System.Enum)(optionsEnums.GetValue(nonfilteredSelectedIndex));
	}
    public static int EnumPopupIndex(Rect rect, System.Enum enumerator, string[] minus)
    {
        string[] options = System.Enum.GetNames(enumerator.GetType());
        string selectedName = System.Enum.GetName(enumerator.GetType(), enumerator);
        List<string> OriginalListOptions = ArrayUtility.ArrayToList(options);
        List<string> FilteredListOptions = ArrayUtility.ArrayToList(options);
        for (int i = 0; i < minus.Length; i++)
        {
            if (FilteredListOptions.Contains(minus[i]))
                FilteredListOptions.Remove(minus[i]);
        }
        int selectedIndex = FilteredListOptions.IndexOf(selectedName);

        selectedIndex = EditorGUI.Popup(rect, selectedIndex, FilteredListOptions.ToArray());
        if (selectedIndex < 0)
            selectedIndex = 0;
        selectedName = FilteredListOptions[selectedIndex];
        int nonfilteredSelectedIndex = OriginalListOptions.IndexOf(selectedName);
        return nonfilteredSelectedIndex;
    }
    public static System.Enum EnumPopup(Rect rect, string label, System.Enum enumerator, string[] minus)
    {
        System.Array optionsEnums = System.Enum.GetValues(enumerator.GetType());
        string[] options = System.Enum.GetNames(enumerator.GetType());
        string selectedName = System.Enum.GetName(enumerator.GetType(), enumerator);
        List<string> OriginalListOptions = ArrayUtility.ArrayToList(options);
        List<string> FilteredListOptions = ArrayUtility.ArrayToList(options);
        for (int i = 0; i < minus.Length; i++)
        {
            if (FilteredListOptions.Contains(minus[i]))
                FilteredListOptions.Remove(minus[i]);
        }
        int selectedIndex = FilteredListOptions.IndexOf(selectedName);

        selectedIndex = EditorGUI.Popup(rect, label, selectedIndex, FilteredListOptions.ToArray());
        if (selectedIndex < 0)
            selectedIndex = 0;
        selectedName = FilteredListOptions[selectedIndex];
        int nonfilteredSelectedIndex = OriginalListOptions.IndexOf(selectedName);
        return (System.Enum)(optionsEnums.GetValue(nonfilteredSelectedIndex));
    }
}
