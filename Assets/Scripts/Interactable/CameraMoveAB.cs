using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeVCamPriority : Interactable
{
    public CinemachineVirtualCamera virtualCamera;
    public int targetPriority = 11;
    [Tooltip("Move camera")]
    public bool isSwitch = false;
    public float timeToReset = 2f;
    public bool dontReset = true;

    private int originalPriority;
    private bool setOriginal = true;

    public override void Start()
    {
        base.Start();

        originalPriority = virtualCamera.Priority;
    }

    public override void Interact(Transform interactingObjectTransform)
    {
        base.Interact(interactingObjectTransform);

        if (isSwitch)
        {
            if (setOriginal)
            {
                virtualCamera.Priority = targetPriority;
                setOriginal = false;
            }
            else
            {
                virtualCamera.Priority = originalPriority;
                setOriginal = true;
            }
        }
        else
        {
            virtualCamera.Priority = targetPriority;
        }

        if (!dontReset)
        {
            // If not set to not reset, invoke the reset after the specified time.
            Invoke("ResetToOriginalPriority", timeToReset);
        }
    }

    private void ResetAll()
    {
        // virtualCamera.;
        setOriginal = true;
    }

}
