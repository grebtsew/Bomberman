#if UNITY_EDITOR
using LeoLuz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
public static class SerializedPropertyExtensions
{
    /// <summary>
    /// Convert serialized property to object value
    /// </summary>
    public static object GetObjectValue(this SerializedProperty serializedProperty, object parent = null)
    {
        if (parent == null)
            parent = GetParent(serializedProperty);

        if (parent == null)
        {
            Debug.Log("No Parent in serializedProperty: " + serializedProperty.name);
            return null;
        }

        FieldInfo fieldToCheck = parent.GetType().GetField(serializedProperty.name);
        if (fieldToCheck == null)
        {
            Debug.Log("No Field in serializedProperty: " + serializedProperty.name);
            return null;
        }
        object obj = fieldToCheck.GetValue(parent);

        return obj;
    }
	public static void draw(this object obj)
    {
		Debug.Log(obj.ToString());
    }

    //Get parent regardless if is array parent or class parent.
    public static object GetParent(this SerializedProperty prop)
    {
        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements.Take(elements.Length - 1))
        {
            if (element.Contains("["))
            {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue(obj, elementName, index);
            }
            else
            {
                obj = GetValue(obj, element);
            }
        }
        return obj;
    }

    public static object GetValue(object source, string name)
    {
        if (source == null)
            return null;
        var type = source.GetType();
        var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (f == null)
        {
            var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p == null)
                return null;
            return p.GetValue(source, null);
        }
        return f.GetValue(source);
    }

    public static object GetValue(object source, string name, int index)
    {
        //if (source == null)
        //    return null;

        Array values = (Array)GetValue(source, name);
        if (values.Length == 0)
            return null;
        var enumerable = values as IEnumerable;
        if (enumerable == null)
            return null;
        //
        //       Debug.Log(enumerable.ToString());
        var enm = enumerable.GetEnumerator();
        //     Debug.Log(enm.ToString());
        if (index >= values.Length)
            return null;

        while (index-- >= 0)
            enm.MoveNext();
        return enm.Current;
    }
    public static List<SerializedProperty> GetChildren(this SerializedProperty serializedProperty)
    {
        SerializedProperty prop = serializedProperty.Copy();
        List<SerializedProperty> children = new List<SerializedProperty>();

        int depth = prop.depth;
        prop.NextVisible(true);        
        if (prop.depth > depth)
        {
            children.Add(prop.Copy());
        }
        while (prop.NextVisible(false))
        {
            if (prop.depth > depth)
            {
                children.Add(prop.Copy());
            }
            else
            {
                return children;
            }
        }
        return children;
    }

    public static void PrintChildList(this SerializedProperty serializedProperty)
    {
        var children = serializedProperty.GetChildren();
        string str="";
        for (int i = 0; i < children.Count; i++)
        {
            str += children[i].name + "\n";
        }
        Debug.Log(str);
    }

    public static void DrawChildrenLight(this SerializedProperty serializedProperty, int startAt = 0, int endAt = 999)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        if (prop.depth > depth && startAt == 0)
        {
            EditorGUI.PropertyField(EditorGUILayout.GetControlRect(false, 16f), prop, false);
        }
        int count = 0;
        while (prop.NextVisible(false))
        {
            count++;
            if (prop.depth > depth)
            {
                if (count >= startAt && count <= endAt)
                {
                    EditorGUI.PropertyField(EditorGUILayout.GetControlRect(false, 16f), prop, false);
                }
            }
            else
            {
                return;
            }
        }
    }
    //public static void GetSiblingProperty(this SerializedProperty serializedProperty, string name)
    //{
    //    SerializedProperty prop = serializedProperty.Copy();
    //    int depth = prop.depth;
    //    prop.NextVisible(true);
    //    if (prop.name > depth && startAt == 0)
    //    {
    //        EditorGUI.PropertyField(EditorGUILayout.GetControlRect(false, 16f), prop, false);
    //    }
    //    int count = 0;
    //    while (prop.NextVisible(false))
    //    {
    //        count++;
    //        if (prop.depth > depth)
    //        {
    //            if (count >= startAt && count <= endAt)
    //            {
    //                EditorGUI.PropertyField(EditorGUILayout.GetControlRect(false, 16f), prop, false);
    //            }
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //}
    public static void DrawChildren(this SerializedProperty serializedProperty,int startAt=0, int endAt=999)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        if (prop.depth > depth && startAt==0)
        {
            EditorGUILayout.PropertyField(prop, true);
        }
        int count=0;
        while (prop.NextVisible(false))
        {
            count++;
            if (prop.depth > depth)
            {
                if (count >= startAt && count <= endAt) {
                    EditorGUILayout.PropertyField(prop, true);
                }
            } else
            {
                return;
            }        
        }
    }
    public static void DrawChildren(this SerializedProperty serializedProperty, int startAt, int endAt, float minLabelWidth=150f, float minValueWidth=22f)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        if (prop.depth > depth && startAt == 0)
        {
            DGUI.PropertyField(prop, minLabelWidth, minValueWidth);
        }
        int count = 0;
        while (prop.NextVisible(false))
        {
            count++;
            if (prop.depth > depth)
            {
                if (count >= startAt && count <= endAt)
                {
                    DGUI.PropertyField(prop, minLabelWidth, minValueWidth);
                }
            }
            else
            {
                return;
            }
        }
    }
    public static void DrawChildrenLight(this SerializedProperty serializedProperty, string startOn, string endOn)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        bool started = false;
        if (startOn == "")
        {
            started = true;
        }
        if (prop.depth > depth && (startOn == "" || prop.name == startOn))
        {
            started = true;

            EditorGUI.PropertyField(EditorGUILayout.GetControlRect(false, 16f), prop, false);
            if (prop.name == endOn)
            {
                return;
            }
        }

        while (prop.NextVisible(false))
        {
            if (prop.name == startOn)
            {
                started = true;
            }
            if (prop.depth > depth)
            {
                if (started == true)
                {
                    EditorGUI.PropertyField(EditorGUILayout.GetControlRect(false, 16f), prop, false);
                }
            }
            else
            {
                return;
            }
            if (prop.name == endOn)
            {
                return;
            }

        }
    }
    public static void DrawChildren(this SerializedProperty serializedProperty, string startOn, string endOn)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        bool started=false;
        if (startOn == "")
        {
            started = true;
        }
		if (prop.depth > depth && (startOn=="" || prop.name== startOn) ) 
        {
            started = true;
            EditorGUILayout.PropertyField(prop, true);
            if (prop.name == endOn)
            {
                return;
            }
        }

        while (prop.NextVisible(false))
        {
            if (prop.name == startOn)
            {
                started = true;
            }
            if (prop.depth > depth)
            {
                if (started == true)
                {
                    EditorGUILayout.PropertyField(prop, true);
                }
            } else
            {
                return;
            }
            if(prop.name==endOn)
            {
                return;
            }
            
        }
    }
    public static void DrawChildren(this SerializedProperty serializedProperty, string startOn, string endOn, float minLabelWidth= 150f, float minValueWidth = 22f)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        bool started = false;
        if (startOn == "")
        {
            started = true;
        }
        if (prop.depth > depth && (startOn == "" || prop.name == startOn))
        {
            started = true;
            DGUI.PropertyField(prop, minLabelWidth, minValueWidth);
            if (prop.name == endOn)
            {
                return;
            }
        }

        while (prop.NextVisible(false))
        {
            if (prop.name == startOn)
            {
                started = true;
            }
            if (prop.depth > depth)
            {
                if (started == true)
                {
                    DGUI.PropertyField(prop, minLabelWidth, minValueWidth);
                }
            }
            else
            {
                return;
            }
            if (prop.name == endOn)
            {
                return;
            }

        }
    }
    public static void TextField(this SerializedProperty prop, float labelwidth = 100f, float minValueWidth = 22f)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 16f);
        float fullwidth = rect.width;
        rect.width = labelwidth;
        EditorGUI.LabelField(rect, prop.displayName);
        rect.x += rect.width;
        rect.width = fullwidth - rect.width;

        if (rect.width < minValueWidth)
        {
            rect.x -= minValueWidth - rect.width;
            rect.width = minValueWidth;
        }

        var oldValue = prop.stringValue;
        prop.stringValue = EditorGUI.TextField(rect, prop.stringValue);
        if(oldValue!=prop.stringValue)
        {
            prop.serializedObject.ApplyModifiedProperties();
        }
    }
    public static void Draw(this SerializedProperty prop, float labelwidth, float minValueWidth)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 16f);
        float fullwidth = rect.width;
        rect.width = labelwidth;
        EditorGUI.LabelField(rect, prop.displayName);
        rect.x += rect.width;
        rect.width = fullwidth - rect.width;

        if (rect.width < minValueWidth)
        {
            rect.x -= minValueWidth - rect.width;
            rect.width = minValueWidth;
        }

        EditorGUI.PropertyField(rect, prop,new GUIContent(""),true);
    }
    public static void DrawChildren(this SerializedProperty serializedProperty, string except)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        if (prop.depth > depth && prop.name != except)
        {
            EditorGUILayout.PropertyField(prop, true);
        }
        int count = 0;
        while (prop.NextVisible(false))
        {
            count++;
            if (prop.depth > depth)
            {
                if (prop.name != except)
                {
                    EditorGUILayout.PropertyField(prop, true);
                }
            }
            else
            {
                return;
            }
        }
    }
    public static void DrawChildren(this SerializedProperty serializedProperty, string except, float labelwidth = 100f, float minValueWidth = 22f)
    {
        SerializedProperty prop = serializedProperty.Copy();
        int depth = prop.depth;
        prop.NextVisible(true);
        if (prop.depth > depth && prop.name!= except)
        {
            prop.Draw(labelwidth, minValueWidth);
        }
        int count = 0;
        while (prop.NextVisible(false))
        {
            count++;
            if (prop.depth > depth)
            {
                if (prop.name!= except)
                {
                    prop.Draw(labelwidth, minValueWidth);
                }
            }
            else
            {
                return;
            }
        }
    }
    public static void Draw(this SerializedProperty serializedProperty)
    {
        EditorGUILayout.PropertyField(serializedProperty, true);
    }
    public static void Draw(this SerializedProperty serializedProperty, string label)
    {
        EditorGUILayout.PropertyField(serializedProperty, new GUIContent(label), true);
    }
    public static void Vector2Field(this SerializedProperty serializedProperty, ref Vector2 value)
    {

        serializedProperty.vector2Value = value;
      //  EditorGUILayout.Vector2Field(serializedProperty.displayName, )
     //   EditorGUILayout.PropertyField(serializedProperty, , true);
    }
    public static float FloatField(this SerializedProperty serializedProperty, float minLabelWidth = 150f, float minValueWidth = 22f)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 16f);
        float fullwidth = rect.width;
        rect.width = minLabelWidth;
        if (rect.width > fullwidth)
            rect.width = fullwidth - 22f;

        GUI.skin.label.clipping = TextClipping.Clip;
        EditorGUI.LabelField(rect, serializedProperty.displayName, GUI.skin.label);
        rect.x += rect.width;
        rect.width = fullwidth - rect.width;
        if (rect.width < minValueWidth)
        {
            rect.x -= minValueWidth - rect.width;
            rect.width = minValueWidth;
        }
        var old = serializedProperty.floatValue;
        serializedProperty.floatValue = EditorGUI.FloatField(rect, serializedProperty.floatValue);
        if(old!= serializedProperty.floatValue)
        {
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }
        return serializedProperty.floatValue;

    }
    public static bool Toogle(this SerializedProperty prop, float minLabelWidth = 100f, float minValueWidth = 22f)
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
        var old = prop.boolValue;
        prop.boolValue = EditorGUI.Toggle(rect, prop.boolValue);
        if (prop.boolValue!=old){
            prop.serializedObject.ApplyModifiedProperties();
        }

        return prop.boolValue;
    }
    //public static void DrawWindowed(this SerializedProperty serializedProperty, float height, string themeName , string IconName = "")
    //{
    //    if(serializedProperty.BeginWrapperWindow(height, themeName, IconName))
    //    {
    //        serializedProperty.DrawChildren();
    //    }
    //    serializedProperty.EndWrapperWindow();
    //}
    public static void CollapseChildren(this SerializedProperty serializedProperty)
    {
        var children = serializedProperty.GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            children[i].isExpanded = false;
        }
    }
    public static void CopyCollapsedChildren(this SerializedProperty serializedProperty, SerializedProperty from)
    {
        var fromChildren = from.GetChildren();
        var children = serializedProperty.GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            children[i].isExpanded = fromChildren[i].isExpanded;
        }
    }
    //public static void DrawWindowed(this SerializedProperty serializedProperty, float height, string themeName, string IconName, int startOn, int endOn, float labelwidth, float minValueWidth)
    //{
    //    if (serializedProperty.BeginWrapperWindow(height, themeName, IconName))
    //    {
    //        serializedProperty.DrawChildren(startOn, endOn, labelwidth, minValueWidth);
    //    }
    //    serializedProperty.EndWrapperWindow();
    //} 
    //public static void DrawWindowed(this SerializedProperty serializedProperty, float height, string themeName, string IconName, string overrideName)
    //{
    //    if (serializedProperty.BeginWrapperWindow(height, themeName, IconName, 0f,overrideName))
    //    {
    //        serializedProperty.DrawChildren();
    //    }
    //    serializedProperty.EndWrapperWindow(true);
    //}
    //public static void DrawWindowed(this SerializedProperty serializedProperty, float height, string themeName = "", string IconName = "", float minLabelWidth=150f, float minValueWidth=22f)
    //{
    //    if (serializedProperty.BeginWrapperWindow(height, themeName, IconName))
    //    {
    //        serializedProperty.DrawChildren(0,999,minLabelWidth, minValueWidth);
    //    }
    //    serializedProperty.EndWrapperWindow();
    //}
    //public static void DrawWindowedChild(this SerializedProperty serializedProperty, string childName, float height, string themeName="", string IconName="")
    //{
    //    SerializedProperty child = serializedProperty.FindPropertyRelative(childName);
    //    if (child.BeginWrapperWindow(height,themeName, IconName))
    //    {
    //        child.DrawChildren();
    //    }
    //    child.EndWrapperWindow();
    //}
    public static void DrawChild(this SerializedProperty serializedProperty, string name)
    {
        serializedProperty.FindPropertyRelative(name).Draw();
    }
	//public static void DrawBeautyArrayofClasses(this SerializedProperty property, string[] themeList, float height = 32f, float Spacement=5f,string IconFileName = "", int depth = 0, string OverrideName="")
 //   {
 //       try
 //       {
 //           if (property.BeginWrapperWindow(height, themeList[0], IconFileName, 0f,OverrideName))
 //           {

 //               if (!property.isArray)
 //               {
 //                   Debug.LogError("Error: " + property.displayName);
 //               }
 //               for (int i = 0; i < property.arraySize; i++)
 //               {
 //                   if (property.GetArrayElementAtIndex(i).hasVisibleChildren && depth > 0)
 //                   {
 //                       string[] newThemeList = ArrayUtility.Remove(themeList, themeList[0]);
 //                       property.GetArrayElementAtIndex(i).DrawBeautyClass(newThemeList, Mathf.Clamp(height * 0.75f, 16f, 999f), 5f,IconFileName, depth - 1);
 //                       if (i < property.arraySize - 1)
 //                           GUILayout.Space(Spacement);
 //                   }
 //                   else
 //                   {
 //                       property.GetArrayElementAtIndex(i).Draw();
 //                   }

 //               }
 //               DGUI.ArrayIncreaseButton(property);
 //           }
 //           property.EndWrapperWindow();
 //       } catch
 //       {
 //           Debug.LogError("Error: " + property.displayName);
 //       }
 //   }
 //   public static void DrawBeautyArray(this SerializedProperty property, float height, string themeName, string IconFileName = "")
 //   {
 //       if (property.BeginWrapperWindow(height, themeName, IconFileName))
 //       {
 //           property.DrawChildren(1,99999);
 //           DGUI.ArrayIncreaseButton(property);
 //       }
 //       property.EndWrapperWindow();
 //   }
    //	public static void DrawBeautyArrayofClasses(this SerializedProperty property, string[] themeList, float[] heightList, float[] SpacementList, string[] IconFileNameList,int depth)
    //    {
    //        try
    //        {
    //			if (property.BeginWrapperWindow(heightList[0], themeList[0], IconFileNameList[0]))
    //            {
    //                ComboMasterInterfaceConfig.DepthTheme theme = InterfaceThemeConfig.GetCustomStyle(themeList[0]);
    //                if (!property.isArray)
    //                {
    //                    Debug.LogError("Error: " + property.displayName);
    //                }
    //                for (int i = 0; i < property.arraySize; i++)
    //                {
    //
    //                    if (property.GetArrayElementAtIndex(i).hasVisibleChildren && depth > 0)
    //                    {
    //
    //                        string[] newThemeList = ArrayUtility.Remove(themeList, themeList[0]);
    //						float[] newHeightList = ArrayUtility.Remove(heightList, heightList[0]);
    //						string[] newSpacementList= ArrayUtility.Remove(SpacementList, SpacementList[0]);
    //						string[] newIconFileName = ArrayUtility.Remove(IconFileNameList, IconFileNameList[0]);
    //						property.GetArrayElementAtIndex(i).DrawBeautyClass(themeList, heightList,SpacementList,IconFileNameList, depth - 1);
    //                        if (i < property.arraySize - 1)
    //							GUILayout.Space(SpacementList[0]);
    //                    }
    //                    else
    //                    {
    //                        property.GetArrayElementAtIndex(i).Draw();
    //                    }
    //
    //                }
    //                DGUI.ArrayIncreaseButton(property);
    //            }
    //            property.EndWrapperWindow();
    //        } catch
    //        {
    //            Debug.LogError("Error: " + property.displayName);
    //        }
    //    }
    //public static void DrawBeautyClass(this SerializedProperty property, string[] themeList, float height, float Spacement, string IconFileName,int depth)
    //{
    //    if (property.BeginWrapperWindow(height, themeList[0], IconFileName))
    //    {

    //        GUILayout.Space(5f);
    //        List<SerializedProperty> children = property.GetChildren();

    //        for (int i = 0; i < children.Count; i++)
    //        {
    //            if (children[i].hasVisibleChildren && depth > 0)
    //            {
    //                string[] newThemeList = ArrayUtility.Remove(themeList, themeList[0]);

    //                if (children[i].isArray)
    //                {
    //                    children[i].DrawBeautyArrayofClasses(newThemeList, Mathf.Clamp(height * 0.75f, 16f, 999f), 5f, IconFileName, depth - 1);
    //                } else
    //                {
				//		children[i].DrawBeautyClass(newThemeList, Mathf.Clamp(height * 0.75f, 16f, 999f), 5f,IconFileName,depth - 1);
    //                }

    //            }
    //            else
    //            {

    //                children[i].Draw();

    //            }
    //            if (i < children.Count - 1)
    //                GUILayout.Space(Spacement);
    //        }
    //    }
    //    property.EndWrapperWindow();
    //}

