using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
public class StartMenuScript : MonoBehaviour {

	public Button autoMovement;
	public Button projectMovement;

	// Use this for initialization
	void Start () {
		VRSettings.enabled = false;
		autoMovement.onClick.AddListener(delegate {OnAutoButtonClick(); });
		projectMovement.onClick.AddListener(delegate {OnProjectButtonClick(); });
	}

	public void OnAutoButtonClick()
	{
		
		Application.LoadLevel("autoMovement");
	}
	public void OnProjectButtonClick()
	{
		Application.LoadLevel("Setting");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
