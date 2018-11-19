//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//public class AssetsUtility
//{
//    [MenuItem("Assets/Read Property Fields")]
//    public static void ReadSerializedObjectFields()
//    {
//        Object selected = Selection.activeObject;
//        string path = "";
//        if (selected == null)
//            Debug.Log("NotSelected");
//        else
//        {
//            path = AssetDatabase.GetAssetPath(selected.GetInstanceID());
//        }
//        if (path != "")
//        {
//            var selectedObject = AssetDatabase.LoadAllAssetsAtPath(path)[0];
//            if (selectedObject != null)
//            {
//                SerializedObject obj = new SerializedObject(selectedObject);
//                if (obj != null)
//                {
//                    string fields = "Fields of " + selectedObject.name;
//                    var prop = obj.GetIterator();
//                    while (prop.NextVisible(true))
//                    {
//                        fields += "\n" + prop.name;

//                    }
//                    Debug.Log(fields);
//                }

//            }
//        }
//    }


//    public static void ChangeObjectValues(Object modified /*recieves .anim file*/, string fieldName /*recieves WrapMode field name*/, System.Type type  /*recieves typeof(WrapMode)*/, object value /*recieves WrapMode.once*/)
//    {
//        string path = AssetDatabase.GetAssetPath(modified.GetInstanceID());
//        var selectedObject = AssetDatabase.LoadAllAssetsAtPath(path)[0]; //find asset file
//        SerializedObject obj = new SerializedObject(selectedObject); //create serialization for this asset
//        if (obj != null)
//        {
//            var prop = obj.GetIterator(); //get iterator to pass over all fields
//            while (prop.NextVisible(true)) //iterate over all fields
//            {
//                if(prop.name== fieldName) //verify if field name is correspondent.
//                {
//                    if(type==typeof(System.Enum)) //Verify if field type is enum
//                    {
//                        string[] names = System.Enum.GetNames(value.GetType()); //get list of values of an enum
//                        for (int i = 0; i < names.Length; i++) //iterate over all value lists to find index.
//                        {
//                            Debug.Log("Names " + names[i]+" == "+ value.ToString());
//                            if (names[i] == value.ToString()) //check correspondent index
//                            {
//                                prop.enumValueIndex = i; //change index value.
//                                Debug.Log("changed");
//                                break;
//                            }
//                        }
//                    }
//                    obj.ApplyModifiedProperties();
//                    Debug.Log("ApplyModifiedProperties()");
//                }                          
//            }
//        }
//    }
//}
