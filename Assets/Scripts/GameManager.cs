using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private int overallSuccess = 3;
    [SerializeField] private Light largeSpot;
    [SerializeField] private Light directionalSpot;
    [SerializeField] private Material successSkybox;
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private GameObject successObject;
    [SerializeField] private GameObject[] sculptures;
    [SerializeField] private static int numberOfStage = 6;
    private static GameObject[,] objectsForMainGame = new GameObject[numberOfStage,2];
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
        SetMainGameObjectsActive(true, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // check win
        if (successCount == numberOfStage)
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
        for (int i = 1; i <= objectsForMainGame.GetLength(0); i++)
        {
            GameObject [] currGameObjects = GameObject.FindGameObjectsWithTag("Stage_" + i.ToString());
            if (currGameObjects.Length != 2)
            {
                Debug.Log("Wrong number of object for stage " + i.ToString() + ". Got " + currGameObjects.Length.ToString() + " items.");
            }
            for (int j = 0; j < currGameObjects.Length; j ++)
            {
                Debug.Log("Got " + currGameObjects[j].name);
                objectsForMainGame[i - 1, j] = currGameObjects[j];
            }
            // objectsForMainGame[i - 1] = currGameObjects;
        }
        // objectsForMainGame[0] = GameObject.Find("Gaslamps/GaslampMustache");
        // objectsForMainGame[1] = GameObject.Find("Gaslamps/GaslampCockroach");
        // objectsForMainGame[2] = GameObject.Find("DaliStudio/Sculptures/Mustache_2");
        // objectsForMainGame[3] = GameObject.Find("DaliStudio/Sculptures/Cockroach_3");
        // objectsForMainGame[4] = GameObject.Find("LightAfterFirstSuccess");
    }

    // no stage means for all stages
    private static void SetMainGameObjectsActive(bool activeValue, int stage = -1)
    {
        Debug.Log("Number of rows: " + objectsForMainGame.GetLength(0).ToString());
        Debug.Log("Number of cols: " + objectsForMainGame.GetLength(1).ToString());
        for (int i = 0; i < objectsForMainGame.GetLength(0); i++)
        {
            if (stage == (i + 1) || stage == -1)
            {
                for(int j = 0; j < objectsForMainGame.GetLength(1); j++)
                {
                    Debug.Log("i = " + i.ToString() + ", j = " + j.ToString() + ", item = " + objectsForMainGame[i, j].name);
                    objectsForMainGame[i, j].SetActive(activeValue);
                    if (activeValue)
                    {
                        Debug.Log("Setting " + objectsForMainGame[i, j] + "active");
                    }
                    else
                    {
                        Debug.Log("Setting " + objectsForMainGame[i, j] + "inactive");
                    }
                }
            }
        }
    }

    private static void SetGaslampInactive(int stageNumber)
    {
        for (int i = 0; i < objectsForMainGame.GetLength(1); i++)
        {
            if (objectsForMainGame[stageNumber - 1, i].name.Contains("Gaslamp"))
            {
                objectsForMainGame[stageNumber - 1, i].SetActive(false);
            }
        }
    }

    public static void AddSuccess(int gaslumpNumber)
    // public static void AddSuccess() 
    {
        if (gaslumpNumber != numberOfStage) 
        {
            SetGaslampInactive(gaslumpNumber);
            SetMainGameObjectsActive(true, gaslumpNumber + 1);
        }
        successCount += 1;
    }
}
