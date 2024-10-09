using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
todo: Add More Button States, like Hover, Pressed, etc.

Normal: The default state of a button.
Highlighted: The state of a button when it is highlighted. kyk pas lagi dihover. Istilah lainnya Focus.
Pressed: The state of a button when it is pressed.
Disabled: The state of a button when it cannot be interacted with.
*/
public class ImageColorSwitch : Interactable
{
    public Image image;
    public Color targetColor;

    public float timeToReset = 0.2f;
    public bool dontReset = false;

    private Color originalColor;
    private float timeStamp;

    public override void Start()
    {
        base.Start();

        originalColor = image.color;
    }

    public override void Interact(Transform interactingObjectTransform)
    {
        base.Interact(interactingObjectTransform);

        timeStamp = Time.time;

        image.color = targetColor;

        if (!dontReset)
        {
            // If not set to not reset, invoke the reset after the specified time.
            Invoke("ResetToOriginalColor", timeToReset);
        }
    }

    private void ResetToOriginalColor()
    {
        if (Time.time - timeStamp >= timeToReset - 0.05)
        {
            image.color = originalColor;
        }
        
    }

}
