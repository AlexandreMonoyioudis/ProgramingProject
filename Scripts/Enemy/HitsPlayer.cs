//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class HitsPlayer : MonoBehaviour
{

    [SerializeField]private int damage;
    [SerializeField]private float knockback;
    private bool IsOnCoolDown;
    private void Start()
    {
        IsOnCoolDown = false;
    }
    private void OnTriggerEnter(Collider other)//damages player on collision and knocks them back
    {
        if (other.CompareTag("Player"))
        {
            PlayerHp script = other.GetComponent<PlayerHp>();
            if (!IsOnCoolDown)//prevents the player from being hit multiple times in quick sucsession
            {
                Invoke(nameof(ResetCoolDown),3);
                IsOnCoolDown = true;
                script.getHit(damage);
            }
            script.nockedBack(knockback,transform.position);
        }
    }
    private void ResetCoolDown()
    {
        IsOnCoolDown = false;
    }
}
