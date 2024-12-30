using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Oven : MonoBehaviour
{
    private enum OvenState
    {
        Off,
        Cooking,
        FoodReady
    }

    [Header("SO")]
    [SerializeField] private Foods foodSO;

    [Header("UI Settings")]
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private TextMeshProUGUI ovenStateText;


    [Header("Oven Settings")]
    [SerializeField] private GameObject ovenText;
    [SerializeField] private float cookingTime = 10f;
    [SerializeField] private float cookingSpeed = 1f;
    private float cookingProgress = 0f;

    private OvenState currentState = OvenState.Off;
    private bool isFoodInOven = false;

    void Start()
    {
        FetchFoodSO();
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

    public void AddFoodToOven()
    {
        isFoodInOven = true;
        Debug.Log("Food is added to the oven.");
    }

    public void RemoveFoodFromOven()
    {
        isFoodInOven = false;
        currentState = OvenState.Off;
        Debug.Log("Food is removed from the oven.");
    }

    private void FetchFoodSO()
    {
        foreach (Food food in foodSO)
        {
            GameObject foodItem = Instantiate(ButtonPrefab, contentPanel);
            foodItem.GetComponent<FoodButton>().foodName = food.foodName;
            foodItem.GetComponent<FoodButton>().foodImage = food.foodImage;
            foodItem.GetComponent<FoodButton>().cookingTime = food.cookingTime;
            foodItem.GetComponent<FoodButton>().foodDescription = food.foodDescription;
            foodItem.GetComponent<FoodButton>().cost = food.cost;
            Debug.Log("Fetched " + food.foodName + " from SO.");
            foodItem.SetActive(true);
        }
        Debug.Log("Fetched all foods from SO. Happy debugging!");
    }

    void Update()
    {
        if (currentState == OvenState.Cooking)
        {
            ovenStateText.text = "Oven is Cooking" + " " + cookingProgress.ToString("F0") + " / " + cookingTime.ToString("F0");
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
            cookingProgress += cookingSpeed;

            if (cookingProgress >= cookingTime)
            {
                cookingProgress = 0f;
                currentState = OvenState.FoodReady;
                Debug.Log(foodButton.foodName + " is cooked and ready!");
            }
        }
    }
}