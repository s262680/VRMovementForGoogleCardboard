using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour {

	public Text timeText;
	public Text scoreText;
	public Text time2Text;
	public Text testText;
	float timeValue=0;
	float timeValue2=0;
	GameObject[] moveOj;
	GameObject eventSys;
	GameObject playerOj;
	movementTest mt;
	public int stage;
	// Use this for initialization
	void Start () {
		stage = 1;
		eventSys = GameObject.Find ("GvrEventSystem");
		InvokeRepeating ("numberUpdate",0.0f, 0.5f);
		InvokeRepeating ("resetEventSystem",0.0f, 1.0f);

		playerOj = GameObject.Find ("Player");
		mt = playerOj.GetComponent<movementTest> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (stage == 1) 
		{
			timeValue += Time.deltaTime;
		}
		else if (moveOj.Length > 0) 
		{
			timeValue2 += Time.deltaTime;
		}

		timeText.text = "Stage1 Time: " + Mathf.RoundToInt(timeValue).ToString();
		if (stage == 2)
		{
			time2Text.text = "Stage2 Time: " + Mathf.RoundToInt (timeValue2).ToString ();
			scoreText.text = "Remaining balls: " + moveOj.Length.ToString ();
		}

		testText.text = "Speed" + mt.upDownSpeed.ToString ();
	}

	void numberUpdate()
	{
		moveOj = GameObject.FindGameObjectsWithTag ("moveOj");
	}

	void resetEventSystem()
	{
		eventSys.SetActive (false);
		eventSys.SetActive (true);	
	}
}
