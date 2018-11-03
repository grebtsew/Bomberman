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

 Autogenerate map

 Next map

Build ais

gravity ide, with sophie boob physics, nice idea!!!!!

 Make the bombs "pushable", so you can escape bombs next to you and push them towards your opponent

 Limit the amount of bombs that can be dropped

Menu

Phone ready

Add breakable blocks that get destroyed by the explosions

Create interesting powerups

Add lives, or a way to earn them

camera follow

 **/

	public GameObject map_parent;
	private Map map;

	// Use this for initialization
	void Start () {
		// increase map size over maps
		map = new Map(1,11,11, map_parent);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
