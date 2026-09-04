//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{
    [SerializeField] private bool destroySelf;
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private int[] enemyCount;
    [SerializeField] private Vector3[] enemyPos;
    [SerializeField] private Quaternion[] rotation;
    [SerializeField] private string[] sortingPosition;
    
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)//spawns object in on colision
    {
        if (other.tag == "Player")
        {
            //Debug.Log("player detected");
            for (int i = enemys.Length-1; i >= 0; i--)
            {
                //Debug.Log("Loop 1 correct");
                for (int j = enemyCount[i]; j > 0; j--)
                {
                    //Debug.Log("spawn");
                    GameObject enemy = Instantiate(enemys[i], enemyPos[i], rotation[i]);//creates enemy in specified location
                    enemy.transform.SetParent(GameObject.Find(sortingPosition[i]).transform, true);
                }
            }
            if (destroySelf)
            {
                Destroy(gameObject);//destroys self
            }
            else
            {
                Destroy(this);//destroys own object
            }
        }
        else
        {
            Debug.LogWarning("unececerry collision");//warns for unneccecery processing
        }
    }
}
