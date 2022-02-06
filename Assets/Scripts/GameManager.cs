using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int overallSuccess = 3;
    [SerializeField] private Light largeSpot;
    [SerializeField] private Light directionalSpot;
    [SerializeField] private Material successSkybox;
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private Transform successObject;
    [SerializeField] private GameObject[] sculptures;
    static private int successCount = 0;
    private Light _spotLightIntencity;
    private Light _directionalLightIntencity;
    private float _maxIntensity;
    private float _maxIntensityD;
    private MeshRenderer _successObjectMesh;
    // Start is called before the first frame update
    void Start()
    {
        _spotLightIntencity = largeSpot.GetComponent<Light>();
        _directionalLightIntencity = directionalSpot.GetComponent<Light>();
        _maxIntensity = _spotLightIntencity.intensity;
        _maxIntensityD = _directionalLightIntencity.intensity;
        _spotLightIntencity.intensity = 0f;
        _directionalLightIntencity.intensity = 0f;
        _successObjectMesh = successObject.GetComponent<MeshRenderer>();
        _successObjectMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check win
        if (successCount == overallSuccess){
            RenderSettings.skybox = successSkybox;
            Gaslamp.gaslightNumberOn = 0;
            _successObjectMesh.enabled = true;
            
            if (_spotLightIntencity.intensity <= _maxIntensity){
                _spotLightIntencity.intensity += 0.1f * Time.deltaTime;  // Time.deltaTime = time[s] from last frame
                _directionalLightIntencity.intensity += 0.1f * Time.deltaTime;  // Time.deltaTime = time[s] from last frame
            }

            if (_directionalLightIntencity.intensity <= _maxIntensityD){
                _directionalLightIntencity.intensity += Time.deltaTime;  // Time.deltaTime = time[s] from last frame
            }

            for (int i=0; i < sculptures.Length; i++)
            {
                Destroy(sculptures[i]);
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
