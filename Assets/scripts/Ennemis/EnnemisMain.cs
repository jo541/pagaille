using UnityEngine;
using System.Collections;

public class EnnemisMain : MonoBehaviour {
	public float HP ;
	public GameObject weaponOfEnemy;
	public float timer;
	public GameObject player;

	public EnnemisMain()
	{
		HP = 300;
		timer =0;
	}
	public void setLife(float newHp)
	{
		HP = newHp;
	}
	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void takeDamage(float damage)
	{
		HP -= damage;
		if (HP <= 0) 
		{
			Destroy(gameObject);
		}
	}

	 public void instanciateBullet(GameObject newBullet)
	{
		GameObject go = Instantiate (newBullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		go.transform.rotation = transform.rotation;
		go.name = "Ennemi";
	}

}
