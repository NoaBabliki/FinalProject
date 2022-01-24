using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimationController : MonoBehaviour
{
    public InputDeviceCharacteristics controllerType;
    private InputDevice _thisController;

    public Animator _animationController;
    private bool _isControllerFound;

    // Start is called before the first frame update
    void Start()
    {
        _animationController = GetComponent<Animator>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isControllerFound)
        {
            Initialize();
        }
        else
        {
            if (_thisController.TryGetFeatureValue(CommonUsages.trigger, out float triggetValue))
            {
                _animationController.SetFloat("Trigger", triggetValue);
            }
            if (_thisController.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                _animationController.SetFloat("Grip", gripValue);
            }
        }
    }

    private void Initialize()
    {
        List<InputDevice> xrDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerType, xrDevices);
        if (xrDevices.Count.Equals(0))
        {
            Debug.Log("No XR devices");
        }
        else 
        {
            _thisController = xrDevices[0];
            _isControllerFound = true;
        }
    }
}
