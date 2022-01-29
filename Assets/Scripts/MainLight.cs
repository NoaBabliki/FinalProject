using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLight : MonoBehaviour
{
    [SerializeField] private Light[] smallLightSources;
    [SerializeField] private Transform floorsAndCeiling;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        for (int i=0; i<smallLightSources.Length; i++){
            if (smallLightSources[i].enabled == false){
                counter += 1;
            }
        }
        if (counter == smallLightSources.Length){
            transform.GetComponent<Light>().intensity += 1;
   
            floorsAndCeiling.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
}
