using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject MenuCamera;
    public GameObject MainCamera;
    public GameObject OvenCamera;

    private enum CameraState
    {
        Menu,
        Main,
        Oven
    }

    private CameraState currentCameraState = CameraState.Menu;

    void Start()
    {
        UpdateCameraState();
    }

    public void ToggleMenuCam()
    {
        currentCameraState = currentCameraState == CameraState.Menu ? CameraState.Main : CameraState.Menu;
        UpdateCameraState();
    }

    public void ToggleOvenCam()
    {
        currentCameraState = currentCameraState == CameraState.Oven ? CameraState.Main : CameraState.Oven;
        UpdateCameraState();
    }

    private void UpdateCameraState()
    {
        MenuCamera.SetActive(currentCameraState == CameraState.Menu);
        MainCamera.SetActive(currentCameraState == CameraState.Main);
        OvenCamera.SetActive(currentCameraState == CameraState.Oven);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleMenuCam();
        }
    }

    public void ToggleMainCam()
    {
        currentCameraState = CameraState.Main;
        UpdateCameraState();
    }
}
