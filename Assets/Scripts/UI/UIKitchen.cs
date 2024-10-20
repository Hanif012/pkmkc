using UnityEngine;

public class UIKitchen : MonoBehaviour
{
    [Header("UI Elements")]
    private bool isActive;
    public GameObject kitchenUI;
    public GameObject orderUI;
    public GameObject RecipeList;

    void Awake()
    {
        kitchenUI.SetActive(false);
        orderUI.SetActive(false);
    }

    void Update()
    {
        
    }
    

}