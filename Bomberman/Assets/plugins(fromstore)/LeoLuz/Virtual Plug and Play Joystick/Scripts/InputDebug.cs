using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDebug : MonoBehaviour {
    [ReadOnly]
    public Vector2 AXIS;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        AXIS=new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
}
