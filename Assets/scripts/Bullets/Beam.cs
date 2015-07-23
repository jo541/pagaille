using UnityEngine;
using System.Collections;

public class Beam : BulletMain {

	// Use this for initialization
	void Start () {
		setSpeed (8);
		transform.position += new Vector3 (0, 0.25f, 0);
	}
	public  void Update()
	{
		transform.Translate(speed*Vector3.forward*Time.deltaTime);
		transform.localScale += new Vector3 (0.06f, 0, 0);
		timer += Time.deltaTime;
		if (timer >= lifeTime) 
		{
			transform.localScale -= new Vector3 (0.25f, 0, 0);
		}
		if (transform.localScale.x <= 0)
			Destroy (gameObject);
	}
	
	// Update is called once per frame

}
