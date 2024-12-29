using System.Collections;
using UnityEngine;

public interface IGameObserver
{
    void OnGameStateChanged(GameManager.GameState gameState);
}

[RequireComponent(typeof(MusicManager))]
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private DailyMission dailyMission;

    [Header("Game GUI")]
    [SerializeField] private GameObject gameGUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endOfDayMenu;
    [SerializeField] private GameObject startDayMenu;
    [SerializeField] private GameObject breakTimeMenu;
    [SerializeField] private GameObject transitionScene;

    [Header("Game Components")]
    [SerializeField] private GameObject laptop;
    [SerializeField] private GameObject oven;
    [SerializeField] private GameObject bank;

    public enum GameState 
    { 
        StartDay, 
        Pause, 
        Play, 
        EndOfDay }

    public static GameManager Instance { get; private set; }
    public MusicManager MusicManager { get; private set; }
    public GameState CurrentGameState { get; private set; } = GameState.StartDay;

    private float orderCooldown = 5f; // Example default value
    private bool isOrderCooldown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        MusicManager = GetComponent<MusicManager>();
        if (MusicManager == null)
        {
            Debug.LogError("MusicManager component is missing.");
        }
    }

    private void Start()
    {
        if (dailyMission == null || gameGUI == null)
        {
            Debug.LogError("One or more required GameManager components are missing.");
        }
        GameStart();
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Play && !isOrderCooldown)
        {
            StartCoroutine(InitOrder());
        }
    }

    private IEnumerator InitOrder()
    {
        isOrderCooldown = true;
        yield return new WaitForSeconds(orderCooldown);
        // Add logic to initialize orders here
        isOrderCooldown = false;
    }

    public void TogglePause()
    {
        if (CurrentGameState == GameState.Pause)
        {
            Time.timeScale = 1;
            CurrentGameState = GameState.Play;
        }
        else
        {
            Time.timeScale = 0;
            CurrentGameState = GameState.Pause;
        }
    }

    public void GameStart()
    {
        if (CurrentGameState == GameState.StartDay)
        {
            CurrentGameState = GameState.Play;
            Time.timeScale = 1;
        }
    }

    public void GameEnd()
    {
        if (CurrentGameState == GameState.Play)
        {
            CurrentGameState = GameState.EndOfDay;
            Time.timeScale = 0;
            // Add end-of-day logic here
        }
    }
}
