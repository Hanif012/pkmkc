using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Food> foods = new List<Food>();

    public void AddFood(Food food)
    {
        foods.Add(food);
        Debug.Log(food.foodName + " added to inventory.");
    }

    public void RemoveFood(Food food)
    {
        if (foods.Contains(food))
        {
            foods.Remove(food);
            Debug.Log(food.foodName + " removed from inventory.");
        }
        else
        {
            Debug.Log(food.foodName + " not found in inventory.");
        }
    }

    public List<Food> GetFoods()
    {
        return new List<Food>(foods);
    }
}