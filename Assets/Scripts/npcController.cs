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

    // AudioManager audioManager;
    // private void Awake()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    // }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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
            if (index + 1 != PathPoints.Length)
            {
                index++;
            }
            else
            {
                index = 0;
                StartCoroutine(PlayWavingAnimation());
                return;
            }
        }

        agent.SetDestination(PathPoints[index].position);

        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
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
}
