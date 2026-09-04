using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int Hp;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Diff") == 5)
        {
            Hp = 1;
        }
        getRepeaired(0);
    }
    public bool getRepeaired(int heal)
    {
        if (Hp == 99){return false;}
        Hp += heal;
        GameObject.Find("Display Canvas").GetComponentInChildren<Graphic>().color = new Color(0.5f, 1, 0.5f, 1);//green
        Invoke(nameof(resetColour), 0.1f);
        if (Hp > 99)
        {
            Hp = 99;
        }
        updateHPDisplay();
        return true;
    }
    public void getHit(int dam)
    {
        Hp -= dam;
        int diff = PlayerPrefs.GetInt("Diff");
        if (diff >= 3)
        {
            Hp -= 5;
        }
        if(diff == 1)
        {
            Hp += 10;
        }
        GameObject.Find("Display Canvas").GetComponentInChildren<Graphic>().color = new Color(1, 0f, 0f, 1);//red
        Invoke(nameof(resetColour), 0.1f);
        if (Hp <= 0)
        {
            updateHPDisplay(0);
            playerDie();
        }
        else
        {
            updateHPDisplay();
        }
    }
    private void resetColour()
    {
        CancelInvoke();//prevents overlapping effects from taking multiple hits in close sucession
        RawImage image = GameObject.Find("Display Canvas").GetComponentInChildren<RawImage>();
        image.color = new Color(image.color.r + 0.1f, image.color.g + 0.1f, image.color.b + 0.1f, 1);
        if (image.color.b < 1f)
        {
            Invoke(nameof(resetColour), 0.1f);
        }
    }
    private void playerDie()
    {
        //stops the users ability to control the player
        Destroy(gameObject.GetComponent<PlayerMovement>());
        Destroy(Camera.main.GetComponent<CameraRotation>());
        Destroy(Camera.main.GetComponentInParent<CameraScript>());
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.transform.GetChild(0).gameObject);
        //creates the effect of the player dying
        Camera.main.AddComponent<SphereCollider>();
        Camera.main.AddComponent<Rigidbody>();
        CancelInvoke();
        Invoke(nameof(endDeathAnimation), 2);
    }
    private void endDeathAnimation()
    {
        //ends the dying effect
        Destroy(Camera.main.GetComponent<SphereCollider>());
        Destroy(Camera.main.GetComponent<Rigidbody>());
        Invoke("Respawn", 1.5f);
    }
    private void Respawn(){//respawns the player
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);//restarts the scene
    }

    public void nockedBack(float nockBack,Vector3 pos)//nocks the player backwards
    {
        //Debug.Log("nockedBack runs");
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(nockBack * new Vector3(0,0.1f,0),ForceMode.Impulse);
        rb.AddForce(nockBack * (transform.position- new Vector3(pos.x, transform.position.y, pos.z)).normalized,ForceMode.Impulse);//launches the player relitive to its posistion
    }

    private void updateHPDisplay()
    {
        //displays HP
        GameObject.Find("HPDisplay").GetComponent<TextMeshProUGUI>().text = "HP: " + Hp.ToString();
    }
    private void updateHPDisplay(int hp)
    {
        //displays HP
        GameObject.Find("HPDisplay").GetComponent<TextMeshProUGUI>().text = "HP: " + hp.ToString();
    }
}