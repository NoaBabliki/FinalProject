using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStay : MonoBehaviour
{

    private Vector3 startPos;
    private float maxYDistance = 0.001f;
    private float maxXDistance = 0.003f;
    private float maxZDistance = 0.003f;

    private float maxDownDistance = -0.03f;
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
           (transform.localPosition.y - startPos.y > maxYDistance) ||
            (transform.localPosition.y - startPos.y < maxDownDistance)){
            transform.localPosition = startPos;
            Debug.Log("out of place");
        }
        
    }

    private void OnCollisionStay(Collision other) {
        Debug.Log("stay + " + other.gameObject.name);
    }
}
