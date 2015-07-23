using UnityEngine;
using System.Collections;
using InControl;
using System;

public class PlayerMain : MonoBehaviour {
	public GameObject camera;
	public Quaternion saveRotate;// garde la rotation si le joystick est laché
	public int playerNum;
	public float HP;
	public float speed = 8; 
	private float moveX=0;
	private float moveY=0;
	public bool canMove=true;
	public Vector2 saveMove = new Vector3 (0, 0);
	private bool isOnBumper=false;
	private Vector3 savePos;//en cas de chute
	private bool onCamera;
	public delegate void CallBackMethode(  );
	CallBackMethode skill1;

	public PlayerMain()
	{
		HP = 100;
		speed = 8;
		moveX = 0;
		moveY = 0;


	}
	void Start () {

	}
	public void setHP(float newHP)
	{
		HP = newHP;
	}

	public void setSkill1(CallBackMethode p_callBackExecution)
	{
		 skill1 = new CallBackMethode (p_callBackExecution);//set le comportement du skill apres avoir appuyer sur l1 (executer dans input player)
	}

	public void checkInputPlayer(InputDevice inputDevice) //mouvement du player sur les axes x et z
	{
		inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

		if(inputDevice!=null)
		updateWithInControle (inputDevice);
	}

	IEnumerator onBumper() 
	{
		isOnBumper = true;
		while (isOnBumper) 
		{
			rigidbody.velocity = new Vector3( -moveX*speed , 100, -moveY*speed);//deplacement sur bumper
			yield return new WaitForSeconds(1.5f);
			isOnBumper = false;
		}
	}
	void ifFall()
	{
		if (transform.position.y <= -15) 
		{
			transform.position=savePos;
		}
	}


	public void updateWithInControle(InputDevice inputDevice )
	{
		
    	moveY = 0;
		moveX = 0;
		moveX -= inputDevice.LeftStickX;
		if(moveX == 0)
			moveX -= inputDevice.DPad.X;
		moveY -= inputDevice.LeftStickY;
		if(moveY==0)
			moveY -= inputDevice.DPad.Y;
		//raycast qui vérifie si le joueur n'est pas en train de tomber
		if(!isOnBumper)
		{
			RaycastHit hit;
			if (!isOnBumper&&!Physics.Raycast (transform.position, new Vector3 (0, -0.001f, 0), out hit, 1)) 
			{
				rigidbody.velocity = new Vector3 (-moveX*speed, -14, -moveY*speed);
			} 
			else if (!isOnBumper && Physics.Raycast (transform.position, new Vector3 (0, -0.001f, 0), out hit, 1)&&hit.collider.name =="Booster" ||hit.collider.name =="Bumper" )
			{
				if(hit.collider.name =="Booster") 
				{
					rigidbody.velocity = new Vector3( -moveX* speed *3, 0,-moveY* speed *3);//deplacement sur acceletateur
				}
				if(hit.collider.name =="Bumper"&&!isOnBumper)
				{
					StartCoroutine(onBumper());
				}
			}
			else
			{
				if(canMove)
				{
					rigidbody.velocity = new Vector3( -moveX* speed, 0,-moveY* speed);//deplacement normal
					savePos=transform.position;
					if(moveX !=0 && moveY != 0)
					{
						saveMove = new Vector2(moveX,moveY);
					}

				}
				else
				{
					rigidbody.velocity = new Vector3( -saveMove.x* speed, 0,-saveMove.y* speed);//deplacement normal
				}

			}
		}
		changePlayer (inputDevice);
		//en cas de chute
		ifFall ();
		//execution des skills
		if(inputDevice.LeftBumper.WasPressed && skill1 != null)
			skill1 ();
	}
	public void takeDamage(float damage)
	{
		HP -= damage;
		if (HP <= 0) 
		{
			Destroy(gameObject);
		}
	}
	public void changePlayer(InputDevice inputDevice)
	{
		if (inputDevice.Action4.WasPressed  ) 
		{
		

		}
	}
	
}