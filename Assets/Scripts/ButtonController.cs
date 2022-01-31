using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Animator Button = null;

    [SerializeField] private bool downTrigger = false;
    [SerializeField] private bool upTrigger = false;

    [SerializeField] private string buttonDown = "ButtonPressDown";
    [SerializeField] private string buttonUp = "ButtonPressUp";

    // Start is called before the first frame update
    void Start()
    {
        //Button.Play(buttonUp, 0, 0.0f);
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("h");
    }

    void OnCollisionEnter(Collision other)
    {
        //Button.Play(buttonUp, 0, 0.0f);
        Button.SetTrigger("a");
    }
    void OnCollisionExit(Collision other)
    {
        //Button.Play(buttonUp, 0, 0.0f);
    }
}
