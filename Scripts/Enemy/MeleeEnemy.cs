//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField]private float accuracy;
    [SerializeField] private GameObject rangedAttack;
    [SerializeField] private bool mortarFiring;
    [Header("Shooting")]
    [SerializeField] private float range;
    [SerializeField] private float rangedAttackCooldown;
    private float rangedAttackTimer;

    private Transform playerLocation;
    private LayerMask target;//player tag
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = LayerMask.GetMask("Player");
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        rangedAttackTimer = rangedAttackCooldown;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        agent.destination = playerLocation.position;//moves to player
        rangedAttackTimer -= Time.deltaTime*Random.Range(0.9f,1.1f);//randomises shooting slightly
        if(rangedAttackTimer < 0) {
            bool playerClose = Physics.CheckSphere(transform.position, range, target);//range check
            if (!playerClose)
            {
               // if (mortarFiring)//projectile spawns around the player //mortar firing unneeded
               // {
                    ///implement l
               // }
                //else//direct projectile //projectile moves from the enemy to the player.
               // {
                    //creates projectile
                    GameObject newProjectile = Instantiate(rangedAttack, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation);
                    newProjectile.transform.SetParent(GameObject.Find("Enemy Projectiles").transform, true);

                    //adds spread to projectile
                    Vector3 accuracyVector = new(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy));
                    newProjectile.GetComponent<projectileTrigger>().spread = accuracyVector;
               // }
                rangedAttackTimer = rangedAttackCooldown;//reset shooting cooldown
            }
        }
    }
}