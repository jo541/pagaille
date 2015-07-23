using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using InControl;


public class GunnerPlayers : PlayerMain {


	private Quaternion targetRot;
	public GameObject bulletSimple;
	public GameObject beam;
	private float timer=0;
	private float rateOfFire = 0.1f;



	// Use this for initialization
	void Start () {
		setSkill1 (beamSkill);
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		Debug.Log (camera.tag);
	}
	
	// Update is called once per frame
	void Update () {
				var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices [playerNum] : null;
				checkInputPlayer (inputDevice);
				if (inputDevice != null) {
						if (Mathf.Abs (inputDevice.RightStickX) >= 0.65 || Mathf.Abs (inputDevice.RightStickY) >= 0.65) {
								timer += Time.deltaTime;
								transform.rotation = Quaternion.Euler (new Vector3 (0, Mathf.Atan2 (inputDevice.RightStickX, inputDevice.RightStickY) * Mathf.Rad2Deg, 0));		
								if (transform.rotation.eulerAngles.y != 180 && transform.rotation.eulerAngles.y != -180)
										saveRotate = transform.rotation;
								if (timer >= rateOfFire) {
										timer = 0;
										instanciateBullet (bulletSimple);
								}
						} else {
								timer = 0;
								transform.rotation = saveRotate;	
						}

						if (inputDevice.LeftStickX == 0 && inputDevice.LeftStickY == 0) {
								rateOfFire = 0.1f;
						} else {
								rateOfFire = 0.2f;
						}

				}
		}
	void instanciateBullet(GameObject newBullet)
	{
		GameObject go = Instantiate (newBullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		go.transform.rotation = transform.rotation;
		if (newBullet.gameObject.name == "Beam")
			go.transform.Translate( Vector3.forward *1.2f);
		go.name = "player" + playerNum;
	}
	void beamSkill()
	{
		instanciateBullet (beam);
	}



}
