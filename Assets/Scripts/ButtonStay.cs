using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStay : MonoBehaviour
{

    private Vector3 startPos;
    private float maxYDistance = 0.0085f;
    private float maxXDistance = 0.005f;
    private float maxZDistance = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
         if ((Mathf.Abs(transform.localPosition.x - startPos.x) > maxXDistance) ||
          (Mathf.Abs(transform.localPosition.z - startPos.z) > maxZDistance) ||
           (transform.localPosition.y - startPos.y > maxYDistance)){
            transform.localPosition = startPos;
            Debug.Log("out of place");
        }
        
    }

    private void OnCollisionStay(Collision other) {
        Debug.Log("stay + " + other.gameObject.name);
    }
}
