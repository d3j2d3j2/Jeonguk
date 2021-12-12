using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Money_Zone_S : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player1")
		{
			GameObject.Find("UI_Setting").GetComponent<UI_Setting_S>().Start_UI();
		}
	}
}
