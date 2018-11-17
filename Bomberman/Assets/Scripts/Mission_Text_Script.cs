using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_Text_Script : MonoBehaviour {

/*	string full_string = "Welcome to my 26th birthday party, one o'clock on saturday the first of december."+
	" The receipt will be at Studievägen 5 in Linköping. Coordinates to party location: 58.403975, 15.579573."+
	" Regarding parking space, there are many parking lots closeby next to the forest."+
	" To get there, drive towards Kårallen and follow the Olaus Magnus Väg all the way to the gravel parking."+
	" Coordinates to parking lots: 58.403871, 15.576743. " +
	" Cheers Grebtsew ";
*/
	string s_1 =  "Welcome ";
	string secret_1 = "to my 26th birthday party,";
	string secret_2 = " one o'clock on saturday ";
	string s_2 = " first of december." ;
	string s_3 = " The receipt will be at ";
	string secret_3 = " Studievägen 5 in Linköping." ;
	string secret_4 = " Coordinates to party location: 58.403975, 15.579573.";
	string s_4 = " Regarding parking space, ";
	string secret_5 = "there are many parking lots closeby next to the forest.";
	string s_5 = "To get there, drive towards Kårallen and ";
	string secret_6 = " follow the Olaus Magnus Väg";
	string s_6 = " all the way to the gravel parking."  ;
	string secret_7 = " Coordinates to parking lots: 58.403871, 15.576743." ;
	string s_7 =  "\n"+ "Cheers Daniel Westberg";

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
