//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class rebotAnimation : MonoBehaviour
{
    private TextMeshProUGUI display; 
    private float counter;//times when to do the next animation
    private int rotationCounter;//conuntes which part of the load rotation it is at

    // Start is called before the first frame update
    private void Start()
    {
        display = GameObject.Find("Objective").GetComponent<TextMeshProUGUI>();
        counter = 0;
        rotationCounter = -1;
        GameObject.Find("Player").GetComponent<objectiveDisplay>().setDisplay("rebooting systems");
    }

    // Update is called once per frame
    private void Update()//rotates for loading
    {
        counter += Time.deltaTime;
        if (counter >= 5)
        {
            counter-=0.2f;
            rotationCounter++;
            switch (rotationCounter) {//rotationCounter chooses the rotation orientation
                case 0:
                    display.text = "rebooting systems \\ ";// the \\ is used to display a single \
                    break;

                case 1:
                    display.text = "rebooting systems |";
                    break;

                case 2:
                    display.text = "rebooting systems /";
                    break;

                case 3:
                    display.text = "rebooting systems -";
                    rotationCounter = -1;//resets to begginging
                    break;
            }

        }
    }
}
