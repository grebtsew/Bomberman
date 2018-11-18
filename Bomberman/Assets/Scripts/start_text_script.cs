using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class start_text_script : MonoBehaviour {


	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = "LEVEL " + PlayerPrefs.GetInt("current_level").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
