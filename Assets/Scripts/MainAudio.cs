using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    [SerializeField] private float timerUntilPlay;
    [SerializeField] private bool isOn;
    private float timer;
    static bool stopAll = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isOn){
            timer = timerUntilPlay * 2;
        }
        else {
            timer = timerUntilPlay;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (stopAll){
            transform.GetComponent<AudioSource>().Stop();
        }
        else if (timer > 0){
            timer -= Time.deltaTime;
        }
        else {
            transform.GetComponent<AudioSource>().Play();
            timer = timerUntilPlay * 2;
        }
      
    }

    static public void StopAll(){
        stopAll = true;
    }
}
