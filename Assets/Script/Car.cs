using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
	
	public GameObject player;
	public GameObject monopoly;
	
	public void Move()
	{
		int nextInd = player.GetComponent<Player>().position;
		GameObject land = GameObject.Find(nextInd.ToString());
		Vector3 nextPosition = land.transform.position;
		gameObject.transform.position
			= new Vector3((gameObject.transform.position.x + land.transform.position.x) / 2
			, gameObject.transform.position.y
			, (gameObject.transform.position.z + land.transform.position.z) / 2);
	}
	public void Rotate()
    {
		if (player.GetComponent<Player>().position / 8 == 0) gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
		else if (player.GetComponent<Player>().position / 8 == 1) gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		else if (player.GetComponent<Player>().position / 8 == 2) gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
		else if (player.GetComponent<Player>().position / 8 == 3) gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Rotate();
	}
}
