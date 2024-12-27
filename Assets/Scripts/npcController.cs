using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine(PlayRightTurn());
            index++;
        }
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
            else if (index == 6)
            {
                StartCoroutine(PlayWavingAnimation());
                // Debug.Log("pp");
                index++;
            }
            else if (index == 7)
            {
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0); 
            }
            else if (index == 9)
            {
                StartCoroutine(PlayOpenDoorAnimation());
                index++;
            }
            else if (index < PathPoints.Length - 1)
            {
                index++;
            }
            else if (index == PathPoints.Length - 1)
            {
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0); 
            }
        }
        
        agent.SetDestination(PathPoints[index].position);
        
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

        yield return new WaitForSeconds(2);
    }

    IEnumerator PlayOpenDoorAnimation()
    {
        agent.isStopped = true;
        animator.SetFloat("vertical", -1);
        animator.SetFloat("horizontal", 0);

        door.Interact();
        yield return new WaitForSeconds(2f);


        agent.isStopped = false;

        animator.SetFloat("vertical", 1);

        yield return new WaitForSeconds(0.5f);

        door.Interact(); 

    }
    IEnumerator PlayRightTurn()
    {
        agent.isStopped = true;
        animator.SetFloat("vertical", 0);
        animator.SetFloat("horizontal", 1);

        
        yield return new WaitForSeconds(1);

        transform.Rotate(0, 180, 0);

        agent.isStopped = false;
        animator.SetFloat("vertical", 1);
        animator.SetFloat("horizontal", 0);
    }
}
