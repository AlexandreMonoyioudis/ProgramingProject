using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class bossPhases : MonoBehaviour
{
    [Header("Big Laser Attack")]
    [SerializeField] private GameObject teleporEffect;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject Eye;
    [SerializeField] private GameObject Rock;
    [SerializeField] private GameObject laser;
    [SerializeField] private Vector3 point;//location the boss will land
    private GameObject ToDestroy;//eye that is destoyed once laser is shot
    [Header("Healing Attack")]
    [SerializeField] private GameObject heals;
    [Header("Ring Of Bullets")]
    [SerializeField] private GameObject bullet;
    [Header("Melee enemy summon")]
    [SerializeField] private GameObject summonEnemy;
    [SerializeField] private GameObject summonEnemyEffect;
    [Header("shootfaster")]
    [SerializeField] private GameObject normalprojectile;
    private int previous;
    // Start is called before the first frame update
    void Start()//drops from the celing
    {
        previous = 0;
        LaserAttackStart();
    }
    private void LaserAttackStart()
    {//removes boss and spawns falling object
        GameObject boss = gameObject.transform.GetChild(0).gameObject;
        Instantiate(teleporEffect, boss.transform.position, Quaternion.identity);//teleport effect
        boss.SetActive(false);//removes boss as it has teleported away
        GameObject rock = Instantiate(Rock, point + new Vector3(0, 1000, 0), transform.rotation);//spawns the rock
        rock.GetComponent<Rigidbody>().AddForce(Vector3.down * 30000, ForceMode.Impulse);//speeds up rock
        StartCoroutine(CheckIfRockOnGround(rock));
    }

    private IEnumerator CheckIfRockOnGround(GameObject rock)//when rock hits ground
    {
        while (rock.transform.position.y > 5)
        {//waiting for rock to hit the ground
            yield return null;
        }
        Destroy(rock);
        EnemySpawnAnimation();
    }

    private void EnemySpawnAnimation()//looks at you with laser
    {
        ToDestroy = Instantiate(Eye, point, transform.rotation);
        Instantiate(smoke, point, transform.rotation);
        Invoke(nameof(ShootLazer), 1);
    }
    private void ShootLazer()//shoots you with previously mentioned lazer
    {
        Destroy(ToDestroy);

        //sets boss to new location
        Transform boss=gameObject.transform.GetChild(0);
        boss.gameObject.SetActive(true);
        boss.transform.GetChild(0).position = point + Vector3.back*3;//sets the nav location for the boss

        Instantiate(laser, point + new Vector3(0,2,0), transform.rotation);//shoots
        NextAttack();//starts next attack
    }

    private void spawnHeals()
    {
        GameObject heal=Instantiate(heals, gameObject.transform.GetChild(0));
        heal.transform.SetParent(GameObject.Find("healthPickups").transform);
        NextAttack();
    }
    private void spray()
    {
        for (int i = 0; i < 200; i++)//spawns a volly of projectiles
        {
            GameObject newProjectile = Instantiate(bullet, gameObject.transform.GetChild(0).transform.GetChild(2).transform.position,Quaternion.identity);
            newProjectile.transform.SetParent(GameObject.Find("Enemy Projectiles").transform);
            Vector3 accuracyVector = new(Random.Range(-360, 360), Random.Range(-5, 7), Random.Range(-360, 360));
            newProjectile.GetComponent<projectileTrigger>().spread = accuracyVector;//sets accutacy
        }
        NextAttack();
    }
    private IEnumerator summon()
    {
        yield return new WaitForSeconds(2);//short wait
        Instantiate(summonEnemyEffect, point- new Vector3(0,4,0), Quaternion.identity);//creates effect close to the ground
        yield return new WaitForSeconds(1);
        GameObject enemy = Instantiate(summonEnemy, point, Quaternion.identity);
        enemy.transform.SetParent(GameObject.Find("Enemies").transform);
        Invoke(nameof(NextAttack),4);
    }
    private IEnumerator rapidFire()
    {
        Transform bossPos = gameObject.transform.GetChild(0).transform.GetChild(0).transform;
        for (int i = 0; i < 50;i++)//summons additional projectiles
        {
            GameObject newProjectile = Instantiate(normalprojectile, bossPos.position + new Vector3(0, 5, 0), Quaternion.identity);
            newProjectile.transform.SetParent(GameObject.Find("Enemy Projectiles").transform);
            Vector3 accuracyVector = new(Random.Range(-30, 30), Random.Range(-1,3), Random.Range(-30, 30));
            newProjectile.GetComponent<projectileTrigger>().spread = accuracyVector;
            yield return new WaitForSeconds(.1f);

        }
        Invoke(nameof(NextAttack), 1);
    }

    private void NextAttack()//controls the attacks
    {
        int attack;
        do//prevents the same attack occuring twice
        {
            attack = Random.Range(0, 5);
        }
        while (attack == previous);
        previous = attack;
        point = gameObject.transform.GetChild(0).transform.GetChild(0).position + new Vector3(0, 4, 0);//sets the location the enemy will land


        switch (attack)//sets the enemy attack
        {
            case 0:
                Invoke(nameof(LaserAttackStart), 5);
                break;
            case 1:
                Invoke(nameof(spawnHeals), 3);
                break;
            case 2:
                Invoke(nameof(spray), 3);
                break;
            case 3:
                StartCoroutine(nameof(summon));
                break;
            case 4:
                StartCoroutine(nameof(rapidFire));
                break;

        }
    }
}
