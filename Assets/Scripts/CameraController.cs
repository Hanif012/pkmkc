using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject MenuCamera;

    private bool test = true;
    void Start()
    {
        
    }

    public void Menu()
    {
        MenuCamera.SetActive(true);
        test = true;
    }
    private void PlayGame()
    {
        MenuCamera.SetActive(false);
        test = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!test)
            {
                Menu();
            }
            else
            {
                PlayGame();
            }
        }
    }
}
