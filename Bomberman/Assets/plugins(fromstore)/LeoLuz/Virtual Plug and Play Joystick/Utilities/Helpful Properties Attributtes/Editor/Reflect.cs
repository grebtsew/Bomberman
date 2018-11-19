using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {
    public Object ObjectToReflect;
    [ReflectMethods("ObjectToReflect")]
    public string MethodToReflect;
    public int value;
	// Use this for initialization
    public void ReflectNow()
    {
        ((MonoBehaviour)ObjectToReflect).SendMessage(MethodToReflect,value);
    }

}
