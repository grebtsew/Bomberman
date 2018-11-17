using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour {


	public Player_Controller player_controller;

    public Vector3 offset;         //Private variable to store the offset distance between the player and camera


	// Use this for initialization
	void Start () {
		

		if(player_controller != null){

		
		player_controller = FindObjectOfType<Player_Controller>();
		offset =new Vector3(-1,0,-4) ;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player_controller != null)
		 transform.position = player_controller.transform.position + offset;
	}
}
