using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numGen : Interactable
{
    public void Interact()
    {
        // Generate and log a random number between 0 and 100
        Debug.Log(Random.Range(0, 100));
    }
}
