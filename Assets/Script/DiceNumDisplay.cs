using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumDisplay : MonoBehaviour {
	public int num;
	public Monopoly monopoly;
	public string gameObjectName;
	public bool chk;
	// Use this for initialization
	void Start () {
		gameObjectName = gameObject.name;
		num = int.Parse(gameObjectName.Substring(1));
		monopoly = GameObject.Find("Monopoly").GetComponent<Monopoly>();
	}
	
	// Update is called once per frame
	void Update () {
		if (monopoly.progress == Monopoly.GameProgress.Move)
		{
			if (monopoly.diceValue == num) gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
		else gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}
