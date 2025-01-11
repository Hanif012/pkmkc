using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject PauseMenuUI;

    public GameObject PauseButton;

    private NewAudioManager audioManager;

    private CameraController cameraController;

    public GameObject MenuCanvas;

    public GameObject SettingsCanvas;

    public GameObject CreditCanvas;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<NewAudioManager>();

        cameraController = GameObject.FindObjectOfType<CameraController>();
    }
    void Update()
    {
        if(MenuCanvas.gameObject.activeSelf || SettingsCanvas.gameObject.activeSelf || CreditCanvas.gameObject.activeSelf)
       {
            
       }
       else
       {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(GamePaused)
                {
                    Resume();
                }
                else
                {
                   Pause();
                }
            }
       }
        
    }

    public void Resume()
    {
        // PauseMenuUI.SetActive(false);
        // PauseButton.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
        audioManager.ResumeBackgroundMusic();
    }

    public void Pause()
    {
        // PauseMenuUI.SetActive(true);
        // PauseButton.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        audioManager.PauseBackgroundMusic();
    }

    public void MainMenu()
    {
        // PauseMenuUI.SetActive(false);
        // PauseButton.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
        cameraController.ToggleMenuCam();
    }
}
