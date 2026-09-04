using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneOnDestroy : MonoBehaviour
{
    [SerializeField] private string sceneToChangeTo;
    private void OnDestroy()
    {
        //makes cursor visable
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Changes scene
        SceneManager.LoadScene(sceneToChangeTo);
    }

}
