
using UnityEngine;
using System.Collections;
using System;

public class Ai_Controller : Controller
{

public GameObject bombPrefab;
public AI_MODES mode;
public AI_STATES state = AI_STATES.IDLE;
public int level = 0; // difficult
private bool dodge; // avoid bombs

public bool aggressive = true;
private bool on_bomb = false;
private bool bomb; // drop bombs
private bool powerup; // pick up powerup
private bool kick; // kick bombs
private bool follow; // follow other players to trap them

private Vector3 next_pos;
private Vector3 next_dir;

private bool acting = false;

private ArrayList bomb_list;

private struct dir_dist{
   public Vector3 dir;
   public int dist;
   public string tag;
   public int path_value ;
}

private Player player;
private Rigidbody rigidBody;
 private Animator animator;
 private CapsuleCollider collider;
private ArrayList detections;

// Use this for initialization
	void Start () {
        collider = GetComponent<CapsuleCollider>();
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody> ();
         animator = transform.Find ("PlayerModel").GetComponent<Animator> ();

		switch(mode){
            case AI_MODES.FARM: // wants to destroy boxes, and loot
            dodge = true;
            bomb = true;
            powerup = true;
            kick = true;
            follow = true;
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
	

	// Update is called once per frame
	void Update () {
         animator.SetBool ("Walking", false);

        switch(state){
            case AI_STATES.DODGE: // move away from bomb
            move(next_dir, next_pos);
            break;
            case AI_STATES.CENTER: // center to tile
            move(next_dir, Round(transform.position));
            break;
            case AI_STATES.FOLLOW:
            break;
            case AI_STATES.BOMB:
            dropBomb();
            on_bomb = true;
            player.bombs--;
            state = AI_STATES.IDLE;
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
                if(contains_tag(detections, "Bomb") || on_bomb){// Detect if standing on a bomb instead
                     
                    on_bomb = false;
                      
                    calculate_next_dodge_pos();
                   
                    acting = true;
                    state = AI_STATES.DODGE;
                    break;
                }
            // if in bomb explosion, maybe kick later
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
                if(player.bombs != 0){
                    if(time_to_place_bomb()){
                        state = AI_STATES.BOMB;
                        
                        break;
                    }
                }
            // state = AI_STATES.BOMB;
            }

           

            //5 Center - center player to box to detect correctly!
            if(transform.position != Round(transform.position)){
                    acting = true;
                    state = AI_STATES.CENTER;   
            }
            break;
        }
    }


    private void calculate_safe_position(){
    }

    private void calculate_escape_path(){

    }

    private void calculate_fastest_path_too(Vector3 goal){
        // calculate shortest path between current pos and goal,
        // save as arraylist
    }
    private void move_path(){
        // move along a path
        // always do checks for bombs on the way and avoid
    }

    

    private bool time_to_place_bomb()
    {
       foreach(dir_dist d in detections){
           if( d.tag == "Breakable" || (d.tag == "Player" && aggressive)){
               if(d.dist <= player.explosion_power){
                   //if(got_escape_path())
                  return true;
               }
           }
       }
       return false;
    }

    private void  dropBomb()
    {
        if (bombPrefab)
        { //Check if bomb prefab is assigned first
       GameObject go = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(transform.position.x), 
        bombPrefab.transform.position.y, Mathf.RoundToInt(transform.position.z)),
        bombPrefab.transform.rotation);

        go.GetComponent<Bomb>().explode_size = player.explosion_power;
        go.GetComponent<Bomb>().player = player;
        }
    }
    private int amount_usable_paths(ArrayList detections)
    {
        int i = 0;
      foreach(dir_dist d in detections){
          if(d.dist > 0){
              i++;
          }
      }
      return i;
    }

    private Vector3 Round(Vector3 v){
        return new Vector3(Mathf.RoundToInt(v.x),Mathf.RoundToInt(v.y),Mathf.RoundToInt(v.z));
    }


    private void calculate_next_dodge_pos(){
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


    private void move(Vector3 direction, Vector3 position){

        // move to exact location over deltatime
        Vector3 movePosition = Vector3.MoveTowards(transform.position, position, player.moveSpeed * Time.deltaTime);
 
        rigidBody.MovePosition(movePosition);

        // player rotation
        if(direction == Vector3.forward){
            transform.rotation = Quaternion.Euler (0, 0, 0);
        } else if(direction == Vector3.back){
            transform.rotation = Quaternion.Euler (0, 180, 0);
        }else if(direction == Vector3.left){
            transform.rotation = Quaternion.Euler (0, 270, 0);
        }else if(direction == Vector3.right){
            transform.rotation = Quaternion.Euler (0, 90, 0);
        }

        // start animation
        animator.SetBool ("Walking", true);

        // done!
        if(Vector3.Distance(transform.position, position) == 0){
             acting = false; // reached goal
             state = AI_STATES.IDLE; // ai idle           
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
                found_collision = true;
  
                result.dir = direction;
                result.tag = hit.collider.tag;
                result.dist = i-1;
                result.path_value = result.dist;
                
                if(result.tag == "Bomb"){
                     result.path_value = result.dist / 2;     
                }
             }
             // add to length
             i++;
    }
    
    return result;
}


}
