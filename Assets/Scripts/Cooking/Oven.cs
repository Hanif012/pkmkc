using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Lean.Gui;

public class Oven : MonoBehaviour
{
    private enum OvenState
    {
        Off,
        Cooking,
        FoodReady
    }

    [Header("SO")]
    [SerializeField] public Foods FoodSO;
    [SerializeField] private FoodButton TheFood;
    [Header("UI Settings")]
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private TextMeshProUGUI ovenStateText;
    [SerializeField] private GameObject FoodCooked;


    [Header("Oven Settings")]
    [SerializeField] private float cookingTime;
    [SerializeField] private float cookingSpeed = 1f;
    [SerializeField] private GameObject OvenButton;
    private float cookingProgress = 0f;
    private Inventory inventory;

    private OvenState currentState = OvenState.Off;
    private bool isFoodInOven = false;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void ToggleOven()
    {
        switch (currentState)
        {
            case OvenState.Off:
                currentState = OvenState.Cooking;
                Debug.Log("Oven is now cooking.");
                break;
            case OvenState.Cooking:
                Debug.Log("Food is in the oven, please wait for it to finish cooking.");
                break;
            case OvenState.FoodReady:
                Debug.Log("Food is ready! Turn off the oven or remove the food.");
                break;
        }
    }

    public void AddFoodToOven(FoodButton foodButton)
    {
        isFoodInOven = true;
        TheFood = foodButton;
        Debug.Log("Food is added to the oven.");
    }

    public void RemoveFoodFromOven(FoodButton foodButton)
    {
        isFoodInOven = false;
        currentState = OvenState.Off;
        inventory.AddFood(foodButton.food); // Add food to inventory
        Debug.Log("Food is removed from the oven and added to inventory.");
    }

    public void SetFoodSO(Foods foodSO)
    {
        this.FoodSO = foodSO;
        FetchFoodSO();
    }

    private void FetchFoodSO()
    {
        foreach (Food food in FoodSO)
        {
            GameObject foodItem = Instantiate(ButtonPrefab, contentPanel);
            foodItem.GetComponent<FoodButton>().foodName = food.foodName;
            foodItem.GetComponent<FoodButton>().foodImage = food.foodImage;
            foodItem.GetComponent<FoodButton>().cookingTime = food.cookingTime;
            foodItem.GetComponent<FoodButton>().foodDescription = food.foodDescription;
            foodItem.GetComponent<FoodButton>().cost = food.cost;
            foodItem.GetComponent<FoodButton>().food = food;
            foodItem.SetActive(true);
        }
    }

    void Update()
    {
        if (currentState == OvenState.Cooking)
        {
            ovenStateText.text = "Timer:" + " " + cookingProgress.ToString("F0") + " / " + cookingTime;
        }
        else if (currentState == OvenState.Off)
        {
            ovenStateText.text = "Oven is Off";
        }
        else if (currentState == OvenState.FoodReady)
        {
            ovenStateText.text = "Food is Ready";
        }    
    }

    public void StartCooking(FoodButton foodButton)
    {
        if (currentState == OvenState.Off)
        {
            isFoodInOven = true;
            currentState = OvenState.Cooking;
            cookingProgress = 0f; // Reset cooking progress
            cookingTime = foodButton.cookingTime; // Update cookingTime according to the pressed button
            StartCoroutine(CookFood(foodButton));
            Debug.Log("Started cooking " + foodButton.foodName);
        }
        else
        {
            Debug.Log("Oven is already in use.");
        }
    }

    private IEnumerator CookFood(FoodButton foodButton)
    {
        Debug.Log("Cooking " + foodButton.foodName + "...");
        float cookingTime = foodButton.cookingTime;

        while (currentState == OvenState.Cooking)
        {
            yield return new WaitForSeconds(1f);
            cookingProgress += GameTime.Instance.timeIncrement * cookingSpeed;

            if (cookingProgress >= cookingTime)
            {
                cookingProgress = 0f;
                currentState = OvenState.FoodReady;
                Debug.Log(foodButton.foodName + " is cooked and ready!");
            }
        }
    }

    public void OnClick() 
    {
        if (currentState == OvenState.FoodReady)
        {
            RemoveFoodFromOven(TheFood);
            //TODO: ADD Inventory
            //TODO:and Bread Shiny
        }
        else if (currentState == OvenState.Cooking)
        {
            // Debug.Log("Food is still cooking.");
        }
        else if (currentState == OvenState.Off)
        {
            // Debug.Log("Oven is off."); 
            GetComponent<LeanWindow>().TurnOn();
        }
    }
}