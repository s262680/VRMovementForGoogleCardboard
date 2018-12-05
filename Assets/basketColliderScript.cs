using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketColliderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "moveOj") 
		{

			other.gameObject.tag = "Untagged";
		}

	}
}
