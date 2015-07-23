using UnityEngine;
using System.Collections;
using System;
using InControl;

public class BrawlerPlayers : PlayerMain{
	public bool canFist=true;
	// Use this for initialization
	void Start () {
		setSkill1 (dodge);
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

		var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices [playerNum] : null;
		checkInputPlayer (inputDevice);
		if (inputDevice != null && canMove ) 
		{
			if (Mathf.Abs (inputDevice.LeftStickX) >= 0.19 || Mathf.Abs (inputDevice.LeftStickY) >= 0.19 ) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, Mathf.Atan2 (inputDevice.LeftStickX, inputDevice.LeftStickY) * Mathf.Rad2Deg, 0));		
				if (transform.rotation.eulerAngles.y != 180 && transform.rotation.eulerAngles.y != -180)
					saveRotate = transform.rotation;
			}
			else 
			{
				transform.rotation = saveRotate;	
			}
			fistHit (inputDevice);
		}
		else 
		{
			transform.rotation = saveRotate;	
		}
		


			
			
	}
	 public void dodge()
	{
		if(canMove)
		StartCoroutine (dodgeCorouts());
	}

	IEnumerator dodgeCorouts()
	{
		//speed = 80;
		float timeDodge = 0.1f;
		float speed = 50;
		float timer = 0;
		canMove = false;
		while (timer<timeDodge) 
		{
			timer += Time.deltaTime;
			rigidbody.velocity = new Vector3( transform.TransformDirection(Vector3.forward).x*speed,0,transform.TransformDirection(Vector3.forward).z*speed);
			Debug.Log (transform.TransformDirection(Vector3.forward));
			yield return 0 ;
		}
		saveMove =new Vector2(0, 0);
		yield return new WaitForSeconds(timeDodge+0.03f);//petit temps d'attente apres esquive
		speed = 8;
		canMove = true;
		yield return 0;
	}

	public void fistHit(InputDevice inputDevice)
	{
		if(inputDevice.Action3.WasPressed && canMove)
		{
			StartCoroutine(fistHitCoroots());
		}
	}
	IEnumerator fistHitCoroots()
	{
		float timer = 0;
		float speed = 10;
		canMove=false;
		while (timer < 0.1 ) 
		{
			timer += Time.deltaTime;
			rigidbody.velocity = new Vector3( transform.TransformDirection(Vector3.forward).x*speed,0,transform.TransformDirection(Vector3.forward).z*speed);

			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out hit, 2) && hit.collider.gameObject.tag.Substring(0,6) == "player" &&canFist) 
			{
				canFist=false;
				//canFist= false;
				hit.collider.gameObject.GetComponent<PlayerMain>().takeDamage(20);

			}
			else if(Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out hit, 2) && hit.collider.gameObject.tag == "Ennemi")
			{
				hit.collider.gameObject.GetComponent<EnnemisMain>().takeDamage(20);
			}
			yield return 0 ;
		}
		saveMove =new Vector2(0, 0);
		yield return new WaitForSeconds (0.1f);
		canMove = true;
		canFist = true;
		yield return 0;
	}


}
