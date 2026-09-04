using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    [SerializeField]private float sensitivityX;
    [SerializeField]private float sensitivityY;

    [SerializeField] private Transform orientation;//camera holder to rotate

    private float xRotation;
    private float yRotation;
   

    // Update is called once per frame
    private void FixedUpdate()
    {
        float mouseSpinX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseSpinY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        xRotation -= mouseSpinY;
        yRotation += mouseSpinX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//limit rotation

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    // Start is called before the first frame update
    private void Start()
    {
        //stops the cursor from being visable
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //changes sensitity to what the player set it to be
        if (PlayerPrefs.HasKey("x"))
        {
            sensitivityX = PlayerPrefs.GetFloat("x");
        }

        if (PlayerPrefs.HasKey("y"))
        {
            sensitivityY = PlayerPrefs.GetFloat("y");
        }
    }
}