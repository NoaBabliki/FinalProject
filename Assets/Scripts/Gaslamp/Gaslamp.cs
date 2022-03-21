using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// turn on / off
// clue system

public class Gaslamp : MonoBehaviour

{
    [SerializeField] private int gaslightNumber;
    [SerializeField] private Light gaslight;
    [SerializeField] private GameObject clue;
    [SerializeField] private Transform statue;
    //[SerializeField] private Transform shadow;
    [SerializeField] private float originalTimeUntilClue = 200f;
    [SerializeField] private AudioSource dropAudio;
    [SerializeField] private Light decorativeLightSpot;

    private float timeUntilClue;
    private bool hintIsOn;
    static public int gaslightNumberOn = 0;
    private int countFloorCollisions = 0;
    MeshRenderer statueMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        timeUntilClue = originalTimeUntilClue;
        hintIsOn = false;
        statueMesh = statue.GetComponent<MeshRenderer>();
        clue.GetComponent<Animator>().SetBool("NeedHint", false);
        gaslight.enabled = false;
        statueMesh.enabled = false;
        decorativeLightSpot.enabled = false;
        //shadow.GetComponent<MeshRenderer>().enabled = false;
    
    }

    // Update is called once per frame
    void Update()
    {   
        AutoSwitch();
    }

    //switch on by turned, every time a mission ends, the next light is turned on and
    //the rest are turned off
    private void AutoSwitch(){
        if (gaslightNumberOn != gaslightNumber) {
            gaslight.enabled = false;
            clue.GetComponent<Animator>().SetBool("NeedHint", false);
            hintIsOn = false;
            statueMesh.enabled = false;
            decorativeLightSpot.enabled = false;
            timeUntilClue = originalTimeUntilClue;
        }
        else {
            gaslight.enabled = true;
            statueMesh.enabled = true;
            decorativeLightSpot.enabled = true;
            if (timeUntilClue > 0){
                timeUntilClue -= Time.deltaTime;
            }
            else {
                if (!hintIsOn)
                {
                    clue.GetComponent<Animator>().SetBool("NeedHint", true);
                    hintIsOn = true;
                }
                
            }
        }
        
    }


    //check collision with floor, if detected play drop sound
    private void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;
        if (other.tag == "Floor"){
            if (countFloorCollisions > 0){
                dropAudio.Play();
            }
            else {
                countFloorCollisions += 1;
            }
            
        }
    }
}