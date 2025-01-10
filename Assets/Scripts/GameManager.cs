using System.Collections;
using System.Collections.Generic;
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
    // [SerializeField] private OrderClass orderClass;

    [Header("Game GUI")]
    [SerializeField] private GameObject gameGUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endOfDayMenu;
    [SerializeField] private GameObject startDayMenu;
    [SerializeField] private GameObject breakTimeMenu;
    [SerializeField] private GameObject transitionScene;
    [SerializeField] private GameObject BakeFinished;

    [Header("Game Components")]
    [SerializeField] private GameObject laptop;
    [SerializeField] private Bank bank;
    [SerializeField] private GameObject messages;
    [SerializeField] private Clock clock; // Add reference to Clock
    [SerializeField] private string NoRekening = "1234567890";
    [SerializeField] private string PlayerName = "Budi";

    // Remove the ovens array
    // [SerializeField] private Oven[] ovens;

    public enum GameState
    {
        StartDay,
        Pause,
        Play,
        EndOfDay
    }

    public static GameManager Instance { get; private set; }
    public MusicManager MusicManager { get; private set; }
    public GameState CurrentGameState { get; private set; } = GameState.StartDay;

    private float orderCooldown = 5f; // Example default value
    private bool isOrderCooldown = false;

    private List<OrderClass> orders = new List<OrderClass>();

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

        if (bank == null)
        {
            Debug.LogError("Bank component is missing.");
            bank = FindAnyObjectByType<Bank>();
        }
        if(clock == null)
        {
            Debug.LogError("Clock component is missing.");
            clock = FindAnyObjectByType<Clock>();
        }

        GetCurrentTime();
        GetCurrentDate();
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

        if (clock == null)
        {
            Debug.LogError("Clock component is missing.");
            clock = FindAnyObjectByType<Clock>();
        }

        AssignFoodSO();
        GameStart();

        // Initialize orders
        InitializeOrders();
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

    private void AssignFoodSO()
    {
        Oven[] ovens = FindObjectsOfType<Oven>();
        foreach (Oven oven in ovens)
        {
            oven.SetFoodSO(foodSO);
        }
    }

    private void InitializeOrders()
    {
        for (int i = 0; i < 1; i++) // Example: Create 5 orders
        {
            // GameObject orderObject = new GameObject("Order" + i);
            GameObject orderObject = new GameObject("Order");
            OrderClass order = orderObject.AddComponent<OrderClass>();
            order.SetFoodSO(foodSO);
            orders.Add(order);
        }
    }

    // We did a little trolling
    public string GetCurrentDate()
    {
        Debug.Log("Current date: " + System.DateTime.Now.ToString("dd/MM/yyyy"));
        Debug.Log("Daily mission date: " + System.DateTime.Now.AddDays(dailyMission.missionDay - 1).ToString("dd/MM/yyyy"));
        return System.DateTime.Now.AddDays(dailyMission.missionDay - 1).ToString("dd/MM/yyyy");
    }

    public string GetCurrentTime()
    {
        if (clock != null)
        {
            return clock.StringGetTime();
        }
        else
        {
            Debug.LogError("Clock component is missing.");
            return string.Empty;
        }
    }

    public string GetRekening()
    {
        return NoRekening;
    }

    public string GetPlayerName()
    {
        return PlayerName;
    }
    // [YarnCommand("DepositToBank")]
    // public void DepositToBank(int amount)
    // {
    //     bank.Deposit(amount);
    // }

    // // [YarnCommand("WithdrawFromBank")]
    // public bool WithdrawFromBank(int amount)
    // {
    //     if (bank.Withdraw(amount))
    //     {
    //         return true;
    //     }
    //     return false;
    // }
}
