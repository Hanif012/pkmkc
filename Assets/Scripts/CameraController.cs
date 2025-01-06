using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject MenuCamera;
    public GameObject MainCamera;
    public GameObject OvenCamera;


    private bool MenuCheck = true;

    private bool OvenCheck = false;
    void Start()
    {
        
    }

    public void MenuCam()
    {
        if(MenuCheck)
        {
            MenuCamera.SetActive(false);
        }
        else
        {
            MenuCamera.SetActive(true);
        }
    }

    public void OvenCam()
    {
        if(OvenCheck)
        {
            OvenCamera.SetActive(false);
            MainCamera.SetActive(true);
            OvenCheck = false;
        }
        else
        {
            OvenCamera.SetActive(true);
            MainCamera.SetActive(false);
            OvenCheck = true;
        }        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(MenuCheck)
            {
                MenuCam();
            }
        }
    }
}
