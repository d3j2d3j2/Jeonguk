using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	public int landNum;
	public int grade;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Map.Land land = Map.landArray[landNum];
		MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
		if (land.build[grade] == true)
        {
			if (mr.enabled == false)
			{
				AudioSource source = GameObject.Find("BuildingSound").GetComponent<AudioSource>();
				AudioClip clip = source.clip;
				source.PlayOneShot(clip);

			}
			mr.enabled = true;
			
			if (land.owner == Monopoly.PlayerType.Player1)
            {
				for(int i=0; i < mr.materials.Length; i++)
                {
					mr.materials[i].color = Color.red;
                }
            }
			else if(land.owner == Monopoly.PlayerType.Player2)
            {
				for (int i = 0; i < mr.materials.Length; i++)
				{
					mr.materials[i].color = Color.blue;
				}
			}
        }
        else
        {
			mr.enabled = false;
		}
	}
}
