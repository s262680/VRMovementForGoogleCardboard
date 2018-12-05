using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class autoMovementScript : MonoBehaviour {

	public Transform vrCamera;

	bool movementTrigger=false;

	private CharacterController cc;



	GameObject[] moveOj;

	GameObject tempOj;

	GameObject canvasOj;
	InGameUI gui;

	Vector3 forward=new Vector3(0,0,0);

	Ray ray;
	RaycastHit hit;

	float gazeTime=2.0f;


	// Use this for initialization
	void Start () {
		VRSettings.enabled = true;




		canvasOj = GameObject.Find ("Canvas");
		gui = canvasOj.GetComponent<InGameUI> ();

		cc = GetComponent<CharacterController> ();

		moveOj = GameObject.FindGameObjectsWithTag ("moveOj");

		ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		hit = new RaycastHit ();

	}



	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyUp(KeyCode.Escape)) {
			Application.LoadLevel("StartMenu");
		}



		 forward = vrCamera.TransformDirection (Vector3.forward);


		if (movementTrigger) {
			cc.SimpleMove (forward * 10);
			
		}

		ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
		hit = new RaycastHit ();
	
		if (Physics.Raycast (ray, out hit, 10.0f)) {	

			if (hit.collider.gameObject.tag == "door") {
				if (gazeTime > 0) {
					gazeTime -= Time.deltaTime;
				} 
				else if (gazeTime <= 0) {
					hit.collider.gameObject.transform.position += new Vector3(0,0,1) * Time.deltaTime;
				}
			} 
			else if (hit.collider.gameObject.tag == "door2") {
				if (gazeTime > 0) {
					gazeTime -= Time.deltaTime;
				} 
				else if (gazeTime <= 0) {
					hit.collider.gameObject.transform.position += new Vector3(-1,0,0) * Time.deltaTime;
				}
			} 
			else if (hit.collider.gameObject.tag == "moveOj") {
				if (gazeTime > 0) {
					gazeTime -= Time.deltaTime;
				} else if (gazeTime <= 0) {
					tempOj = hit.collider.gameObject;
					hit.collider.gameObject.GetComponent<Rigidbody> ().useGravity = false;
					hit.collider.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
				}
			}
			else 
			{
				gazeTime = 2.0f;
				if (tempOj != null) 
				{
					tempOj.GetComponent<Rigidbody> ().useGravity = true;
				}
		
			}
		} else {
			gazeTime = 2.0f;
			if (tempOj != null) 
			{
				tempOj.GetComponent<Rigidbody> ().useGravity = true;
			}
		}


		//input section*******************************************************

		if (Input.GetButtonDown ("Fire1")) {

			if (!movementTrigger) {
				movementTrigger = true;
			} else {
				movementTrigger = false;
			}
		}
		/*
			Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, 10.0f)) {	

				if (hit.collider.gameObject.tag == "door") {

					hit.collider.gameObject.transform.position +=transform.right*Time.deltaTime;
				}

				else if (hit.collider.gameObject.tag == "moveOj") {
					tempOj = hit.collider.gameObject;
					hit.collider.gameObject.GetComponent<Rigidbody> ().useGravity = false;
					hit.collider.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
				}
				else 
				{
					if (tempOj != null) 
					{
						tempOj.GetComponent<Rigidbody> ().useGravity = true;
					}
				}


			}
		} 
		else 
		{
			if (tempOj != null) 
			{
				tempOj.GetComponent<Rigidbody> ().useGravity = true;
			}
		}
		*/

	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "stageTrigger") 
		{

			gui.stage = 2;
			other.gameObject.tag = "Untagged";
		}
	}




}