using UnityEngine;

public class projectileTrigger : MonoBehaviour
{
	[SerializeField] private int damage;
	[SerializeField] private float speed;
	private float timer;//to prevent useless objectsx
	private int counter;
	public Vector3 spread;//so all bullets dont go in a perfectly strait line

	// Start is called before the first frame update
	void Start()
	{
		timer = 25;
	}

	void FixedUpdate()
	{
        if (counter <30)
        {
			counter++;
			//finds the closest enemy and looks at it
			GameObject player = GameObject.FindGameObjectWithTag("MainCamera");
			if (spread == null) spread = new Vector3(0, 0, 0);

			if (player != null)//prevents errors
			{
				transform.LookAt(player.transform.position + spread);//adds spread to direction
			}
			else
			{
				Debug.Log("MainCamera not found");
			}
		}
		//checks for the projectiles no collisions
		timer -= Time.deltaTime; if (timer < 0)
		{
			projectileHit();
		}
		//launches the projecile
		transform.Translate(0,0 , speed/100);
		
	}
	private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))//deals damage to the player if they collide
		{
            PlayerHp script = other.GetComponent<PlayerHp>();
            script.getHit(damage);
        }
		projectileHit();
    }

	private void projectileHit()//handles effects for projectile collision
    {
		//effects for projectile destroying goes here
		Destroy(gameObject);//destroys self after collision
	}
}
