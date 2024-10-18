using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[RequireComponent(typeof(MusicManager))]
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        StartDay,
        Pause,
        BreakTime,
        Play,
        EndOfDay
    }
    [SerializeField] private GameObject gameGUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endOfDayMenu;
    [SerializeField] private GameObject startDayMenu;
    [SerializeField] private GameObject breakTimeMenu;
    [SerializeField] private GameObject TransitionScene;
    [SerializeField] private DailyMission dailyMission;

    // public GameTime time = new(8, 0);
    public static GameManager Instance { get; private set; }
    public MusicManager musicManager;
    public GameState gameState = GameState.StartDay;
    
    private float orderCooldown = 0;

    // bool collection
    private bool isOrderCooldown = false;
    // private bool isStartDay = true;
    
    void Awake()
    {
        //Fetch data for mission
        dailyMission = GetComponent<DailyMission>();

        Instance = this;
        musicManager = GetComponent<MusicManager>();
        if(orderCooldown == 0)
        {
            Debug.LogWarning("Order Cooldown is 0");
        }

    }
    void Start()
    {
        GameStart();
    }

    void Update()
    {
        if(gameState == GameState.Play)
        {
            StartCoroutine(InitOrder());
        }
    }
    // Will be called in even on pause
    void FixedUpdate()
    {
        if(gameState == GameState.Pause)
        {
            // ToDo : Pause Menu
        }
    }

    public IEnumerator InitOrder()
    {
        
        if (gameState == GameState.Play && !isOrderCooldown)
        {
            yield return new WaitForSeconds(orderCooldown);
        }     
    }

    public void TogglePause()
    {
        if (gameState == GameState.Pause)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void GameStart()
    {
        if(gameState == GameState.StartDay)
        {
            TogglePause();
            gameState = GameState.Play;
            TogglePause();
        }
    }
    
    public void GameEnd()
    {
        if(gameState == GameState.EndOfDay)
        {
            TogglePause();



        }
    }

}
