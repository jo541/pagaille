using UnityEngine;
using System.Collections;

public class BulletMain : MonoBehaviour {
	public float power;
	public float speed ;
	public float lifeTime;
	public float timer = 0;
	public int playerID;//set a l'instantiation
	public BulletMain()
	{
		speed = 15;
	}
	public void setSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= lifeTime) 
		{
			Destroy(gameObject);
		}
		transform.Translate(speed*Vector3.forward*Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag != transform.name) 
		{

			Destroy (gameObject);
			if(col.tag.Substring(0,6) =="player")
			{

				 col.GetComponent<PlayerMain>().takeDamage(power);
			}
			else if(col.tag == "Ennemi")
			{
				col.GetComponent<EnnemisMain>().takeDamage(power);
			}
		}
		 

	}
}
