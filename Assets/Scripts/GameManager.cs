// using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static int numberOfStages = 4;
    [SerializeField] private Light largeSpot;
    [SerializeField] private Light directionalSpot;
    [SerializeField] private Material successSkybox;
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private GameObject successObject;
    private static GameObject[,] objectsForMainGame = new GameObject[numberOfStages, 2];
    private static GameObject[][] furnitureForStages = new GameObject[numberOfStages][];
    private static AudioSource[] audioOfAllStages = new AudioSource[numberOfStages];
    static private int successCount = 0;
    private Light _spotLightIntencity;
    private Light _directionalLightIntencity;
    private float _maxIntensity;
    private float _maxIntensityD;
    private bool _winFlag = false;
    private static int _activateStageSequence = -1;

    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        _spotLightIntencity = largeSpot.GetComponent<Light>();
        _directionalLightIntencity = directionalSpot.GetComponent<Light>();
        _maxIntensity = _spotLightIntencity.intensity;
        _maxIntensityD = _directionalLightIntencity.intensity;
        _spotLightIntencity.intensity = 0f;
        _directionalLightIntencity.intensity = 0f;
        Debug.Log("hi");
        initializeAudioOfAllStages();
        Debug.Log("bi");
        successObject.SetActive(false);
        initializeObjectsForMainGame();
        initializeFurnitureForStages();
        SetFurnitureForStages(false);
        SetMainGameObjectsActive(false);
        SetMainGameObjectsActive(true, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime)
        {
            firstTime = false;
            DebugConsole.Log("-0");
            initializeAudioOfAllStages();
            DebugConsole.Log("-1");
            successObject.SetActive(false);
            DebugConsole.Log("-2");
            initializeObjectsForMainGame();
            DebugConsole.Log("-3");
            initializeFurnitureForStages();
            DebugConsole.Log("-4");
            SetFurnitureForStages(false);
            DebugConsole.Log("-5");
            SetMainGameObjectsActive(false);
            DebugConsole.Log("-6");
            SetMainGameObjectsActive(true, 1);
            DebugConsole.Log("-7");
        }
        // check win
        if (_activateStageSequence != -1)
        {
            DebugConsole.Log("activating stage " + _activateStageSequence.ToString());
            StartCoroutine(SetStageSequence(_activateStageSequence));
            _activateStageSequence = -1;
        }

        if (successCount == numberOfStages)
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
        DebugConsole.Log("ITS A WIN");
        RenderSettings.skybox = successSkybox;
        Gaslamp.gaslightNumberOn = 0;
        successObject.SetActive(true);
        
        GameObject[] sculptures = GameObject.FindGameObjectsWithTag("Sculpture");
        foreach (GameObject sculpture in sculptures)
        {
            sculpture.SetActive(false);
        }
        MainAudio.StopAll();
        winAudio.GetComponent<AudioSource>().Play();
    }

    private static void initializeAudioOfAllStages()
    {
        List<AudioSource> Children = new List<AudioSource>();
        GameObject[] goParent = GameObject.FindGameObjectsWithTag("StageAudio");
        foreach (GameObject child in goParent)
        {
            Children.Add(child.GetComponent<AudioSource>());
        }
        audioOfAllStages = Children.ToArray();
        // audioOfAllStages.Sort(audioOfAllStages, function(g1, g2) String.Compare(g1.name, g2.name));
    }

    private static void initializeFurnitureForStages()
    {
        DebugConsole.Log("Initializing furnitures");
        for (int i = 1 ; i <= numberOfStages ; i++) 
        {
            DebugConsole.Log("Initializing furnitures for stage " + i.ToString());
            furnitureForStages[i - 1] = GameObject.FindGameObjectsWithTag("Furniture_Stage_" + i.ToString());

            // Random random = new Random();
            // furnitureForStages[i - 1] = furnitureForStages[i - 1].OrderBy(x => random.Next()).ToArray();

            // // Shuffle the array of objects
            // Random rand = new Random();
            // // For each spot in the array, pick
            // // a random item to swap into that spot.
            // for (int i = 0; i < furnitureForStages[i - 1].Length - 1; i++)
            // {
            //     int j = rand.Next(i, furnitureForStages[i - 1].Length);
            //     GameObject temp = furnitureForStages[i - 1][i];
            //     furnitureForStages[i - 1][i] = furnitureForStages[i - 1][j];
            //     furnitureForStages[i - 1][j] = temp;
            // }

            DebugConsole.Log("Got " + furnitureForStages[i - 1].Length.ToString() + " objects");
        }
    }

    private static void SetFurnitureForStages(bool activeValue, int stage = -1)
    {
        for (int i = 0; i < furnitureForStages.Length; i++)
        {
            DebugConsole.Log("Going over furnitures for stage " + (i + 1).ToString() + "called with " + stage.ToString());
            if (stage == (i + 1) || stage == -1)
            {
                for(int j = 0; j < furnitureForStages[i].Length; j++)
                {
                    DebugConsole.Log("i = " + i.ToString() + ", j = " + j.ToString() + ", item = " + furnitureForStages[i][j].name);
                    furnitureForStages[i][j].SetActive(activeValue);
                    if (activeValue)
                    {
                        DebugConsole.Log("Setting " + furnitureForStages[i][j] + "active");
                    }
                    else
                    {
                        DebugConsole.Log("Setting " + furnitureForStages[i][j] + "inactive");
                    }
                }
            }
        }
    }

    static IEnumerator SetStageSequence(int stage)
    {
        DebugConsole.Log("set sequence");
        MainAudio.decreaseVolume();
        audioOfAllStages[stage - 1].Play();
        if (stage != 1)
        {
            DebugConsole.Log("De activating last stage");
            SetFurnitureForStages(false, stage - 1);
        }
        float clipLength = audioOfAllStages[stage - 1].clip.length;
        float deltaTime = clipLength / furnitureForStages[stage - 1].Length;
        for (int i = 0; i < furnitureForStages[stage - 1].Length; i++)
        {
            DebugConsole.Log("Adding object number " + i.ToString());
            furnitureForStages[stage - 1][i].SetActive(true);
            yield return new WaitForSeconds(deltaTime);
        }
        if (stage != numberOfStages) 
        {
            DebugConsole.Log("activating next stage");
            SetMainGameObjectsActive(true, stage + 1);
        }
        yield return null;
    }

    private static void initializeObjectsForMainGame() 
    {
        DebugConsole.Log("initializing main");
        for (int i = 1; i <= objectsForMainGame.GetLength(0); i++)
        {
            GameObject [] currGameObjects = GameObject.FindGameObjectsWithTag("Stage_" + i.ToString());
            if (currGameObjects.Length != 2)
            {
                DebugConsole.Log("Wrong number of object for stage " + i.ToString() + ". Got " + currGameObjects.Length.ToString() + " items.");
            }
            for (int j = 0; j < currGameObjects.Length; j ++)
            {
                DebugConsole.Log("Got " + currGameObjects[j].name);
                objectsForMainGame[i - 1, j] = currGameObjects[j];
            }
        }
    }

    // no stage means for all stages
    private static void SetMainGameObjectsActive(bool activeValue, int stage = -1)
    {
        DebugConsole.Log("Number of rows: " + objectsForMainGame.GetLength(0).ToString());
        DebugConsole.Log("Number of cols: " + objectsForMainGame.GetLength(1).ToString());
        for (int i = 0; i < objectsForMainGame.GetLength(0); i++)
        {
            if (stage == (i + 1) || stage == -1)
            {
                for(int j = 0; j < objectsForMainGame.GetLength(1); j++)
                {
                    DebugConsole.Log("i = " + i.ToString() + ", j = " + j.ToString() + ", item = " + objectsForMainGame[i, j].name);
                    objectsForMainGame[i, j].SetActive(activeValue);
                    if (activeValue)
                    {
                        DebugConsole.Log("Setting " + objectsForMainGame[i, j] + "active");
                    }
                    else
                    {
                        DebugConsole.Log("Setting " + objectsForMainGame[i, j] + "inactive");
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

    private static void PrintStatus()
    {
        List<AudioSource> Children = new List<AudioSource>();
        GameObject[] goParent = GameObject.FindGameObjectsWithTag("StageAudio");
        foreach (GameObject child in goParent)
        {
            DebugConsole.Log("Adding voice " + child.name);
            Children.Add(child.GetComponent<AudioSource>());
        }
        audioOfAllStages = Children.ToArray();
        DebugConsole.Log("finished audio");


        DebugConsole.Log("Initializing furnitures");
        for (int i = 1 ; i <= numberOfStages ; i++) 
        {
            DebugConsole.Log("Initializing furnitures for stage " + i.ToString());
            furnitureForStages[i - 1] = GameObject.FindGameObjectsWithTag("Furniture_Stage_" + i.ToString());
            DebugConsole.Log("Got " + furnitureForStages[i - 1].Length.ToString() + " objects");
        }
    }

    public static void AddSuccess(int gaslumpNumber)
    // public static void AddSuccess() 
    {
        DebugConsole.Log("Add Success " + gaslumpNumber.ToString());
        // PrintStatus();
        _activateStageSequence = gaslumpNumber;
        successCount += 1;
    }
}
