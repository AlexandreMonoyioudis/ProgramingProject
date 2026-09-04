using UnityEngine;

public class TempBarricade : MonoBehaviour
{
    void LateUpdate()
    {
        if (GameObject.Find("Enemies").transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
