//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class HPEnemy : MonoBehaviour
{
    [SerializeField] private int Hp;
    private SpriteRenderer sp;
    private combatZoneStarter sc;
    private Color col;
    private void Start()
    {//setting up variables
        sp = gameObject.transform.GetComponentInChildren<SpriteRenderer>();
        col = sp.color;
    }
    public void objectset(combatZoneStarter Sc)
    {
        sc = Sc;
    }
    public void dealDamage(int dam)
    {
        Hp -= dam;
        sp.color = Color.red;
        Invoke(nameof(resetColour), 0.25f);
    }
    private void die()//runs when the player dies
    {   
        gameObject.transform.Find("Dead").gameObject.SetActive(true);
        gameObject.transform.Find("Dead").transform.SetParent(GameObject.Find("Objects").transform, true);
        if (sc != null)//updates wave info
        {
            sc.enemyDead();
        }
        Destroy(gameObject);
    }
    private void resetColour()
    {
        sp.color = col;
        if (Hp <= 0) { die(); }
    }
}