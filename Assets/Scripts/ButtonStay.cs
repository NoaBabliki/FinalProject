using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStay : MonoBehaviour
{

    private Vector3 startPos;
    private float maxYDistance = 0.0085f;
    private float maxXDistance = 0.005f;
    private float maxZDistance = 0.005f;
    private Vector3 pushedPos;
    // Start is called before the first frame update
    void Start()
    {
        //First, Find the Parent Object which is either EnemyObject or EnemyObject(Clone)
        Transform button = transform.parent;
        Transform buttonSpring = button.parent;
        //Now, Find it's Enemy Object
        GameObject wantedPosGO = buttonSpring.FindChild("PushedPosition").gameObject;
        pushedPos = wantedPosGO.transform;
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
