using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GaslightButton : MonoBehaviour
{

    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] private int gaslampNumber;
    [SerializeField] private AudioSource switchSound;
    //[SerializeField] private SpringJoint joint;
    
    private float distanceWhenPressed = 0.0075f;
    private bool isPressed;
    private Vector3 startPos;
    
    //public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.parent.localPosition;//transform.localPosition;
        //joint = GetComponent<ConfigurableJoint>();
    }

    private void Pressed(){
        if (gaslampNumber != Gaslamp.gaslightNumberOn){
            isPressed = true;
            Gaslamp.gaslightNumberOn = gaslampNumber;
            switchSound.Play();
            Debug.Log("pressed" + gaslampNumber);
        }
        
    }

    private void Released(){
        isPressed = false;
        Debug.Log("releades" + gaslampNumber);

    }

    // Update is called once per frame
    void Update()
    {
        bool isCurrentlyPressed;
        if ((transform.parent.localPosition.y - startPos.y) > distanceWhenPressed)
        {
            isCurrentlyPressed = true;
        }
        else
        {
            isCurrentlyPressed = false;
        }
        if (!isPressed && isCurrentlyPressed)
        {
            Pressed();
        }
        if (isPressed && !isCurrentlyPressed)
        {
            Released();
        }
        // if (!isPressed && GetValue() + threshold >= 1){
        //     Pressed();
        // }
        // if (isPressed && GetValue() - threshold <= 0){
        //     Released();
        // }
       
    }

    private float GetValue(){
        var value = Vector3.Distance(startPos, transform.parent.localPosition) / distanceWhenPressed; // / joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadZone){
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);  
    }

    
}