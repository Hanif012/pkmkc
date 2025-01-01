using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    private bool isOpen = false;

    public void Interact()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        doorAnimator.ResetTrigger("Close");  // Reset any previous Close trigger
        doorAnimator.SetTrigger("Open");
        isOpen = true;
    }

    void CloseDoor()
    {
        doorAnimator.ResetTrigger("Open");  // Reset any previous Open trigger
        doorAnimator.SetTrigger("Close");
        isOpen = false;
    }
}
