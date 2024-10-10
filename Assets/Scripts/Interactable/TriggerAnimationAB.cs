using UnityEngine;

public class TriggerAnimationAB : Interactable
{
    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] string triggerName = "Trigger";

    [SerializeField] bool isToggle = false;
    [SerializeField] bool isReset = false;
    
}