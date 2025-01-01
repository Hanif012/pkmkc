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

    public bool isWaving = true;

    private DoorController door;

    private NewAudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<NewAudioManager>();
    }
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
            if (index == 4 || index == 8)
            {
                StartCoroutine(PlayOpenDoorAnimation());
                index++;
            }
            else if (index == 6 && isWaving)
            {
                StartCoroutine(PlayWavingAnimation());
            }
            else if (index == 6)
            {
                animator.SetFloat("vertical", 0);
                animator.SetFloat("horizontal", 0); 
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
        isWaving = true;
        agent.isStopped = true;
        animator.SetFloat("vertical", 0);
        animator.SetFloat("horizontal", -1);

        yield return new WaitForSeconds(1.7f);

        isWaving = false;
    }

    IEnumerator PlayOpenDoorAnimation()
    {
        agent.isStopped = true;
        animator.SetFloat("vertical", -1);
        animator.SetFloat("horizontal", 0);

        yield return new WaitForSeconds(0.2f);

        door.Interact();
        audioManager.PlaySFX(audioManager.Door);
        
        yield return new WaitForSeconds(1.5f);


        animator.Rebind();

        agent.isStopped = false;

        animator.SetFloat("vertical", 1);

        yield return new WaitForSeconds(1f);

        door.Interact(); 

    }
    IEnumerator PlayRightTurn()
    {
        agent.isStopped = true;
        animator.SetFloat("vertical", 0);
        animator.SetFloat("horizontal", 1);

        
        yield return new WaitForSeconds(0.8f);

        transform.Rotate(0, 180, 0);

        agent.isStopped = false;
        animator.SetFloat("vertical", 1);
        animator.SetFloat("horizontal", 0);
    }
}
