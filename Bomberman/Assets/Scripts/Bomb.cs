using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomb : MonoBehaviour {

public int explode_size = 2;
public GameObject explosionPrefab;
private bool exploded = false;

	// Use this for initialization
	void Start () {
		Invoke("Explode", 3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Explode() 
{
// center one
Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1


StartCoroutine(CreateExplosions(Vector3.forward));
StartCoroutine(CreateExplosions(Vector3.right));
StartCoroutine(CreateExplosions(Vector3.back));
StartCoroutine(CreateExplosions(Vector3.left));  



GetComponent<MeshRenderer>().enabled = false; //2
exploded = true; 
transform.Find("Collider").gameObject.SetActive(false); //3
Destroy(gameObject, .3f); //4
} 

private IEnumerator CreateExplosions(Vector3 direction) 
{

  List<Vector3> instantiate_list = new List<Vector3>();
//1
for (int i = 1; i <= explode_size; i++) 
  { 
  //2
  RaycastHit hit; 
  //3
  Physics.Raycast(transform.position , direction, out hit, 
    i); 

  //4
  if (!hit.collider) 
  { 
   instantiate_list.Add(transform.position + (i * direction));
   
    //6
  } 
  else 
  { //7

  

    if(hit.collider.CompareTag("Breakable")){
        instantiate_list.Add(transform.position + (i * direction));
    } else if(hit.collider.CompareTag("Player") || hit.collider.CompareTag("powerup") || hit.collider.CompareTag("Bomb")){
        instantiate_list.Add(transform.position + (i * direction));
        continue;
    } 
    

    break; 
  }

}

foreach(Vector3 v in instantiate_list){
   Instantiate(explosionPrefab, v,
    //5 
      explosionPrefab.transform.rotation);  
  //8
  yield return new WaitForSeconds(.05f); 
}
}

void OnCollisionEnter(Collision collision) {
{

if (!exploded && collision.collider.CompareTag("Explosion"))
{ // 1 & 2  
 
  CancelInvoke("Explode"); // 2
   Explode(); // 3
}  
}

}

 
}
