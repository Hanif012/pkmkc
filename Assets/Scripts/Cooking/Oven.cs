using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Oven : MonoBehaviour
{
    // Define the oven states using an enum
    private enum OvenState
    {
        Off,
        Cooking,
        FoodReady
    }

    [Header("Food SO")]
    [SerializeField] private List<Food> foodSO;

    [Header("UI Settings")]
    [SerializeField] private GameObject foodItemPrefab; // Prefab for each food item
    [SerializeField] private Transform contentPanel;    // The UI panel to hold food items
    [SerializeField] private Text ovenStateText;        // Text to display oven state
    // [SerializeField] private  FoodSOFocus;        // Text to display food state
    [Header("Food UI SO Fetch")]
    [SerializeField] private Image CurrentFoodImage;        // Text to display food state
    [SerializeField] private TextMeshProUGUI CurrentFoodName;        // Text to display food state
    [SerializeField] private TextMeshProUGUI CurrentFoodDescription;        // Text to display food state
    [SerializeField] private TextMeshProUGUI CurrentFoodCost;        // Text to display food state
    [SerializeField] private TextMeshProUGUI CurrentFoodTime;        // Text to display food state

    [Header("Oven Settings")]
    [SerializeField] private float cookingTime = 10f;
    [SerializeField] private float cookingSpeed = 1f;
    [SerializeField] private float cookingTemperature = 180f;
    private float cookingProgress = 0f;

    // Local references
    private OvenState currentState = OvenState.Off;  // Default state is Off
    private bool isFoodInOven = false;  // This remains a bool to indicate if food is placed

    void Start()
    {
        FetchFoodSO();
    }

    public void ToggleOven()
    {
        if (isFoodInOven && currentState != OvenState.FoodReady)
        {
            // Throw warning that food is in oven and not ready
            Debug.Log("Food is in the oven, please wait for it to finish cooking.");
        }
        else
        {
            // Toggle oven between Off and Cooking states
            if (currentState == OvenState.Off)
            {
                if (isFoodInOven)
                {
                    currentState = OvenState.Cooking;
                    Debug.Log("Oven is now cooking.");
                }
                else
                {
                    Debug.Log("No food in the oven to cook.");
                }
            }
            else if (currentState == OvenState.FoodReady)
            {
                Debug.Log("Food is ready! Turn off the oven or remove the food.");
            }
            else
            {

                currentState = OvenState.Off;
                Debug.Log("Oven is turned off.");
            }
        }
    }

    void Update()
    {
        if (currentState == OvenState.Cooking && isFoodInOven)
        {
            StartCoroutine(CookFood());
        }
    }

    private void FetchFoodSO()
    {
        // Clear any existing items in the UI
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Loop through the foodSO list and create UI elements for each one
        foreach (Food food in foodSO)
        {
            GameObject newFoodItem = Instantiate(foodItemPrefab, contentPanel);
            newFoodItem.transform.Find("FoodNameText").GetComponent<Text>().text = food.foodName;

            Image foodImage = newFoodItem.transform.Find("FoodImage").GetComponent<Image>();
            if (food.foodImage != null)
            {
                foodImage.sprite = food.foodImage;
            }
            // You can add more properties like description if necessary
        }
    }



    public IEnumerator CookFood()
    {
        yield return new WaitForSeconds(1f);
        cookingProgress += cookingSpeed;

        if (cookingProgress >= cookingTime)
        {
            // Food is cooked, update the state
            cookingProgress = 0f;
            currentState = OvenState.FoodReady;
            Debug.Log("Food is cooked and ready!");
        }
    }
}
