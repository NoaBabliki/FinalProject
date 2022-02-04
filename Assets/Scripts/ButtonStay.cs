using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStay : MonoBehaviour
{

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
         if ((Mathf.Abs(transform.localPosition.x - startPos.x) > 0.001f) ||
          (Mathf.Abs(transform.localPosition.z - startPos.z) > 0.001f) ||
           (transform.localPosition.y - startPos.y > 0.01f)){
            transform.localPosition = startPos;
            Debug.Log("out of place");
        }
        
    }

    private void OnCollisionStay(Collision other) {
        
        Debug.Log("stay + " + other.gameObject.name);
        
       
    }
}
