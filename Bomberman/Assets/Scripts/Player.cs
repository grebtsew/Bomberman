/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

private Text bomb_label;
private Text life_label;
private Text kick_label;
private Text explosion_label;
private Text speed_label;

public GlobalStateManager globalManager;

    public float moveSpeed = 5f;
   
    public ParticleSystem Explosion;

    public int bombs = 2;
    //Amount of bombs the player has left to drop, gets decreased as the player
    //drops a bomb, increases as an owned bomb explodes
    public bool canKick = false;
    public int lifes = 1;
    public int explosion_power = 2;
    public bool dead = false;
    public bool respawning = false;

    private fade_script fade;



public void update_label(POWERUPS powerup){
    switch(powerup){
        case POWERUPS.BOMB:
        bomb_label.text = bombs.ToString();
        break;
        case POWERUPS.KICK:
        if(canKick){
        kick_label.text = "1";
        } else {
        kick_label.text = "0";
        }
        break;
        case POWERUPS.LIFE:
        life_label.text = lifes.ToString();
        break;
        case POWERUPS.POWER:
        explosion_label.text = explosion_power.ToString();
        break;
        case POWERUPS.SPEED:
        speed_label.text = moveSpeed.ToString();
        break;
    }
}

    IEnumerator respawn_wait()
    {
        yield return new WaitForSeconds(3);
        respawning = false;
    }

  IEnumerator gameover_wait()
    {
    
        
         yield return new WaitForSeconds(1f);
            show_gameover_panel();

    }

    // Use this for initialization
    void Start ()
    {
        // init fader
        foreach(fade_script f in FindObjectsOfType<fade_script>()){
            if(f.tag == "fader"){
                continue;
            } else {
                fade = f;
            }
        }

        // init labels
        if(GetComponent<Player_Controller>().isActiveAndEnabled){
            foreach(Text t in FindObjectsOfType<Text>()){
                switch(t.tag){
                    case "Bomb":
                    bomb_label = t;
                    break;
                    case "life":
                    life_label = t;
                    break;
                    case "power":
                    explosion_label = t;
                    break;
                    case "speed":
                    speed_label = t;
                    break;
                    case "kick":
                    kick_label = t;
                    break;
                }
            }
        }
        //Cache the attached components for better performance and less typing

    }

    IEnumerator dmg_animation(){
        StartCoroutine(fade.FadeOnly(fade_script.FadeDirection.In));
         yield return new WaitForSeconds(1);
           StartCoroutine(fade.FadeOnly(fade_script.FadeDirection.Out));
      
          yield return new WaitForSeconds(1);
            StartCoroutine(fade.FadeOnly(fade_script.FadeDirection.In));
      
           yield return new WaitForSeconds(1);
             StartCoroutine(fade.FadeOnly(fade_script.FadeDirection.Out));
      
    }

    private void show_gameover_panel(){
        foreach(hide_on_start h in Resources.FindObjectsOfTypeAll<hide_on_start>()){
      
                if(h.tag == "gameover"){
            h.toggle();
            break;
                }
            }
             Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag ("Explosion"))
        {

        if(!respawning){
          lifes--;
        
        if(GetComponent<Player_Controller>().isActiveAndEnabled){
        update_label(POWERUPS.LIFE);
        }
          if(lifes <= 0){
            dead = true; // 1
            if(GetComponent<Player_Controller>().isActiveAndEnabled){     
               
                StartCoroutine(gameover_wait());
            } else {
                FindObjectOfType<Global_Game_Controller>().update_labels();
                Destroy(gameObject);
            }
            // 3 
            
            
            } else {
                if(GetComponent<Player_Controller>().isActiveAndEnabled){
                StartCoroutine(dmg_animation());
                } else {
                   
                }
            respawning = true;
            StartCoroutine(respawn_wait());
             }
             
            Instantiate(Explosion,transform.position, Quaternion.identity);
         
        }
        }
    }
}
