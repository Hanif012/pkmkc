using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    private string isOpen = "n";
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        if (gameObject.name == "ovenDoor")
        {
            if(isOpen == "n")
            {
                Debug.Log("pp");
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 2, 0);
                isOpen = "o";
                StartCoroutine(stopDoor());
            }
            else if (isOpen == "y")
            {
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
                isOpen = "c";
                StartCoroutine(stopDoor());
            }
        }
    }

    IEnumerator stopDoor()
    {
        yield return new WaitForSeconds(5);

        if (isOpen == "o")
        {
            isOpen = "y";
        }
        
        if (isOpen == "c")
        {
            isOpen = "n";
        }
    }
}
