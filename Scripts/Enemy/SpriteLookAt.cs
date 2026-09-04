using UnityEngine;

public class SpriteRotater : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = Camera.main.transform;
    }
    private void Update()
    {
        transform.LookAt(player);
    }
}
