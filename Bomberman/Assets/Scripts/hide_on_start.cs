using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hide_on_start : MonoBehaviour {

	public bool hide = true;

	// Use this for initialization
	void Start () {
		if(hide){
		gameObject.SetActive(false);
		} else {
			
			Time.timeScale = 0;
		}
	}
	
	public void toggle(){
		if(gameObject.activeSelf){
			gameObject.SetActive(false);
		} else {
			gameObject.SetActive(true);
		}

		bool t = false;
		foreach(hide_on_start h in Resources.FindObjectsOfTypeAll<hide_on_start>()){
			if(h.isActiveAndEnabled){
				t = true;
				break;
			}
            }
		if(t){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
		
	}

	public void restart(){
		Time.timeScale = 1;
		FindObjectOfType<Global_Game_Controller>().Restart();
	}
	public void exit(){
		Time.timeScale = 1;
		Application.Quit();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
