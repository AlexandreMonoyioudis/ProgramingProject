//using System.Collections;
//sing System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movementRangedEnemy : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    [Header("Speed")]
    [SerializeField] private float duration;
    [SerializeField] private float speedModifyer;
    private LayerMask target;//player tag
    private NavMeshAgent agent;
   
    private float randomisation;
    private float randTimer;

    // Start is called before the first frame update
     private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = LayerMask.GetMask("Player");
        randTimer = -1;
    }

    private void FixedUpdate()
    {
        if (randTimer < 0)//randomisation
        {
            randTimer = duration;
            randomisation = Random.Range(-2, 2);//randomises direction
        }
        randTimer -= Time.deltaTime;

        //movement
        bool playerInAttackRange = Physics.CheckSphere(transform.position, maxRange+randomisation*6, target);
        bool playerInMinRange = Physics.CheckSphere(transform.position, minRange+randomisation*6, target); 
        Vector3 playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;//gets the player position

        //Debug.Log(playerInAttackRange+" "+playerInMinRange);
        if (playerInMinRange)//moves away from player
        {
            agent.acceleration = 40 * speedModifyer;
            agent.destination = transform.position + (transform.position - playerLocation).normalized * (6 + randomisation);
        }
        else if (!playerInAttackRange)//out of range moves towards player
        {
            agent.acceleration = 8 * speedModifyer;
            agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;//gets the players position and moves to it
        }
        else
        {
            //causes the enemy to circle the player
            Vector3 normal = (transform.position - playerLocation).normalized;
            Vector3 tangent = Vector3.Cross(normal, Vector3.down);
            //Debug.Log(randomisation);
            agent.destination = transform.position - tangent*randomisation;
        }
    }
}
