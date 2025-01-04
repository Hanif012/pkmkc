using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Lean.Gui;
using DG.Tweening;

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
    [SerializeField] private GameObject FoodPickedUpFX;


    [Header("Oven Settings")]
    [SerializeField] private float cookingTime;
    [SerializeField] private float cookingSpeed = 1f;
    [SerializeField] private GameObject OvenButton;
    private float cookingProgress = 0f;
    private Inventory inventory;

    private OvenState currentState = OvenState.Off;
    private bool isFoodInOven = false;

    void Awake()
    {
        FoodPickedUpFX.SetActive(false);
    }
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
        cookingTime = foodButton.cookingTime; // Update cookingTime according to the new food item
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
            cookingProgress = 0f; 
            cookingTime = foodButton.cookingTime; 
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
        // Debug.Log("Cooking " + foodButton.foodName + "...");

        while (currentState == OvenState.Cooking)
        {
            yield return new WaitForSeconds(1f);
            cookingProgress += GameTime.Instance.timeIncrement * cookingSpeed;

            if (cookingProgress >= cookingTime)
            {
                cookingProgress = 0f;
                currentState = OvenState.FoodReady;
                // Debug.Log(foodButton.foodName + " is cooked and ready!");
            }
        }
    }

    public void OnClick() 
    {
        if (currentState == OvenState.FoodReady)
        {
            RemoveFoodFromOven(TheFood);
            StartCoroutine(FoodPickedUp(TheFood));
                    // Debug.LogError("Food is picked up."); 

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

    public IEnumerator FoodPickedUp(FoodButton food)
    {
        var fx = FoodPickedUpFX.transform.Find("FX").GetComponent<ParticleSystem>();
        var bread = FoodPickedUpFX.transform.Find("Bread").GetComponent<Image>();
        var text = FoodPickedUpFX.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "+1 " + food.foodName;

        if (fx != null && bread != null)
        {
            fx.Play();
            bread.sprite = food.foodImage;
            FoodPickedUpFX.SetActive(true);
            FoodPickedUpFX.transform.localScale = Vector3.zero; // Start from scale 0

            // Chain tweens: scale up, boing boing, then scale back to 0
            FoodPickedUpFX.transform.DOScale(Vector3.one * 1.5f, 0.5f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    FoodPickedUpFX.transform.DOScale(Vector3.one, 0.5f)
                        .SetLoops(2, LoopType.Yoyo)
                        .OnComplete(() =>
                        {
                            FoodPickedUpFX.transform.DOScale(Vector3.zero, 0.5f)
                                .SetEase(Ease.InBack)
                                .OnComplete(() =>
                                {
                                    FoodPickedUpFX.SetActive(false);
                                });
                        });
                });

            yield return new WaitForSeconds(3f);
        }
        else
        {
            Debug.LogError("FX or Bread component is missing.");
        }
    }
}