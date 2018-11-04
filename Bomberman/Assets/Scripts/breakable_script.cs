using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable_script : MonoBehaviour {

	public GameObject powerup_prefab;

	// Use this for initialization
	void Start () {
		 powerup_prefab = (GameObject) Resources.Load("PowerUp",typeof(GameObject));
    
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	  void OnCollisionEnter(Collision collision)
    
    {
	
        if (collision.collider.CompareTag ("Explosion"))
        {
           
            Destroy(gameObject); // 3  

			if(Random.Range(0.0f, 1.0f)> 0.7f){
			
				GameObject temp_floor = Instantiate(powerup_prefab, transform.position, Quaternion.identity) ;
			}
        }
    }
}
