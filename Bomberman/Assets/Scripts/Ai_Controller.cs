
using UnityEngine;
using System.Collections;
using System;

public class Ai_Controller : Controller
{

public AI_MODES mode;

// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(mode){
            case AI_MODES.FARM: // wants to destroy boxes, and loot

            break;
            case AI_MODES.DODGE: // wont drop bombs only avoid bombs and loot

            break;
            case AI_MODES.KILL: // will try to kill you!

            break;
        }
	}

}
