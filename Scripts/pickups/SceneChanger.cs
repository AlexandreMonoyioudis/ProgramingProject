//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    [SerializeField]private string sceneToChangeTo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
                SceneManager.LoadScene(sceneToChangeTo);//Changes scene
        }
    }

}
