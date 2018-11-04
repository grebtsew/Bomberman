using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup_script : MonoBehaviour {


	public POWERUPS powerup;

	// Use this for initialization
	void Start () {
	
		powerup = (POWERUPS)Random.Range(0, 5);

		// load prefab look
		switch(powerup){
			case POWERUPS.BOMB:
			break;
			case POWERUPS.KICK:
			break;
			case POWERUPS.LIFE:
			break;
			case POWERUPS.POWER:
			break;
			case POWERUPS.SPEED:
			break;
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	  void OnCollisionEnter(Collision collision)
    
    {
	
        if (collision.collider.CompareTag ("Player"))
        {
           
            Destroy(gameObject); // 3  

			Player player = collision.collider.GetComponent<Player>();

			Debug.Log(powerup);

			switch(powerup){
			case POWERUPS.BOMB:
			player.bombs++;
			break;
			case POWERUPS.KICK:
			player.canKick = true;
			break;
			case POWERUPS.LIFE:
			player.lifes++;
			break;
			case POWERUPS.POWER:
			player.explosion_power++;
			break;
			case POWERUPS.SPEED:
			player.moveSpeed++;
			break;
		}
        }
    }
}
