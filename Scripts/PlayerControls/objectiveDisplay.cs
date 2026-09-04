using UnityEngine;
using TMPro;

public class objectiveDisplay : MonoBehaviour
{
    private TextMeshProUGUI display;
    private string displayText;
    private string currentlyDisplaying;
    private int displayCounter;
    // Start is called before the first frame update
    private void Awake()//sets variables
    {
        display = GameObject.Find("Objective").GetComponent<TextMeshProUGUI>();
    }

    private void Display()
    {
        display.text = currentlyDisplaying;//displays next letter

        if (displayCounter < displayText.Length)//continues to display letter by letter
        {
            displayCounter++;
            currentlyDisplaying = displayText.Substring(0, displayCounter);
            Invoke("Display", 0.2f);
        }
    }
    public void setDisplay(string newText)
    {
        
        CancelInvoke();//prevents multiple texts being displayed symltaniously 
        displayCounter = 0;
        displayText = newText;
        //Debug.Log(displayText);
        currentlyDisplaying = "";
        Display();
    }
}
