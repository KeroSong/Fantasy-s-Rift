using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnnemy : MonoBehaviour
{
    [SerializeField] Transform m_Target;
    [SerializeField] float idleDistance = 10f, walkDistance = 7f;
    NavMeshAgent agent;
    Animator ennemyAnimator;

    private void Awake()
    {
        ennemyAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance > walkDistance)
        {
            agent.speed = 0;
            ennemyAnimator.SetBool("walk", false);
        }
        else
        {
            agent.speed = 3f;
            ennemyAnimator.SetBool("walk", true);
        }
        agent.SetDestination(m_Target.position);

        
    }
}
