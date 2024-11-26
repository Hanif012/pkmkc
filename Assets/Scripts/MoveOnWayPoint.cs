using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWayPoint : MonoBehaviour
{
    public List<GameObject> waypoints;
    public float speed = 5;
    int index = 0;
    public bool isLoop = true;
    private Animator animator; // Reference to Animator

    void Start()
    {
        animator = GetComponent<Animator>(); // Initialize Animator
    }

    void Update()
    {
        // Current destination
        Vector3 destination = waypoints[index].transform.position;

        // Movement logic
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);

        // Check if near the destination
        if (distance <= 0.05f)
        {
            if (index < waypoints.Count - 1)
            {
                index++;
            }
            else if (isLoop)
            {
                index = 0;
            }

            // Stop walking when destination is reached
            animator.SetBool("isWalking", false);
        }
        else
        {
            // Walking if moving
            animator.SetBool("isWalking", true);
        }
    }
}
