using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButton : MonoBehaviour
{
    [SerializeField] private Animator _buttonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //_buttonAnimator = GetComponent<Animator>();
        //_buttonAnimator.SetTrigger("ButtonPressed");
        _buttonAnimator.Play("SimpleButton", 0, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "player") {
           // _buttonAnimator.SetTrigger("ButtonPressed");
        //}
    }
}
