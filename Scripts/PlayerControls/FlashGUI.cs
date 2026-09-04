using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FlashGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        //changes the flash frequency reducing the lack of GUI over time
        float subTractionNum = 0.005f;

        for (float i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.1f+subTractionNum);
            subTractionNum *= 2;
            sleepChilden();
            yield return new WaitForSeconds(0.1f);
            sleepChilden();
        }
        Destroy(this);//destroys the script as it is no longer needed
        yield return null;
    }



    private void sleepChilden()//sleeps all children or wakes up children
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy == true)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
