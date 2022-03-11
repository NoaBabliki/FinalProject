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
    [SerializeField] private GameObject successObject;
    [SerializeField] private GameObject[] sculptures;
    private static GameObject[] objectsForMainGame = new GameObject[5];
    static private int successCount = 0;
    private Light _spotLightIntencity;
    private Light _directionalLightIntencity;
    private float _maxIntensity;
    private float _maxIntensityD;
    private MeshRenderer _successObjectMesh;
    private bool _winFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        _spotLightIntencity = largeSpot.GetComponent<Light>();
        _directionalLightIntencity = directionalSpot.GetComponent<Light>();
        _maxIntensity = _spotLightIntencity.intensity;
        _maxIntensityD = _directionalLightIntencity.intensity;
        _spotLightIntencity.intensity = 0f;
        _directionalLightIntencity.intensity = 0f;
        // _successObjectMesh = successObject.GetComponent<MeshRenderer>();
        // _successObjectMesh.enabled = false;
        successObject.SetActive(false);
        initiateObjectsForMainGame();
        SetMainGameObjectsActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check win
        if (successCount == overallSuccess)
        {
            if (!_winFlag)
            {
                _winFlag = true;
                Win();
            }
            if (_spotLightIntencity.intensity <= _maxIntensity){
                _spotLightIntencity.intensity += 0.05f * Time.deltaTime;  // Time.deltaTime = time[s] from last frame
                _directionalLightIntencity.intensity += 0.05f * Time.deltaTime;  // Time.deltaTime = time[s] from last frame
            }

            if (_directionalLightIntencity.intensity <= _maxIntensityD){
                _directionalLightIntencity.intensity += Time.deltaTime;  // Time.deltaTime = time[s] from last frame
            }
        }
    }

    private void Win()
    {
        Debug.Log("ITS A WIN");
        RenderSettings.skybox = successSkybox;
        Gaslamp.gaslightNumberOn = 0;
        // _successObjectMesh.enabled = true;
        successObject.SetActive(true);
            
        for (int i=0; i < sculptures.Length; i++)
        {
            Destroy(sculptures[i]);
        }

        MainAudio.StopAll();
        winAudio.GetComponent<AudioSource>().Play();
    }

    private static void initiateObjectsForMainGame() 
    {
        objectsForMainGame[0] = GameObject.Find("Gaslamps/GaslampMustache");
        objectsForMainGame[1] = GameObject.Find("Gaslamps/GaslampCockroach");
        objectsForMainGame[2] = GameObject.Find("DaliStudio/Sculptures/Mustache_2");
        objectsForMainGame[3] = GameObject.Find("DaliStudio/Sculptures/Cockroach_3");
        objectsForMainGame[4] = GameObject.Find("LightAfterFirstSuccess");
    }

    private static void SetMainGameObjectsActive(bool activeValue)
    {
        for(int i = 0; i < objectsForMainGame.Length; i++)
        {
            objectsForMainGame[i].SetActive(activeValue);
        }
    }

    public static void AddSuccess(int gaslumpNumber)
    // public static void AddSuccess() 
    {
        if (gaslumpNumber == 1) 
        {
            SetMainGameObjectsActive(true);
        }
        successCount += 1;
    }
}
