using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.XR;

public class DebugConsole : MonoBehaviour
{

    public InputDeviceCharacteristics controllerType;
    public Text console;
    public GameObject debugPlane;
    private InputDevice _controller;
    private bool _foundDevice;
    private bool _isPrimaryPressed;
    private bool _isSecondaryPressed;

    private static DebugConsole _instance;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        Initialise();
    }

    private void Initialise()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerType, devices);
        if (devices.Count.Equals(0))
        {
            //Debug.Log("No device found.");
        }
        else
        {
            _controller = devices[0];
            _foundDevice = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_foundDevice)
        {
            Initialise();
        }
        else
        {
            if(_controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryPressed))
            {
                if(primaryPressed)
                {
                    if(!_isPrimaryPressed)
                    {
                        debugPlane.SetActive(!debugPlane.activeSelf);
                    }
                    _isPrimaryPressed = true;
                }
                else
                {
                    _isPrimaryPressed = false;
                }
            }
            if(_controller.TryGetFeatureValue(CommonUsages.secondaryButton, out bool seconderyPressed))
            {
                if(seconderyPressed)
                {
                    if(!_isSecondaryPressed)
                    {
                        console.text = "";
                    }
                    _isSecondaryPressed = true;
                }
                else
                {
                    _isSecondaryPressed = false;
                }
            }
        }
    }

    public static DebugConsole GetDebugConsole()
    {
        return _instance;
    }

    public static void Log(string message)
    {
        _instance.console.text += message + "\n";
        // Debug.Log(message);
    }
}
