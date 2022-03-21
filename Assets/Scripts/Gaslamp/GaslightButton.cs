using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GaslightButton : MonoBehaviour
{
    [SerializeField] private int gaslampNumber;
    [SerializeField] private AudioSource switchSound;

    [SerializeField] private GameObject brotherButtonElement;

    private float distanceWhenPressed = 0.003f;
    private bool isPressed = false;

    private Vector3 pushedPos;
    void Start()
    {
        // First, Find the Parent Object which has the pushed position
        Transform button = transform.parent;
        Transform buttonSpring = button.parent;
        // Now, Find it's pushed position Object
        GameObject wantedPosGameObject = buttonSpring.Find("ButtonPushed").gameObject;
        pushedPos = wantedPosGameObject.transform.localPosition;
    }

    private void Pressed(){
        if (gaslampNumber != Gaslamp.gaslightNumberOn){
            isPressed = true;
            Gaslamp.gaslightNumberOn = gaslampNumber;
            switchSound.Play();
            Debug.Log("pressed" + gaslampNumber);
        }
        else 
        {
            isPressed = true;
            Gaslamp.gaslightNumberOn = 0;
            switchSound.Play();
            Debug.Log("unPressed" + gaslampNumber);
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
        float dist = Vector3.Distance(pushedPos, transform.parent.localPosition);
        if (dist < distanceWhenPressed)
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
    }
}
