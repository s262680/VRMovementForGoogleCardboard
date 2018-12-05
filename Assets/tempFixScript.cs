using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempFixScript : MonoBehaviour {
	//GameObject[] moveOj;

	// Use this for initialization
	void Start () {
		//moveOj = GameObject.FindGameObjectsWithTag ("moveOj");

	}
	
	// Update is called once per frame
	void Update () {

		if (this.gameObject.transform.position.y < -3.0f) 
		{
			this.gameObject.transform.position = new Vector3(this.transform.position.x,0,this.transform.position.z);
		}
	}
	/*
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "moveOj") 
		{
			Debug.Log ("abc");
			Rigidbody rb=other.GetComponent<Rigidbody>();
			rb.AddForce (transform.up * 20);
		}
	}
	*/
}
