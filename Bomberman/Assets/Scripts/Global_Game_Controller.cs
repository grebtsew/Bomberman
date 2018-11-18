using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global_Game_Controller : MonoBehaviour {

 
	private Text level_label;
	private Text enemy_label;

	public GameObject map_parent;
	public Map map;

	// Use this for initialization
	void Start () {
		// init labels
		 foreach(Text t in FindObjectsOfType<Text>()){
                switch(t.tag){
                    case "enemies":
                    enemy_label = t;
                    break;
					case "level":
                    level_label = t;
                    break;
				}
		 }

		

		// increase map size over maps
			if(PlayerPrefs.GetInt("current_level").ToString().Length == 0){
				 PlayerPrefs.SetInt("current_level", 1);
				 }

		int level = PlayerPrefs.GetInt("current_level");


		if(level <= 8){
			map =  gameObject.AddComponent<Map>();
			map.construct(1+level,11,11 , map_parent);
		} else {
			map =  gameObject.AddComponent<Map>();
			map.construct(1+level,11 +(level-8)*2,11+(level-8)*2 , map_parent);
		}

		update_labels();
		
	}

	public void update_labels(){
		int i = 0;
		foreach(Player a in FindObjectsOfType<Player>()){
			if(a.isActiveAndEnabled){
				i++;
			}
		}

			if(i <= 1){
			if(FindObjectOfType<door_script>()){
				
			Destroy(FindObjectOfType<door_script>().gameObject);
			}
		}

		if(i > 0){
			i-= 1;
		}

		enemy_label.text = (i).ToString();
	

		level_label.text = PlayerPrefs.GetInt("current_level").ToString();
	}

	public void Restart(){


	// get animation
				fade_script fade = new fade_script();
			// init fader
        foreach(fade_script f in FindObjectsOfType<fade_script>()){
            if(f.tag == "fader"){
               fade = f;
            } else {
               continue;
            }
        }
		
		// reset values


		// load map
		StartCoroutine(fade.FadeAndLoadScene(fade_script.FadeDirection.In, "Game"));
           
       
	}

}
