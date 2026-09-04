//using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private float speed;
    private void Start()
    {
        //speed is difficulty*0.1 + 0.8 so difficulty 2 is normal speed and difficulty 5 is 1.45 speed
        speed = PlayerPrefs.GetInt("Diff") * 0.15f + 0.7f; 
        Time.timeScale = speed;
    }
    public void ChangeVolume(float volume)
    {
        //changes the volume based on a slider
        PlayerPrefs.SetFloat("volume", volume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Pause") > 0.5f)//pauses if button presses
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
            //makes cursor visable
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Debug.Log(AudioListener.volume);
            gameObject.transform.GetChild(0).GetComponentInChildren<Slider>().value = AudioListener.volume;
        }
    }
    public void Unpause()//starts the game again
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 0.15f;
        StartCoroutine(IncreaseTime());//speeds up so the start is not jarring
        //stops the cursor from being visable
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private IEnumerator IncreaseTime()
    {
        while (Time.timeScale < speed)
        {
            yield return new WaitForSeconds(0.1f);
            Time.timeScale *= 1.2f;
            //Debug.Log(Time.timeScale);
        }
        Time.timeScale = speed;
        yield return null;
    }
    public void ChangeScene(string Scene)
    {
        Time.timeScale = 1;//resets speed of time
        gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);//loading screen
        SceneManager.LoadScene(Scene);//loads main menu
    }
}
