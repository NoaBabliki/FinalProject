using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int overallSuccess = 3;
    [SerializeField] private Light largeSpot;
    [SerializeField] private Canvas instructions;
   
    [SerializeField] private float instructionsTimer = 30;
    [SerializeField] private AudioSource winAudio;
    static private int successCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check win
        if (successCount == overallSuccess){
            Gaslamp.gaslightNumberOn = 0;
            Light lightIntencity = largeSpot.GetComponent<Light>();
            if (lightIntencity.intensity < 3.0f){
                lightIntencity.intensity += Time.deltaTime;
            }
            Debug.Log("ITS A WIN");
            MainAudio.StopAll();
            winAudio.GetComponent<AudioSource>().Play();
        }
        if (instructionsTimer > 0){
            instructionsTimer -= Time.deltaTime;
        }
        else if (instructionsTimer <= 0) {
            instructions.enabled = false;
        }

        
    }

    public static void AddSuccess() {
        successCount += 1;
    }
}
