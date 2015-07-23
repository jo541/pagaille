using UnityEngine;
using System.Collections;

public class Tower : EnnemisMain {

	// Use this for initialization
	void Start () {
		player=	GameObject.FindGameObjectWithTag("player0");
		setLife (100);
	}
	
	// Update is called once per frame
	void Update () {

		Quaternion rotate = Quaternion.LookRotation (player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotate, Time.deltaTime * 1);
		timer += Time.deltaTime;
		if (timer >= 0.35f) 
		{
			timer =0;
			instanciateBullet(weaponOfEnemy);
		}

	}
}