//	public static void DrawBeautyClass(this SerializedProperty property, string[] themeList, float[] heightList, float[] SpacementList,string[] IconFileName, int depth)
//    {
//		if (property.BeginWrapperWindow(heightList[0], themeList[0], IconFileName[0]))
//        {
//            ComboMasterInterfaceConfig.DepthTheme theme = InterfaceThemeConfig.GetCustomStyle(themeList[0]);
//            GUILayout.Space(theme.arrayFirstSpacement);
//            List<SerializedProperty> children = property.GetChildren();
//
//            for (int i = 0; i < children.Count; i++)
//            {
//                if (children[i].hasVisibleChildren && depth > 0)
//                {
//                    string[] newThemeList = ArrayUtility.Remove(themeList, themeList[0]);
//					float[] newHeightList = ArrayUtility.Remove(heightList, heightList[0]);
//					string[] newSpacementList= ArrayUtility.Remove(SpacementList, SpacementList[0]);
//					string[] newIconFileName = ArrayUtility.Remove(IconFileName, IconFileName[0]);
//                    if (children[i].isArray)
//                    {
//						children[i].DrawBeautyArrayofClasses(newThemeList, heightList, SpacementList, IconFileName, depth - 1);
//                    } else
//                    {
//						children[i].DrawBeautyClass(newThemeList, heightList, SpacementList,IconFileName,depth - 1);
//                    }
//                }
//                else
//                {
//
//                    children[i].Draw();
//
//                }
//                if (i < children.Count - 1)
//					GUILayout.Space(SpacementList[0]);
//            }
//        }
//        property.EndWrapperWindow();
//    }
 //   public static ComboMasterInterfaceConfig InterfaceThemeConfig;
    public static string ThemeFileName = "ComboMasterTheme";
    public static class Margin
    {
        public static float left = 10f;
        public static float right = 10f;
        public static float up = 4f;
        public static float down = 10f;
    }


 //   public static bool BeginWrapperWindow (this SerializedProperty property, float height = 32f, string themeName = "", string IconFileName = "", float reduceClicableWidth=0f, string overrideLabel="", float LabelXOffset=0)
	//{
	//	//try {
	//		if (InterfaceThemeConfig == null) {
	//			InterfaceThemeConfig = FileUtility.LoadFile (ThemeFileName) as ComboMasterInterfaceConfig;
	//		}

	//		ComboMasterInterfaceConfig.DepthTheme theme = themeName == "" ? InterfaceThemeConfig.CustomStyles [0] : InterfaceThemeConfig.GetCustomStyle (themeName);
	//		Texture2D icon = (Texture2D)FileUtility.LoadTexture (IconFileName);

	//		if (theme == null) {
	//			Debug.Log ("Theme Error");
	//		}
	//		GUILayout.BeginVertical (property.isExpanded ? theme.WindowOpennedHeaderStyle : theme.WindowClosedHeaderStyle);
	//		Rect label = EditorGUILayout.GetControlRect (false, height);
	//		label.x -= 12f;
	//		label.width += 12f;
 //       //Header block style:
 //       #region Header visible block
 //       label.x += LabelXOffset;
 //       GUI.Box(label, icon != null ? new GUIContent((overrideLabel==""?property.displayName: overrideLabel) + " ", icon) : new GUIContent((overrideLabel == "" ? property.displayName : overrideLabel), (Texture2D)FileUtility.LoadTexture("ClosedFoldIcon")), theme.HeaderLabelStyle);
 //       //label.x -= LabelXOffset;
 //       #endregion
 //       #region clicable button
 //       label.width += reduceClicableWidth-LabelXOffset;

 //           if (GUI.Button (label, "", GUIStyle.none)) {
	//			property.isExpanded = !property.isExpanded;
	//		}
 //           #endregion
 //           #region Sandwich
 //           GUIStyle sandwichStyle = new GUIStyle ();
	//		sandwichStyle.alignment = TextAnchor.MiddleRight;

 //           GUI.Box (label, new GUIContent (theme.MinimizeIcon), sandwichStyle);
	//		#endregion

	//		if (property.isExpanded) {
	//			EditorGUILayout.BeginVertical (theme.WindowInnerStyle);
	//			EditorGUILayout.BeginHorizontal ();
	//			GUILayout.Space (Margin.left);
	//			EditorGUILayout.BeginVertical ();
 //                GUILayout.Space(Margin.up);
	//		}
	////	} catch {
	//		//Debug.Log("Error: "+property.name);
	////	}
 //       return property.isExpanded;
 //   }
    public static bool ArrayIncreaseButton(this SerializedProperty prop, float height = 32f, float width = 32f)
    {
        Rect buttons = EditorGUILayout.GetControlRect(false, height);
        //   buttons.height = 32f;
        //  buttons.y -= 38f;
        buttons.x += buttons.width * 0.5f - width;
        buttons.width = width;

        if (GUI.Button(buttons, "+"))
        {
            prop.arraySize += 1;
            return true;
        }
        buttons.x += width;
        if (GUI.Button(buttons, "-") && prop.arraySize > 0)
        {
            prop.arraySize -= 1;
            return true;
        }
        return false;
    }
    public static void EndWrapperWindow(this SerializedProperty property)
    {
        if (property.isExpanded)
        {
            GUILayout.Space(Margin.down);
            EditorGUILayout.EndVertical();
            GUILayout.Space(Margin.right);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        //GUILayout.Space(-EditorGUIUtility.standardVerticalSpacing);
    }
    public static Rect EndWrapperWindow(this SerializedProperty property, bool getLastRect)
    {
        if (property.isExpanded)
        {
            GUILayout.Space(Margin.down);
            EditorGUILayout.EndVertical();
            GUILayout.Space(Margin.right);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
        Rect lastRect = GUILayoutUtility.GetLastRect();
        GUILayout.Space(-EditorGUIUtility.standardVerticalSpacing);
        return lastRect;
    }
}
#endif