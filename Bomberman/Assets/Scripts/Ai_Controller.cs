
using UnityEngine;
using System.Collections;
using System;

public class Ai_Controller : Controller
{

public AI_MODES mode;
public AI_STATES state = AI_STATES.IDLE;
public int level = 0; // difficulty

private bool dodge; // avoid bombs
private bool bomb; // drop bombs
private bool powerup; // pick up powerup
private bool kick; // kick bombs
private bool follow; // follow other players to trap them

private Vector3 next_pos;
private Vector3 next_dir;

private bool acting = false;

private struct dir_dist{
   public Vector3 dir;
   public int dist;
   public string tag;
   public int path_value ;
}

private Player player;
private Rigidbody rigidBody;
 private Animator animator;
private ArrayList detections;

// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody> ();
         animator = transform.Find ("PlayerModel").GetComponent<Animator> ();

		switch(mode){
            case AI_MODES.FARM: // wants to destroy boxes, and loot
            dodge = true;
            bomb = true;
            powerup = false;
            kick = false;
            follow = false;
            break;

            case AI_MODES.DODGE: // wont drop bombs only avoid bombs and loot
            dodge = true;
            bomb = false;
            powerup = true;
            kick = false;
            follow = false;
            break;

            case AI_MODES.KILL: // will try to kill you!
            dodge = true;
            bomb = true;
            powerup = true;
            kick = true;
            follow = true;
            break;
        }
	}
	

    private void center(){
        transform.position = next_pos;
        acting = false;
    }


	// Update is called once per frame
	void Update () {
         animator.SetBool ("Walking", false);

        switch(state){
            case AI_STATES.DODGE:
            move();

            if(!acting)
            state = AI_STATES.IDLE;
            break;

            case AI_STATES.CENTER:
            center();

            if(!acting)
            state = AI_STATES.IDLE;
            break;
            case AI_STATES.FOLLOW:
            break;
            case AI_STATES.BOMB:
            break;
            case AI_STATES.KICK:
            break;
            case AI_STATES.POWERUP:
            break;
            case AI_STATES.IDLE:
            /* When idle decide next move, see interrupt levels below */

            //Do raycast in all 4 directions to see what we can do...

            detections = get_closest_collisions();
 
            //1 avoid bomb
            if(dodge){
                if(contains_tag(detections, "Bomb")){// || contains_tag(detections, "Explosion")){
                      
                    calculate_next_pos();
                    acting = true;
                    state = AI_STATES.DODGE;
                    break;
                }
            // if in bomb explosion, maybe kick later
            // state = AI_STATES.DODGE;
           
             }
          
            //2 pick up powerup, if accessable
            if(powerup){
            // state = AI_STATES.POWERUP;
             }

            //3 follow player
            if(follow){
            // state = AI_STATES.FOLLOW;
            }

            //4 place bomb, if player or breakable, optimze
            if(bomb){
            // state = AI_STATES.BOMB;
            }

            //5 kick 
            if(kick){
            // state = AI_STATES.KICK;
            }

            //6 Center - center player to detect correctly!
          
            break;
        }
    }

    private void calculate_next_pos(){

    dir_dist temp = new dir_dist();
    // get best
        foreach(dir_dist d in detections){
            if(temp.dir == null){
                temp = d;
            } else {
                if(d.path_value > temp.path_value){
                    temp = d;
                }
            }
        }

        next_pos = transform.position + temp.dir;
        next_dir = temp.dir;
     
    }


    private void move(){
        rigidBody.velocity = rigidBody.velocity + next_dir * player.moveSpeed ;

        if(next_dir == Vector3.forward){
            transform.rotation = Quaternion.Euler (0, 0, 0);
        } else if(next_dir == Vector3.back){
            transform.rotation = Quaternion.Euler (0, 180, 0);
        }else if(next_dir == Vector3.left){
            transform.rotation = Quaternion.Euler (0, 270, 0);
        }else if(next_dir == Vector3.right){
            transform.rotation = Quaternion.Euler (0, 90, 0);
        }


        animator.SetBool ("Walking", true);

        if(Vector3.Distance(transform.position, next_pos) < 0.2f){
             acting = false; // reached goal
        }
    }

    private bool contains_tag(ArrayList list, string tag){
        
        foreach(dir_dist d in list){
            if(d.tag == tag){
               return true;
            }
        }
        return false;
    }
		
    private ArrayList get_closest_collisions(){
            ArrayList temp = new ArrayList();
            temp.Add(get_closest_collision(Vector3.forward));
            temp.Add(get_closest_collision(Vector3.back));
            temp.Add(get_closest_collision(Vector3.left));
            temp.Add(get_closest_collision(Vector3.right));
            return temp;
    }
        
	
    private dir_dist get_closest_collision(Vector3 direction){

        dir_dist result = new dir_dist();
        bool found_collision = false;

        int i = 1;
         RaycastHit hit; 

        while(!found_collision){

   
            Physics.Raycast(transform.position , direction, out hit, i); 

            if (hit.collider) 
            { 
                Debug.Log(hit.collider.tag);
                found_collision = true;
  
                result.dir = direction;
                result.tag = hit.collider.tag;
                result.dist = i-1;
                result.path_value = result.dist;
                
                if(result.tag == "Bomb"){// || tag == "Explosion"){
                     result.path_value = -i;
              
                }
               

             }

             // add to length
             i++;
    }
    
    return result;
}


}
