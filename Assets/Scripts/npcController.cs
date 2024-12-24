using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    private Transform[] PathPoints;
    public float minDistance = 0;    
    public int index = 0;

    private DoorController door;

    NewAudioManager audioManager;
    // private void Awake()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<NewAudioManager>();
    // }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        door = GameObject.Find("PivotDoor")?.GetComponent<DoorController>();

        PathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < PathPoints.Length;i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }

        agent.SetDestination(PathPoints[index].position);
    }

    
    void Update()
    {
        roam();
    }

    void roam()
    {
        if (Vector3.Distance(transform.position, PathPoints[index].position) < minDistance)
        {
            if (index == 4)
            {
                StartCoroutine(PlayOpenDoorAnimation());
                index++;
            }
            else if (index == PathPoints.Length - 1)
            {
                StartCoroutine(PlayWavingAnimation());
                index++;
            }
            else if (index > PathPoints.Length - 1)
            {
                agent.isStopped = true;
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0);         
            }
            else
            {
                index++;
            }
        }
        if(index > PathPoints.Length - 1)
        {
        }
        else
        {
            agent.SetDestination(PathPoints[index].position);
        }
        if(!agent.isStopped)
        {
            animator.SetFloat("vertical", 1);
        }
    }

    IEnumerator PlayWavingAnimation()
    {
        agent.isStopped = true;
        animator.SetFloat("vertical", 0);
        animator.SetFloat("horizontal", -1);

        
        yield return new WaitForSeconds(3.5f);

        animator.SetFloat("horizontal", 0);
        agent.isStopped = false;
    }

    IEnumerator PlayOpenDoorAnimation()
    {
        agent.isStopped = true;
        animator.SetFloat("vertical", -1);
        animator.SetFloat("horizontal", 0);

        door.Interact();
        yield return new WaitForSeconds(3);


        agent.isStopped = false;

        animator.SetFloat("vertical", 1);

        
    }
}
