using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class SettingMenuScript : MonoBehaviour {
	
	public Slider sensitiveSlider;
	public Slider upDownDelaySlider;
	public Slider forwardDelaySlider;
	public Slider capSlider;
	public Slider gyroSensitiveSlider;
	public Toggle LooKAroundToggle;
	public Toggle useGyroToggle;
	public Toggle SmoothMovementToggle;
	public Button StartButton;
	public Text sensitiveText;
	public Text UpDownDelayText;
	public Text forwardDelayText;
	public Text capText;
	public Text gyroSensitiveText;


	public GameObject settingDataOjFab;
	GameObject settingDataOj;
	SettingDataScript sds;

	// Use this for initialization
	void Start () {
		VRSettings.enabled = false;
		//settingDataOj = GameObject.Find ("SettingData");
		if (!GameObject.Find (settingDataOjFab.name)) 
		{
			SpawnSettingObject ();
		} 
		else 
		{
			settingDataOj = GameObject.Find ("SettingData");
		}
		sds = settingDataOj.GetComponent<SettingDataScript> ();

		/*
		sensitiveText = transform.Find ("SensitveText").GetComponent<Text> ();
		UpDownDelayText = transform.Find ("UpDownDelayText").GetComponent<Text> ();
		forwardDelayText = transform.Find ("ForwardDelayText").GetComponent<Text> ();
		capText = transform.Find ("CapText").GetComponent<Text> ();
		gyroSensitiveText = transform.Find ("GyroSensitiveText").GetComponent<Text> ();
		*/

		if (sds.keepValueTrigger) 
		{
			
			sensitiveSlider.value=sds.sensitiveValue;
			upDownDelaySlider.value=sds.upDownDelayValue;
			forwardDelaySlider.value=sds.forwardDelayValue;
			capSlider.value=sds.capValue;
			gyroSensitiveSlider.value=sds.gyroSensitiveValue;
			LooKAroundToggle.isOn=sds.LookAroundTrigger;
			useGyroToggle.isOn=sds.useGyro;
			SmoothMovementToggle.isOn=sds.SmoothMovement;
		} 
		else 
		{
			sds.sensitiveValue = sensitiveSlider.value;
			sds.upDownDelayValue = upDownDelaySlider.value;
			sds.forwardDelayValue = forwardDelaySlider.value;
			sds.capValue = capSlider.value;
			sds.gyroSensitiveValue = gyroSensitiveSlider.value;
			sds.LookAroundTrigger = LooKAroundToggle.isOn;
			sds.useGyro = useGyroToggle.isOn;
			sds.SmoothMovement = SmoothMovementToggle.isOn;
		}
		sensitiveSlider.onValueChanged.AddListener(delegate {SensitiveSitting(); });
		upDownDelaySlider.onValueChanged.AddListener(delegate {upDownDelaySitting(); });
		forwardDelaySlider.onValueChanged.AddListener(delegate {ForwardDelaySitting(); });
		capSlider.onValueChanged.AddListener(delegate {CapSitting(); });
		gyroSensitiveSlider.onValueChanged.AddListener(delegate {GyroSensitiveSitting(); });
		LooKAroundToggle.onValueChanged.AddListener(delegate {LookAroundSitting(); });
		useGyroToggle.onValueChanged.AddListener(delegate {useGyroSitting(); });
		SmoothMovementToggle.onValueChanged.AddListener(delegate {useSmoothMovementSitting(); });
		StartButton.onClick.AddListener(delegate {OnStartButtonClick(); });
	}
	
	// Update is called once per frame
	void Update () {
		sensitiveText.text =sensitiveSlider.value.ToString();
		UpDownDelayText.text = upDownDelaySlider.value.ToString ();
		forwardDelayText.text = forwardDelaySlider.value.ToString ();
		capText.text = capSlider.value.ToString ();
		gyroSensitiveText.text = gyroSensitiveSlider.value.ToString ();
	}

	public void SensitiveSitting()
	{
		sds.sensitiveValue=sensitiveSlider.value;
	}
	public void upDownDelaySitting()
	{
		sds.upDownDelayValue=upDownDelaySlider.value;
	}
	public void ForwardDelaySitting()
	{
		sds.forwardDelayValue=forwardDelaySlider.value;
	}
	public void CapSitting()
	{
		sds.capValue=capSlider.value;
	}
	public void GyroSensitiveSitting()
	{
		sds.gyroSensitiveValue=gyroSensitiveSlider.value;
	}
	public void LookAroundSitting()
	{
		sds.LookAroundTrigger = LooKAroundToggle.isOn;
	}
	public void useGyroSitting()
	{
		sds.useGyro = useGyroToggle.isOn;
	}
	public void useSmoothMovementSitting()
	{
		sds.SmoothMovement = SmoothMovementToggle.isOn;
	}
	public void OnStartButtonClick()
	{
		sds.keepValueTrigger = true;
		Application.LoadLevel("test");
	}

	void SpawnSettingObject()
	{		
		settingDataOj = (GameObject)Instantiate(settingDataOjFab);
		settingDataOj.name = "SettingData";
	}
}
