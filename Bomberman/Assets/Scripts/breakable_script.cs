using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable_script : MonoBehaviour {

	public GameObject powerup_prefab;

	public ParticleSystem explosion;

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
           
		 Instantiate(explosion, transform.position, Quaternion.identity);
		
			if(Random.Range(0.0f, 1.0f)> 0.7f){
			
				Instantiate(powerup_prefab, transform.position, Quaternion.identity) ;
			}
			 Destroy(gameObject); // 3  
        }
    }
}
