using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private static bool GamePaused = false;
    public GameObject MenuCanvas;
    public GameObject MenuCamera;
    private NewAudioManager audioManager;

    void Start()
    {

    }
    public void Play()
    {
        MenuCamera.SetActive(false);
        MenuCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Update()
    {
       
    }
}
