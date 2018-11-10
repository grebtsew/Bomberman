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

Make the bombs "pushable", so you can escape bombs next to you and push them towards your opponent

Menu

Phone ready

AI Possible fixes:
Always see bomb bug, player in the way! see through
Dose to higher sides, also do a nextpos check and add to dose!
Calculate safe pos
follow
level
powerup
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

		map = new Map(3,11 ,11 , map_parent);

	}

}
