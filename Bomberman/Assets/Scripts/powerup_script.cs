using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup_script : MonoBehaviour {

	public GameObject bomb;
	public GameObject kick;
	public GameObject life;
	public GameObject power;
	public GameObject speed;

	private GameObject text;
	private GameObject curr;

	public POWERUPS powerup;

	// Use this for initialization
	void Start () {
	
		powerup = (POWERUPS)Random.Range(0, 5);

		// load prefab look
		switch(powerup){
			case POWERUPS.BOMB:
			curr = bomb;
			break;
			case POWERUPS.KICK:
			curr = kick;
			break;
			case POWERUPS.LIFE:
			curr = life;
			break;
			case POWERUPS.POWER:
			curr = power;
			break;
			case POWERUPS.SPEED:
			curr = speed;
			break;
		}
			// curr position
		GameObject go =	Instantiate(curr, transform.position, Quaternion.identity) as GameObject;
		go.GetComponent<Transform>().SetParent(this.transform);

		text = Resources.Load("PopupTextParent") as GameObject;
	}
	
	  void OnTriggerEnter(Collider collider)
    
    {
	
        if (collider.CompareTag ("Player"))
        {
           
            Destroy(gameObject); // 3  

			Player player = collider.GetComponent<Player>();


			string s = "";

			switch(powerup){
			case POWERUPS.BOMB:
			s = "+1 Bomb";
			player.bombs++;
			break;
			case POWERUPS.KICK:
			player.canKick = true;
			s = "Kick unlocked";
			break;
			case POWERUPS.LIFE:
			player.lifes++;
			s = "+1 Life";
			break;
			case POWERUPS.POWER:
			player.explosion_power++;
			s = "+1 explosive power";
			break;
			case POWERUPS.SPEED:
			player.moveSpeed++;
			s = "+1 Speed";
			break;
		}
		
		if(player.GetComponent<Player_Controller>().isActiveAndEnabled){ // if human controlled
		player.update_label(powerup);
		}

		GameObject go = Instantiate(text,collider.transform.position, Quaternion.identity) as GameObject;

			go.GetComponent<FloatingText>().setText(s, new Color());

			foreach(Canvas c in  FindObjectsOfType<Canvas>()){
				if(c.tag == "world_canvas"){
					go.transform.SetParent(c.transform);
				}
			}
			

        }
    }
}
