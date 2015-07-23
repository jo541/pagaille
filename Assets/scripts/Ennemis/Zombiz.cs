using UnityEngine;
using System.Collections;

public class Zombiz : EnnemisMain {

	// Use this for initialization
	void Start () {
		setLife (50);
	}
	
	// Update is called once per frame
	void Update () {
		if (player!=null && Mathf.Abs(player.transform.position.x - transform.position.x) < 12f && Mathf.Abs(player.transform.position.z - transform.position.z) < 12f ) 
		{
			gameObject.GetComponent<NavMeshAgent> ().SetDestination (player.transform.position);
		}
	}
}
