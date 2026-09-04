//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Shooting Controls")]
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float timeToShoot;
    private float timeBettwenShots;
    [SerializeField] private float accuracy;



    [Header("Ground Handleing")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool moves;
    private Rigidbody rb;

    private float hoverDirection;
    private float hoverTimer;
    private Transform navLocation;//location the drone gos to

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        hoverDirection = 3f;
        timeBettwenShots = timeToShoot;
        timeToShoot = 1f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //enemy movement
        if (moves)
        {
            if (hoverTimer > .5f)
            {
                rb.AddForce(hoverDirection * Time.deltaTime * Vector3.up, ForceMode.Impulse);
                hoverTimer -= hoverTimer * Time.deltaTime;
                hoverDirection -= hoverDirection * Time.deltaTime;
            }
            else
            {
                bool hovering = Physics.Raycast(transform.position, Vector3.down, 2f, ground);
                if (hovering)
                {
                    hoverDirection = Random.Range(5, 9);
                    hoverTimer += hoverDirection;
                }
                else //enemy is to high reduce height
                {
                    //reduce y axis
                    hoverDirection = Random.Range(-3, -6);
                    hoverTimer -= hoverDirection;

                }
            }
            //moves enemy to correct spot
            navLocation = gameObject.transform.GetChild(0).transform;//gets transform from navigator child
            transform.position = new Vector3(navLocation.position.x, transform.position.y, navLocation.position.z);
            if (transform.position.y < navLocation.position.y)
            {
                transform.position = new Vector3(navLocation.position.x, navLocation.position.y, navLocation.position.z);
            }

            gameObject.transform.GetChild(0).transform.position = new Vector3(transform.position.x, navLocation.position.y, transform.position.z);
        }
        //delay between shots
        timeToShoot -= Time.deltaTime;
        if (timeToShoot <0)
        {
            timeToShoot += timeBettwenShots * Random.Range(0.8f,1.2f);
            GameObject newProjectile= Instantiate(Projectile, transform.position, transform.rotation);
            newProjectile.transform.SetParent(GameObject.Find("Enemy Projectiles").transform,true);

            //adds spread to projectile
            Vector3 accuracyVector = new(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy));
            newProjectile.GetComponent<projectileTrigger>().spread = accuracyVector;
        }
    }
   
}
