using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_Text_Script : MonoBehaviour {

	string s_1 =  "Welcome ";
	string secret_1 = "This is the story about ";
	string secret_2 = " a small game development project ";
	string s_2 = "  First of " ;
	string s_3 = " this is a Bomberman game. ";
	string secret_3 = " Hope you like it. " ;
	string secret_4 = " Here is an example of how level progression could work. ";
	string s_4 = " Text ";
	string secret_5 = " secret ";
	string s_5 = "Text ";
	string secret_6 = " Secret";
	string s_6 = " text"  ;
	string secret_7 = " secret." ;
	string s_7 =  "\n"+ "Cheers  Grebtsew";

	// Use this for initialization
	void Start () {
		Text t = GetComponent<Text>();
		
		t.text = create_text();
	}

	private string create_text(){
		string res = "";
		int i =	PlayerPrefs.GetInt("current_level");
		Debug.Log(i);
		res += s_1;
		if(i >= 1){
			res+= secret_1;
		} else {
			res += "-- --- -- -----";
		}
		
		if(i >= 2){
			res+= secret_2;
		} else {
			res += "-- ---- -- ---";
		}
		res += s_2;
		res += s_3;
		if(i >= 3){
			res += secret_3;
		} else {
			res += "--- --- ------";
		}
		
		if(i >= 4){
			res += secret_4;
		} else {
			res += "-------------------";
		}
		res += s_4;
		if(i >= 5){
			res += secret_5;
		} else {
			res += "--- -- ----";
		}
		
		res += s_5;
		if(i >= 6){
			res += secret_6;
		} else {
			res += "------- -- --";
		}
		
		res += s_6;
		if(i >= 7){
			res += secret_7;
		} else {
			res += "--- -- ----";
		}
		
		res += s_7;
		
		return res;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
