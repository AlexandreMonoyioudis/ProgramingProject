//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class teleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 newPos;
    private void OnDestroy()
    { GameObject.Find("Player").transform.position = newPos; }
}
