using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    [SerializeField] private float timerUntilPlay;
    [SerializeField] private bool isOn;
    private float timer;
    private enum ChangeVolume
        {
            Decrease,
            Increase,
            Nothing
        }
    private static ChangeVolume changeVolume = ChangeVolume.Nothing;
    private static bool stopAll = false;
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
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else {
            transform.GetComponent<AudioSource>().Play();
            timer = timerUntilPlay * 2;
        }

        switch(changeVolume) 
        {
            case ChangeVolume.Increase:
            transform.GetComponent<AudioSource>().volume = 0.25f;
            changeVolume = ChangeVolume.Nothing;
            break;
            case ChangeVolume.Decrease:
            transform.GetComponent<AudioSource>().volume = 0.1f;
            changeVolume = ChangeVolume.Nothing;
            break;
        }
    }

    public static void decreaseVolume()
    {
        changeVolume = ChangeVolume.Decrease;
    }

    public static void increaseVolume()
    {
        changeVolume = ChangeVolume.Increase;
    }

    public static void StopAll()
    {
        stopAll = true;
    }
}
