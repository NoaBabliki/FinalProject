using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// turn on / off
// clue system

public class Gaslamp : MonoBehaviour

{
    [SerializeField] private int gaslightNumber;
    [SerializeField] private Light gaslight;
    [SerializeField] private Transform clue;
    [SerializeField] private Transform statue;
    [SerializeField] private float originalTimeUntilClue = 200f;
    [SerializeField] private AudioSource dropAudio;

    private float timeUntilClue;
    static public int gaslightNumberOn = 1;
    private int countFloorCollisions = 0;
    MeshRenderer clueMesh;
    MeshRenderer statueMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        timeUntilClue = originalTimeUntilClue;
        statueMesh = statue.GetComponent<MeshRenderer>();
        clueMesh = clue.GetComponent<MeshRenderer>();
        clueMesh.enabled = false;
        if (gaslightNumber == 1){
            gaslight.enabled = true;
            statueMesh.enabled = true;
        }
        else{
            gaslight.enabled = false;
            statueMesh.enabled = false;
        }
        
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
            clueMesh.enabled = false;
            statueMesh.enabled = false;
            timeUntilClue = originalTimeUntilClue;
        }
        else {
            gaslight.enabled = true;
            statueMesh.enabled = true;
            if (timeUntilClue > 0){
                timeUntilClue -= Time.deltaTime;
            }
            else {
                clueMesh.enabled = !clueMesh.enabled;
                timeUntilClue = 1;
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
