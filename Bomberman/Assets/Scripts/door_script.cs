using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_script : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(FindObjectsOfType<Player>().Length <= 1){
			Destroy(gameObject);
		}
	}
}
