using UnityEngine;
using System.Collections.Generic;
using Yarn.Unity;

public class OrderClass : MonoBehaviour
{
    public Dictionary<Food, int> FoodsOrder = new Dictionary<Food, int>();
    public Inventory inventory;
    public Foods availableFoods;
    public int defaultItemCount = 5;
    public GameManager gameManager;

    public void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetFoodSO(Foods foodSO)
    {
        availableFoods = foodSO;
    }

    public void AddOrder(Food food, int count)
    {
        if (FoodsOrder.ContainsKey(food))
        {
            FoodsOrder[food] += count;
        }
        else
        {
            FoodsOrder.Add(food, count);
        }
        Debug.Log(count + " " + food.foodName + "(s) added to order.");
    }

    [YarnCommand("CheckOrderInventory")]
    public bool CheckOrderInventory()
    {   
        foreach (KeyValuePair<Food, int> entry in FoodsOrder)
        {
            Food food = entry.Key;
            int count = entry.Value;
            if (inventory.HasFood(food))
            {
                Debug.Log("Order contains " + count + " " + food.foodName + "(s)");
            }
            else
            {
                Debug.Log("Order does not contain " + count + " " + food.foodName + "(s)");
                return false;
            }
        }
        return true;
    }

    [YarnCommand("GenerateOrder")]
    public string GenerateOrder(string orderOutput)
    {
        FoodsOrder.Clear();
        for (int i = 0; i < defaultItemCount; i++)
        {
            int randomIndex = Random.Range(0, availableFoods.foodList.Count);
            Food randomFood = availableFoods.foodList[randomIndex];
            if (FoodsOrder.ContainsKey(randomFood))
            {
                FoodsOrder[randomFood]++;
            }
            else
            {
                FoodsOrder.Add(randomFood, 1);
            }
        }
        // Order output
        orderOutput += " ";

        foreach (KeyValuePair<Food, int> entry in FoodsOrder)
        {
            orderOutput += entry.Value + " " + entry.Key.foodName + "(s), ";
        }
        return orderOutput;
    }

    [YarnCommand("FulfillOrder")]
    public void FulfillOrder()
    {
        foreach (KeyValuePair<Food, int> entry in FoodsOrder)
        {
            Food food = entry.Key;
            int count = entry.Value;
            for (int i = 0; i < count; i++)
            {
                inventory.RemoveFood(food);
            }
        }
        gameManager.DepositToBank(1000);
        Debug.Log("Order fulfilled. Rp 1000 added to balance.");
    }

    //TODO: Test this method
}