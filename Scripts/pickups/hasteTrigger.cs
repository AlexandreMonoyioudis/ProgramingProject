//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class hasteTrigger : MonoBehaviour
{
    [SerializeField]private int setValue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setHaste(setValue);
        }
        else//tells me if it is triggering on somthing it should not
        {
            Debug.Log("Unecisary Collition");
        }
    }
}
