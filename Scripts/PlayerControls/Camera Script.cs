using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] private Transform cameraPosition;
    private void Update()
    {
        transform.position = cameraPosition.position;//moves camera to player position
    }
}

