using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
public static class FileUtility {
    static Dictionary<string, Texture> loadedTextures;
    static Dictionary<string, object> loadedFiles;
    public static Texture LoadTexture(string name)
    {
        if (name == "")
            return null;

        if (loadedTextures == null)
            loadedTextures = new Dictionary<string, Texture>();

        if (!loadedTextures.ContainsKey(name))
        {
            Texture tex = Resources.Load(name) as Texture;
            loadedTextures.Add(name, tex); 
            if (tex == null)
                Debug.LogError("File not found " + name);

            return tex;
        }
           
        return loadedTextures[name];
    }

    public static object LoadFile(string name)
    {
        if (loadedFiles == null)
            loadedFiles = new Dictionary<string, object>();

        if (!loadedFiles.ContainsKey(name))
        {
            object tex = Resources.Load(name) as object;
            loadedFiles.Add(name, tex);
        }

        if (loadedFiles[name] == null)
        {
            Debug.LogError("FILE NOT FOUND: "+ name);
        }
      return loadedFiles[name];
       // return Resources.Load(name) as object;
    }

    public static void OpenFileWithDefaultApplication(string name)
    {
#if UNITY_EDITOR
        string[] file = AssetDatabase.FindAssets(name);
        string AssetPath = AssetDatabase.GUIDToAssetPath(file[0]);
        string appPath = Application.dataPath;
        appPath = appPath.Replace("Assets", "");
        Debug.Log("Opening File: " + appPath + AssetPath);
        System.Diagnostics.Process.Start(appPath + AssetPath);
#endif
    }
	#if UNITY_EDITOR
	public static string GetSelectionFolder() {
		string folder = AssetDatabase.GetAssetPath (Selection.activeObject);
		string[] folderSplited = folder.Split ('/');
		if (folderSplited [folderSplited.Length - 1].Contains (".")) {
			folder=folderSplited[0];
			for(int i=1; i<folderSplited.Length - 1; i++) {
				folder+="/"+folderSplited[i];
			}
		}
		return folder;
    }
	public static bool CheckIfItIsSelectionFolder (string folder)
	{

		string[] folderSplited = folder.Split ('/');
		if (folderSplited [folderSplited.Length - 1].Contains (".")) {
			return false;
		} else {
			if (folderSplited [folderSplited.Length - 1] == "Resources") {
				return true;
			} else {
				return false;
			}
		}
    }
	#endif
}
