using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "pkmkc/Food")]
public class Food : ScriptableObject
{
    [Header("Food Settings")]
    [SerializeField] public string foodName;
    [SerializeField] public Sprite foodImage;
    [SerializeField] public float cookingTime = 10f;
    [SerializeField] public string foodDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec pur";
    [SerializeField] public int cost = 10;
}