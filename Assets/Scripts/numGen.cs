using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numGen : MonoBehaviour
{
    void OnMouseDown()
    {
        // Generate and log a random number between 0 and 100 when the object is clicked
        GenerateRandomNumber();
    }

    void GenerateRandomNumber()
    {
        int randomNumber = Random.Range(0, 101); // Generate a random number (0 to 100 inclusive)
        Debug.Log("Generated Random Number: " + randomNumber);
    }
}
