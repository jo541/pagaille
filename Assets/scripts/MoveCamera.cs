using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	private Quaternion setRotation;
	public GameObject target;
	// Use this for initialization
	void Start () {
		setRotation = transform.rotation;

		StartCoroutine(cameraPos ());
	}
	
	// Update is called once per frame
	void Update () {


	}
	IEnumerator cameraPos()
	{
		while (true) 
		{
			if(gameObject != null)
			{
				transform.rotation = Quaternion.Euler(70,0,0);
				if(target.name.Substring(0,6) =="Player" )
				{
					transform.position =new Vector3( target.transform.position.x,target.transform.position.y+17,target.transform.position.z-6f);
				}
				
				yield return 0 ;
			}

		}
	}
}
