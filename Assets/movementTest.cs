using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class movementTest : MonoBehaviour {

	public Transform vrCamera;


	float speed =7.0f;


	public float upDownSpeed=0.1f;
	float minSpeed=0.1f;
	float maxSpeed=1.5f;

	float tempGyroX=0;
	float tempGyroY=0;


	public bool accDown;
	public bool accUp;

	public float DownDelay=0.7f;
	public float UpDelay=0.7f;
	private CharacterController cc;

	Matrix4x4 baseMatrix = Matrix4x4.identity;

	Vector3 forwardDir;
	Vector3 tempForwardDir;

	float dirDelay=0.0f;

	public float forwardMomentum=0.0f;
	float tempForwardDelay=0.2f;


	GameObject[] moveOj;

	GameObject tempOj;

	GameObject settingDataOj;
	SettingDataScript sds;

	GameObject canvasOj;
	InGameUI gui;

	float tempCurrent=0;
	float tempDelay=0.5f;

	Matrix4x4 calibrationMatrix;

	Vector3 inputDir;

	float elapsed =0.0f;




	// Use this for initialization
	void Start () {
		VRSettings.enabled = true;
		Input.gyro.enabled = true;

		settingDataOj = GameObject.Find ("SettingData");
		sds = settingDataOj.GetComponent<SettingDataScript> ();

		canvasOj = GameObject.Find ("Canvas");
		gui = canvasOj.GetComponent<InGameUI> ();

		cc = GetComponent<CharacterController> ();
		forwardDir=new Vector3 (0,0,0);
		tempForwardDir=new Vector3(0,0,0);
		moveOj = GameObject.FindGameObjectsWithTag ("moveOj");

		tempCurrent = Input.gyro.attitude.eulerAngles.y;
		DownDelay=sds.upDownDelayValue;
		UpDelay=sds.upDownDelayValue;

		Calibrate ();

		tempGyroX = Input.gyro.attitude.eulerAngles.x;
		tempGyroY = Input.gyro.attitude.eulerAngles.y;

		//InvokeRepeating ("Calibrate",0.0f, 1.0f);

		inputDir = AdjustedAccelerometer (Input.acceleration);
	}
	


	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyUp(KeyCode.Escape)) {
			Application.LoadLevel("Setting");
		}


		forwardDir = vrCamera.TransformDirection (Vector3.forward);

		elapsed += Time.deltaTime;

		inputDir = AdjustedAccelerometer (Input.acceleration);


			if (Mathf.Abs (inputDir.y) < sds.capValue) {
				if (inputDir.y < -sds.sensitiveValue) {

					accUp = true;
				}
				if (inputDir.y > sds.sensitiveValue) {
					
					accDown = true;
				}
			}






		if (accUp) 
		{
			UpDelay -= Time.deltaTime;
		}
		if (accDown) 
		{
			DownDelay -= Time.deltaTime;
		}


		if (UpDelay <= 0) 
		{
			Calibrate ();
			UpReset ();
		}
		if (DownDelay <= 0) 
		{
			Calibrate ();
			DownReset ();
		}

		//forward section***********************************************

		if(accDown && accUp) 
		{

			forwardMomentum = sds.forwardDelayValue;
			/*
			if (upDownSpeed < maxSpeed / 3.5f) 
			{
				forwardMomentum = sds.forwardDelayValue / 1.5f;
			} 
			else 
			{
				forwardMomentum = sds.forwardDelayValue;
			}
*/
			UpReset ();
			DownReset ();

		}

		if (sds.LookAroundTrigger) 
		{
			if (dirDelay <= 0) 
			{
				tempForwardDir = forwardDir;
			} 
		}				





		if (forwardMomentum>0) 
		{
			forwardMomentum -= Time.deltaTime;
			Calibrate ();
			upDownSpeed=Mathf.Clamp (upDownSpeed += Mathf.Abs(inputDir.y)/2, minSpeed, maxSpeed);
			dirDelay = 1.0f;

			if (elapsed >= 1.0f)
			{
				elapsed = elapsed % 1.0f;
				Calibrate ();

			}

			/*
			if (Input.gyro.attitude.eulerAngles.x > tempGyroX + sds.gyroSensitiveValue || Input.gyro.attitude.eulerAngles.x < tempGyroX - sds.gyroSensitiveValue || Input.gyro.attitude.eulerAngles.y > tempGyroY + sds.gyroSensitiveValue || Input.gyro.attitude.eulerAngles.y < tempGyroY - sds.gyroSensitiveValue) 
			{

				Calibrate ();
				//Debug.Log ("abc");
				tempGyroX = Input.gyro.attitude.eulerAngles.x;
				tempGyroY = Input.gyro.attitude.eulerAngles.y;
			}
*/


			if (!sds.LookAroundTrigger) 
			{
				cc.SimpleMove (forwardDir * speed*upDownSpeed);
			} 
			else 
			{
				cc.SimpleMove (tempForwardDir * speed*upDownSpeed);
			}


		
			if (sds.SmoothMovement) 
			{
				if (upDownSpeed > maxSpeed / 2) 
				{
					if (accUp) 
					{
						forwardMomentum = sds.forwardDelayValue/2;
					}
					else if (accDown) 
					{
						forwardMomentum = sds.forwardDelayValue/2;	
					}
				}
			}
		} 
		else
		{
			if (Input.gyro.attitude.eulerAngles.x > tempGyroX + sds.gyroSensitiveValue*3 || Input.gyro.attitude.eulerAngles.x < tempGyroX - sds.gyroSensitiveValue*3 || Input.gyro.attitude.eulerAngles.y > tempGyroY + sds.gyroSensitiveValue*3 || Input.gyro.attitude.eulerAngles.y < tempGyroY - sds.gyroSensitiveValue*3) 
			{

				tempForwardDelay = 0.3f;
				Calibrate ();
				tempGyroX = Input.gyro.attitude.eulerAngles.x;
				tempGyroY = Input.gyro.attitude.eulerAngles.y;
			}
			dirDelay -= Time.deltaTime;
		}




		if (tempForwardDelay>0) 
		{
			forwardMomentum = 0;
			tempForwardDelay -= Time.deltaTime;
		}


		if (upDownSpeed > minSpeed && upDownSpeed < maxSpeed/2) 
		{
			//upDownSpeed -= Time.deltaTime * (upDownSpeed*0.2f);
			upDownSpeed -= Time.deltaTime * (upDownSpeed*0.5f);
		}
		else if (upDownSpeed > maxSpeed/2) 
		{
			//upDownSpeed -= Time.deltaTime * (upDownSpeed*0.25f);
			upDownSpeed -= Time.deltaTime * (upDownSpeed*0.7f);
		}




		if (!accDown && !accUp) 
		{
			if (upDownSpeed > minSpeed && upDownSpeed < maxSpeed/2) 
			{				
				upDownSpeed -= Time.deltaTime*upDownSpeed;
			}
			else if (upDownSpeed > maxSpeed/2) 
			{
				upDownSpeed -= Time.deltaTime*upDownSpeed*2;
			}

			if (elapsed >= 1.0f) {
				elapsed = elapsed % 1.0f;
				Calibrate ();
			}
				
		}
		/*
		else if (!accDown || !accUp) 
		{
			if (upDownSpeed > minSpeed && upDownSpeed < maxSpeed/2) 
			{
				//upDownSpeed -= Time.deltaTime * (upDownSpeed*0.2f);
				upDownSpeed -= Time.deltaTime * (upDownSpeed*0.5f);
			}
			else if (upDownSpeed > maxSpeed/2) 
			{
				//upDownSpeed -= Time.deltaTime * (upDownSpeed*0.25f);
				upDownSpeed -= Time.deltaTime * (upDownSpeed*0.7f);
			}

		}

	*/
	

		//input section*******************************************************

		if (Input.GetButton ("Fire1")) {
			
			//Calibrate ();
			Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, 10.0f)) {	
				
				if (hit.collider.gameObject.tag == "door") {
					
					hit.collider.gameObject.transform.position += new Vector3(0,0,1) * Time.deltaTime;
				}
				else if (hit.collider.gameObject.tag == "door2") {
					
					hit.collider.gameObject.transform.position += new Vector3(-1,0,0) * Time.deltaTime;
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



	

	}



	public void Calibrate() 
	{
		
		Vector3 wantedDeadZone = new Vector3 (Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);

		Quaternion rotate = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f),wantedDeadZone);

		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotate, new Vector3(1.0f, 1.0f, 1.0f));

		calibrationMatrix = matrix.inverse;

	}

	public Vector3 AdjustedAccelerometer(Vector3 accelerator) 
	{
		Vector3 accel = this.calibrationMatrix.MultiplyVector (accelerator);
		return accel;
	}


	public void UpReset()
	{
		accUp = false;
		UpDelay = sds.upDownDelayValue;
	}

	public void DownReset()
	{
		accDown = false;
		DownDelay = sds.upDownDelayValue;
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
