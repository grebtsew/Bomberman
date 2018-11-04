using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startpos_script : MonoBehaviour {


	public bool player_controller = false;
	private GameObject player_prefab;
	// Use this for initialization
	void Start () {
		 player_prefab = (GameObject) Resources.Load("Player", typeof(GameObject));

		 GameObject temp_prefab = Instantiate(player_prefab, transform.position, Quaternion.identity) ; // create new prefab instance
	
		 if(!player_controller){
			temp_prefab.GetComponent<Player_Controller>().enabled = false; // disable playercontroller
		 } else {
			 
			 temp_prefab.GetComponent<Ai_Controller>().enabled = false; 
			 camera_follow_player cam = FindObjectOfType<camera_follow_player>();
			 cam.player_controller = temp_prefab.GetComponent<Player_Controller>();
			 cam.offset = cam.transform.position - cam.player_controller.transform.position;
		 }
	
	}
	
}
