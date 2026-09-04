//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class healsPlayer : MonoBehaviour
{
    [SerializeField] private int heal;
    private void Start()
    {
        int diff = PlayerPrefs.GetInt("Diff");
        if (diff == 1)
        {
            heal*=2;
        }
        else if (diff >= 4)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)//heals the player if Hp <99
    {
        if (other.CompareTag("Player"))
        {
            PlayerHp script = other.GetComponent<PlayerHp>();
            if (script.getRepeaired(heal))
            {
                Destroy(gameObject);
            }
        }
    }
}
