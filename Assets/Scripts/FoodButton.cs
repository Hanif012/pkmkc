using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodButton : MonoBehaviour
{
    [Header("Oven Settings")]
    [SerializeField] private Oven oven;

    [Header("Food Settings")]
    public string foodName;
    public Sprite foodImage;
    public float cookingTime;
    public string foodDescription;
    public int cost;
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI foodNameText;
    [SerializeField] private Image foodImageSprite;
    [SerializeField] private GameObject cookingTimeText;
    [SerializeField] private TextMeshProUGUI foodDescriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [Header("Bake Button Settings")]
    [SerializeField] private Text bakeButtonText;
    [SerializeField] private GameObject BakeButton;
    
    public void Awake()
    {
        bakeButtonText.text = foodName;
        // FetchDataText();
    }

    public void FetchDataText()
    {
        foodNameText.text = foodName;
        foodImageSprite.sprite = foodImage;

        foodDescriptionText.text = foodDescription;
        costText.text = "Cost: Rp  " + cost.ToString();
        BakeButton.GetComponent<LeanButton>().OnClick.AddListener(() => BakeFood());
    }

    public void BakeFood()
    {
        oven.AddFoodToOven(); // Ensure food is added to the oven
        oven.StartCooking(this);
    }
}
