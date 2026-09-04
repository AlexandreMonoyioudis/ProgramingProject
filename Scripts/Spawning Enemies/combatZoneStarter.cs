//using System.Collections.Generic;
using UnityEngine;

public class combatZoneStarter : MonoBehaviour
{
    [Header("spawning")]
    [SerializeField] private int Waves;
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private int[] enemyCount;
    [SerializeField] private Vector3[] enemyPos;
    [SerializeField] private int increasePerWave;

    [Header("display")]
    [SerializeField] private string[] messages;
    [SerializeField] private string OnFinishMessage;
    private int messageTodisplay;

    private int CurrentEnemies;
    private bool startWaves;//flag to start waves
    private Transform enemiesCounter;//enemies object
    // Start is called before the first frame update
    private void Start()
    {
        enemiesCounter = GameObject.Find("Enemies").transform;//setup gameobject
        startWaves = false;
    }

    private void OnTriggerEnter(Collider other)//spawns object in on colision
    {
        if (other.tag == "Player")//starts combat
        {
            //sends first message
            Destroy(GameObject.Find("Player").GetComponent<rebotAnimation>());//destroys rebootComponet
            sendMessage(messages[0]);
            messageTodisplay = 1;

            startWaves = true;
            //Debug.Log("Collides");
            Destroy(gameObject.GetComponent<Collider>());
            spawnEnemies();
        }
    }

    private void LateUpdate()
    {
        //Debug.Log(CurrentEnemies);
        if (CurrentEnemies == 0 && startWaves)//next wave
        {
            if (Waves == 0)
            {
                if (OnFinishMessage != "")//displays message once all enemies are destroyed
                {
                    //Debug.Log("onFinish message sent");
                    sendMessage(OnFinishMessage);
                }
                Destroy(gameObject);//destroys after so that child objects of the collider are only destroyed once the fight is over
            }
            else 
            {
                //Debug.Log(messageTodisplay);
                //Debug.Log(messages[messageTodisplay]);
                //displays next message
                if (messages.Length> messageTodisplay && messages[messageTodisplay] != "")
                {
                    sendMessage(messages[messageTodisplay]);
                }
                spawnEnemies();
            }

        }
    }


    private void spawnEnemies()
    {
        Waves--;
        //Debug.Log("player detected");
        for (int i = enemys.Length - 1; i >= 0; i--)
        {
            //Debug.Log("Loop 1 correct");
            for (int j = enemyCount[i]; j > 0; j--)
            {
                //Debug.Log("spawn");
                GameObject enemy = Instantiate(enemys[i], enemyPos[Random.Range(0, enemyPos.Length)], transform.rotation);//creates enemy in specified location
                CurrentEnemies++;
                enemy.GetComponent<HPEnemy>().objectset(this);
                enemy.transform.SetParent(enemiesCounter, true);//organisation
            }
            enemyCount[i] = increasePerWave + enemyCount[i];//increase difficulty each wave
        }
    }

    private void sendMessage(string message)
    {
        //send message to player GUI
        //Debug.Log(message);
        //Debug.Log(messageTodisplay);
        GameObject.Find("Player").GetComponent<objectiveDisplay>().setDisplay(message);
        messageTodisplay++;
    }
    public void enemyDead()
    {
        CurrentEnemies--;
    }
}
