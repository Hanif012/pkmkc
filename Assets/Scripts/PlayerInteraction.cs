using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;  // The distance within which the player can interact

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))  // Press E to interact
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                DoorController door = hit.collider.GetComponent<DoorController>();
                if (door != null)
                {
                    door.Interact();  // Call the interact function of the door
                }
            }
        }
    }
}
