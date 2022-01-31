using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int overallSuccess = 3;
    [SerializeField] private Light largeSpot;
    [SerializeField] private Material successSkybox;
    [SerializeField] private AudioSource winAudio;
    static private int successCount = 0;
    private Light _lightIntencity;
    private float _maxIntensity;
    // Start is called before the first frame update
    void Start()
    {
        _lightIntencity = largeSpot.GetComponent<Light>();
        _maxIntensity = _lightIntencity.intensity;
        _lightIntencity.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // check win
        if (successCount == overallSuccess){
            RenderSettings.skybox = successSkybox;
            Gaslamp.gaslightNumberOn = 0;
            
            if (_lightIntencity.intensity <= _maxIntensity){
                _lightIntencity.intensity += 0.1f * Time.deltaTime;  // Time.deltaTime = time[s] from last frame
            }
            Debug.Log("ITS A WIN");
            MainAudio.StopAll();
            winAudio.GetComponent<AudioSource>().Play();
        }
    }

    public static void AddSuccess() {
        successCount += 1;
    }
}
