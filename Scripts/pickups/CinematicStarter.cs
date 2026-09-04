using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CinematicStarter : MonoBehaviour
{
    //objects that area spawnied in the duration of the scene
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject Eye;
    [SerializeField] private GameObject Rock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Objective").GetComponent<TextMeshProUGUI>().text = "...";//display text
            Physics.gravity *= 10;//increase gravity
            Camera.main.transform.SetPositionAndRotation(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - 7), new Quaternion(0, 90, 0, 0));
            Destroy(Camera.main.transform.GetComponent<CameraRotation>());//prevents the user from rotating the camera
            Destroy(GameObject.Find("Player"));//prevemts the player from moving
            GameObject rock = Instantiate(Rock, new Vector3(297, 300, 118), transform.rotation);//spawns the rock so it falls fast with the increased gravity
            StartCoroutine(CheckIfRockOnGround(rock));
        } 
    }

    private IEnumerator CheckIfRockOnGround(GameObject rock)//when rock hits ground
    {
        while (rock.transform.position.y > 5)
        {//waiting for rock to hit the ground
            yield return null;
        }
        Destroy(rock);
        Physics.gravity /= 10;//reduce gravity
        EnemySpawnAnimation();
    }

    private void EnemySpawnAnimation()
    {
        Instantiate(Eye, new Vector3(297, 4, 118), transform.rotation);
        Instantiate(smoke, new Vector3(297, 4, 118), transform.rotation);
        Invoke(nameof(teleport), 2); 
    }
    private void teleport()
    {
        GameObject.Find("Display Canvas").GetComponentInChildren<Graphic>().color = new Color(0, 0, 0, 1);//green
        SceneManager.LoadScene("Level 3");//Changes scene
    }
}
