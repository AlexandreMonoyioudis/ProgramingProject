using UnityEngine;
//using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuControls : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            //changes the slider based on a volume
            GameObject.Find("Volume").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        }
        if (PlayerPrefs.HasKey("Diff"))
        {
            //changes difficulty
            Slider slider = GameObject.Find("difficulty").GetComponent<Slider>();
            slider.value = PlayerPrefs.GetInt("Diff");
            slider.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = slider.value.ToString();
        }
        else//sets a default difficulty if it has not been altered before
        {
            PlayerPrefs.SetInt("Diff", 2);
        }
        if (PlayerPrefs.HasKey("x"))
        {
            //changes sense
            Slider slider = GameObject.Find("X Sensitivity").GetComponent<Slider>();
            slider.value = PlayerPrefs.GetFloat("x");
            slider.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "X   " + slider.value;
        }
        if (PlayerPrefs.HasKey("y"))
        {
            //changes sense
            Slider sliderVar = GameObject.Find("Y Sensitivity").GetComponent<Slider>();
            sliderVar.value = PlayerPrefs.GetFloat("y");
            sliderVar.gameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Y   " + sliderVar.value;
        }
        GameObject.Find("Settings").SetActive(false);
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);//Changes scene
    }

    public void ChangeVolume(float volume)
    {
        //changes the volume based on a slider
        PlayerPrefs.SetFloat("volume", volume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        PlayerPrefs.Save();
    }
    public void ChangeXsens(float sens)
    {
        //changes the volume based on a slider
        PlayerPrefs.SetFloat("x", sens);
        PlayerPrefs.Save();
        GameObject.Find("X Sensitivity").transform.GetComponentInChildren<TextMeshProUGUI>().text = "X   " + sens;
    }
    public void ChangeYsens(float sens)
    {
        //changes the volume based on a slider
        PlayerPrefs.SetFloat("y", sens);
        PlayerPrefs.Save();
        GameObject.Find("Y Sensitivity").transform.GetComponentInChildren<TextMeshProUGUI>().text = "Y   " + sens;
    }
    public void ChangeDifficulty(float diff)
    {
        //changes the volume based on a slider

        PlayerPrefs.SetInt("Diff", (int)diff);
        PlayerPrefs.Save();
        GameObject.Find("difficulty").transform.GetComponentInChildren<TextMeshProUGUI>().text = diff.ToString();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
