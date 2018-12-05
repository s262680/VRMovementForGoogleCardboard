using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingDataScript : MonoBehaviour {

	public float sensitiveValue=0;
	public float upDownDelayValue=0;
	public float forwardDelayValue=0;
	public float capValue=0;
	public float gyroSensitiveValue=0;
	public bool LookAroundTrigger=false;
	public bool useGyro=false;
	public bool SmoothMovement=false;
	public bool keepValueTrigger=false;
	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
