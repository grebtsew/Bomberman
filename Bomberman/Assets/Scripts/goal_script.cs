using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	  void OnCollisionEnter(Collision collision)
    
    {
	
        if (collision.collider.CompareTag ("Player"))
        {
			// set next level
		
			
 			PlayerPrefs.SetInt("current_level",PlayerPrefs.GetInt("current_level") + 1);
           
			
			 PlayerPrefs.Save();

			

			//if not done
			// load map
            SceneManager.LoadScene("Game");
           
        }
    }
}
