using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUtility : MonoBehaviour {
    static Dictionary<string, Texture> loadedTextures;

    public static Texture LoadTexture(string name)
    {
        if (loadedTextures == null)
            loadedTextures = new Dictionary<string, Texture>();

        if (!loadedTextures.ContainsKey(name))
        {
            Texture tex = Resources.Load(name) as Texture;
            loadedTextures.Add(name, tex);
            Debug.Log("Loaded Texture");
        }

        return loadedTextures[name];
    }
}
