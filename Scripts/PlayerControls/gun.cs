using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gun : MonoBehaviour
{
    [SerializeReference]private LayerMask layer;
    [Header ("weapon Visuals")]
    [SerializeField] private ParticleSystem shootingSystem;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private string Name;
    [Header("weapon Values")]
    [SerializeField] private float inaccuracy;
    [SerializeField] private float fireRate;
    [SerializeField] private int multishot;//amout of projectiles
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int amunition;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool Automatic;
    private float fireRateTimer;
    private float reloadTimer;
    private int ammoCounter;

    private AudioSource sound;
    private TextMeshProUGUI ammoDisplay;
    private Animator animator;
    private bool shotTaken;
    private void Awake()
    {
        if (Name != null)
        {
            animator = GameObject.Find(Name).GetComponent<Animator>();
        }
        ammoDisplay = GameObject.Find("WeaponDisplay").GetComponent<TextMeshProUGUI>();//the gameobjects that displays the ammo
        sound = gameObject.GetComponent<AudioSource>();
        reloadTimer = 0;
        ammoCounter = amunition;//sets the amunition at max at the start of every load
    }
    private void OnEnable()//sets up weapon switch
    {
        shotTaken = true;//makes the user repress the trigger to shoot for non automatic weapons
        if (reloadTimer > 0)//if reloading
        {
            setAnimator(0);
            updateGunDisplay("Reload");
        }
        else
        {
            updateGunDisplay(ammoCounter + "/" + amunition);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Reload") > 0.5f && amunition != ammoCounter)//starts reloading
        {
            setAnimator(0);
            ammoCounter = amunition;
            reloadTimer = reloadTime;
            //Debug.Log("Reloading");
        }

        if (reloadTimer < 0)
        {
            setAnimator(2);
            if (fireRateTimer < 0)
            {
                updateGunDisplay(ammoCounter + "/" + amunition);
                setAnimator(1);
                if (Input.GetAxisRaw("Fire1") > 0.5f && shotTaken)
                {
                    sound.Play();//plays the weapon sound
                    shotTaken = false;
                    StartCoroutine(isShot());
                    fireRateTimer = fireRate;
                    ammoCounter--;
                    //Debug.Log(ammoCounter);
                    if (ammoCounter <= 0)
                    {
                        ammoCounter = amunition;
                        reloadTimer = reloadTime;
                    }
                    for (int i = 0; i < multishot; i++)
                    {
                        Vector3 direction = calcSpread(Camera.main.transform.forward, inaccuracy).normalized;//calculates the projectiles direction by adding inaccuracy
                        Ray ray = new(Camera.main.transform.position, direction);
                        TrailRenderer trail = Instantiate(trailRenderer, transform.position, Quaternion.identity);
                        trail.transform.SetParent(GameObject.Find("Player Projectiles").transform, true);
                        if (Physics.Raycast(ray, out RaycastHit rayHit, range, layer))
                        {
                            StartCoroutine(spawnTrail(trail, rayHit.point, rayHit));//rayhit.point is where it colides
                        }
                        else
                        {
                            direction = Camera.main.transform.position + direction * range;//finds the location where the projectile will end
                            StartCoroutine(spawnTrail(trail, direction));
                        }
                    }
                }
            }
            else
            {
                setAnimator(3);
                fireRateTimer -= Time.deltaTime;
                updateGunDisplay(ammoCounter + "/" + amunition);
            }
        }
        else//reload
        {
            setAnimator(0);
            updateGunDisplay("Reload");
            //Debug.Log("Reloading");
            reloadTimer -= Time.deltaTime;
        }
    }
    private IEnumerator spawnTrail(TrailRenderer trail,Vector3 hit, RaycastHit rayHit)//The vesion of the Cortine if the value does hit somthing
    {
        GameObject gameObjectHit = rayHit.collider.gameObject;
        if (gameObjectHit.CompareTag("Enemy"))//checks if a the enemy was hit
        {
            //deals damage
            HPEnemy script = gameObjectHit.GetComponent<HPEnemy>();
            script.dealDamage(damage);
        }

        float timer = 0;
        float distance = rayHit.distance;//calculates distance
        while (timer < 0.05f * distance){//moves the object to the enemy
            trail.transform.position = Vector3.Lerp(transform.position, hit, 0.1f);
            timer += Time.deltaTime/trail.time;
            yield return null;
        }
        trail.transform.position = hit;
        ParticleSystem hitEffect = Instantiate(shootingSystem, hit, Quaternion.LookRotation(rayHit.normal));//intanciates the effect for the bullet hit
        hitEffect.transform.SetParent(GameObject.Find("Player Projectiles").transform, true);
        Destroy(trail.gameObject, trail.time);
    }

    private IEnumerator spawnTrail(TrailRenderer trail, Vector3 hit)//The vesion of the Cortine if the value does not hit somthing
    {
        float timer = 0;
        float distance = (transform.position - hit).magnitude;
        while (timer < 0.05f*distance)//moves the object to the enemy
        {
            trail.transform.position = Vector3.Lerp(transform.position, hit, 0.1f);
            timer += Time.deltaTime/trail.time;
            yield return null;
        }
        trail.transform.position = hit;//moves the effect to its final position
        Destroy(trail.gameObject, trail.time);
    }
    private Vector3 calcSpread(Vector3 pos ,float inaccuracy)//calcuatest the new direction of the projectile
    {
        pos = new Vector3(pos.x + Random.Range(-inaccuracy, inaccuracy), pos.y + Random.Range(-inaccuracy, inaccuracy), pos.z + Random.Range(-inaccuracy, inaccuracy));
        return pos;
    }

    private IEnumerator isShot()//automatic fire
    {
        if (Automatic)
        {
            shotTaken = true;
            yield return null;
        }
        while (shotTaken == false)
        {
            yield return new WaitForSeconds(0.05f);
            if (Input.GetAxisRaw("Fire1") == 0)
            {
                //Debug.Log("shot");
                shotTaken = true;
            }
        }
        yield return null;
    }

    private void updateGunDisplay(string message)
    {
        //displays gun name
        ammoDisplay.text = message;
    }
    private void setAnimator(int choice)//sets a specific value in the animator
    {
        if (animator != null)
        {
            switch (choice)
            {
                case 0:
                    animator.SetBool(name: "Is Reloading", true);
                    break;
                case 1:
                    animator.SetBool(name: "IsReady", true);
                    break;
                case 2:
                    animator.SetBool(name: "Is Reloading", false);
                    break;
                case 3:
                    animator.SetBool(name: "IsReady", false);
                    break;

            }
        }
    }
}