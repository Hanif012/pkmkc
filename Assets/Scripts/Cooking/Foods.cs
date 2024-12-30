using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "pkmkc/Foods")]
public class Foods : ScriptableObject, IEnumerable<Food>
{
    public List<Food> foodList;

    public IEnumerator<Food> GetEnumerator()
    {
        return foodList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
[System.Serializable]
public class Food
{
    [Header("Food Settings")]
    [SerializeField] public string foodName;
    [SerializeField] public Sprite foodImage;
    [SerializeField] public float cookingTime = 10f;
    [SerializeField] public string foodDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec pur";
    [SerializeField] public int cost = 10;
}