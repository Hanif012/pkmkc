using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "pkmkc/Food")]
public class Food : ScriptableObject
{
    [Header("Food Settings")]
    [SerializeField] private string foodName;
    [SerializeField] private float cookingTime = 10f;
    [SerializeField] private Image foodImage;
}