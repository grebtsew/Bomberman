using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Game_Controller : MonoBehaviour {


/** TODOLIST

change character and animations

animated Text

Boss battles

Dialog 

Animationer

GUI

Build ais

 Make the bombs "pushable", so you can escape bombs next to you and push them towards your opponent

 Limit the amount of bombs that can be dropped

Menu

Phone ready



 **/

	public GameObject map_parent;
	private Map map;

	// Use this for initialization
	void Start () {
		// increase map size over maps

			if(PlayerPrefs.GetInt("current_level").ToString().Length == 0){
				 PlayerPrefs.SetInt("current_level", 1);
				 }

		int level = PlayerPrefs.GetInt("current_level");

		map = new Map(7,11 ,11 , map_parent);

	}

}
