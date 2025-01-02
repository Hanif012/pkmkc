using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class Inventory : MonoBehaviour
{
    public List<Food> FoodsInventory = new List<Food>();
    
    [SerializeField] private TextMeshProUGUI inventoryText;

    private Dictionary<string, int> foodCounts = new Dictionary<string, int>();

    public void AddFood(Food food)
    {
        FoodsInventory.Add(food);
        if (foodCounts.ContainsKey(food.foodName))
        {
            foodCounts[food.foodName]++;
        }
        else
        {
            foodCounts[food.foodName] = 1;
        }
        Debug.Log(food.foodName + " added to inventory.");
        UpdateInventoryText();
    }

    public bool RemoveFood(Food food)
    {
        if (FoodsInventory.Contains(food))
        {
            FoodsInventory.Remove(food);
            if (foodCounts.ContainsKey(food.foodName))
            {
                foodCounts[food.foodName]--;
                if (foodCounts[food.foodName] == 0)
                {
                    foodCounts.Remove(food.foodName);
                }
            }
            Debug.Log(food.foodName + " removed from inventory.");
            UpdateInventoryText();
            return true;
        }
        else
        {
            Debug.Log(food.foodName + " not found in inventory.");
            return false;
        }
    }

    public bool HasFood(Food food)
    {
        return foodCounts.ContainsKey(food.foodName) && foodCounts[food.foodName] > 0;
    }

    // [YarnFunction("AddFood")]
    public bool AddFoodByName(string foodName)
    {
        Food food = FoodsInventory.Find(f => f.foodName == foodName);
        if (food != null)
        {
            AddFood(food);
            return true;
        }
        else
        {
            Debug.Log(foodName + " not found in food list.");
            return false;
        }
    }

    // [YarnCommand("RemoveFood")]
    public bool RemoveFoodByName(string foodName)
    {
        Food food = FoodsInventory.Find(f => f.foodName == foodName);
        if (food != null)
        {
            if (!HasFood(food))
            {
                Debug.Log(foodName + " not found in inventory.");
                return false;
            }
            RemoveFood(food);
            return true;
        }
        else
        {
            Debug.Log(foodName + " not found in inventory.");
            return false;
        }
    }

    public List<Food> GetFoods()
    {
        return new List<Food>(FoodsInventory);
    }

    private void UpdateInventoryText()
    {
        inventoryText.text = "Inventory:\n";
        foreach (var foodCount in foodCounts)
        {
            inventoryText.text += foodCount.Key + " x" + foodCount.Value + "\n";
        }
    }
}