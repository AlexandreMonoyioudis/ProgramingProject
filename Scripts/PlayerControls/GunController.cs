using UnityEngine;

public class GunController : MonoBehaviour
{
    private int childCount;
    private int currentlySelected;
    // Start is called before the first frame update
    void Start()
    {
        sleepChilden();
        transform.GetChild(0).gameObject.SetActive(true);//sets child active
        currentlySelected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        childCount = transform.childCount;
        if (Input.GetAxisRaw("Weapon1") > 0.5f && currentlySelected!= 0)
        {
            sleepChilden();
            currentlySelected = 0;
            transform.GetChild(0).gameObject.SetActive(true);//sets child active
        }
        else if (Input.GetAxisRaw("Weapon2") > 0.5f && childCount >= 2 && currentlySelected != 1)
        {
            sleepChilden();
            currentlySelected = 1;
            transform.GetChild(1).gameObject.SetActive(true);//sets child active
        }
        else if (Input.GetAxisRaw("Weapon3") > 0.5f && childCount >= 3 && currentlySelected != 2)
        {
            sleepChilden();
            currentlySelected = 2;
            transform.GetChild(2).gameObject.SetActive(true);//sets child active
        }
    }
    private void sleepChilden()//sleeps all children
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
