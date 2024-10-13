using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Animator component reference
    public Dictionary<string, bool> animationStates = new Dictionary<string, bool>(); // Store the state of animations

    // Function to trigger an animation interaction (toggle state)
    public void Interact(string animationName)
    {
        if (!animationStates.ContainsKey(animationName))
        {
            animationStates[animationName] = false; // Initialize animation state if it doesn't exist
        }

        if (!animationStates[animationName])
        {
            PlayAnimation(animationName);
        }
        else
        {
            StopAnimation(animationName);
        }
    }

    // Function to play the given animation
    void PlayAnimation(string animationName)
    {
        animator.ResetTrigger($"Stop_{animationName}"); // Reset the stop trigger
        animator.SetTrigger($"Play_{animationName}"); // Set the play trigger
        animationStates[animationName] = true; // Update the animation state to "playing"
    }

    // Function to stop the given animation
    void StopAnimation(string animationName)
    {
        animator.ResetTrigger($"Play_{animationName}"); // Reset the play trigger
        animator.SetTrigger($"Stop_{animationName}"); // Set the stop trigger
        animationStates[animationName] = false; // Update the animation state to "stopped"
    }
}

// Example Usage:
// Attach this script to any GameObject with an Animator component.
// Use the Interact function to toggle animations by name.
// Example: animationController.Interact("OpenDoor"); will trigger "Play_OpenDoor" or "Stop_OpenDoor" depending on the state.