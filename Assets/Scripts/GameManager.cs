using System.Collections;
using System.ComponentModel.Design;
using UnityEngine;
using Yarn.Unity;

public interface IGameObserver
{
    void OnGameStateChanged(GameManager.GameState gameState);
}

[RequireComponent(typeof(MusicManager))]
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private DailyMission dailyMission;
    [SerializeField] private Foods foodSO;

    [Header("Game GUI")]
    [SerializeField] private GameObject gameGUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endOfDayMenu;
    [SerializeField] private GameObject startDayMenu;
    [SerializeField] private GameObject breakTimeMenu;
    [SerializeField] private GameObject transitionScene;

    [Header("Game Components")]
    [SerializeField] private GameObject laptop;
    [SerializeField] private GameObject[] ovens;
    [SerializeField] private GameObject bank;
    [SerializeField] private GameObject messages;
    // [SerializeField] private GameObject ;

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
        AssignFoodSO();
    }

    private void Start()
    {
        if (dailyMission == null || gameGUI == null)
        {
            Debug.LogError("One or more required GameManager components are missing.");
        }
        else
        {
            var messageComponent = messages.GetComponent<Message>();
            if (messageComponent != null)
            {
                messageComponent.FetchGameManagerToMessages(dailyMission);
            }
            else
            {
                Debug.LogError("Message component is missing on the messages GameObject.");
            }
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
    public void CheckOrder()
    {
        
    }

    private void AssignFoodSO()
    {
        foreach (GameObject oven in ovens)
        {
            Oven ovenComponent = oven.GetComponent<Oven>();
            if (ovenComponent != null)
            {
                ovenComponent.SetFoodSO(foodSO);
            }
            else
            {
                Debug.LogError("Oven component is missing on one of the oven GameObjects.");
            }
        }
        // Add similar assignments for other components if needed
    }
}
