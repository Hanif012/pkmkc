using System.Collections;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [Header("Oven Settings")]
    [SerializeField] private float cookingTime = 10f;
    [SerializeField] private float cookingSpeed = 1f;
    [SerializeField] private float cookingTemperature = 180f;
    // [SerializeField] private Food;
    // Local references
    private Coroutine something ;
    private bool isOn = false;
    private bool isFoodinOven = false;

    public void ToggleOven()
    {

    }

    void Update()
    {
        if (!isOn && isFoodinOven)
        {
            
        }
    }

    public IEnumerator CookFood()
    {
        yield return new WaitForSeconds(1f);
        
    }

}