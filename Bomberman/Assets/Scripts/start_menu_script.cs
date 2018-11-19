using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start_menu_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void exit_pressed(){
		Application.Quit();
	}

	public void start_pressed(){
		 
		
		 // set first start values
		 PlayerPrefs.SetInt("current_level", 0); // set start level
		// animation and load scene
		     if (Application.CanStreamedLevelBeLoaded("Game"))
     {
		StartCoroutine(GameObject.FindObjectOfType<fade_script>().FadeAndLoadScene(fade_script.FadeDirection.In, "Game"));
	 } else {
		 	StartCoroutine(GameObject.FindObjectOfType<fade_script>().FadeAndLoadScene(fade_script.FadeDirection.In, "Game_mobile"));
	
	 }
	
	}
}
