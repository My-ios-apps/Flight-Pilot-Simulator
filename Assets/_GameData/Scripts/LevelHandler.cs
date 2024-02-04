using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {

	public GameObject allCheckPoints;
	public int currentPoint = 0;
	public bool ismovingBaloon;
	public int baloonCount=2;

	// events 

	public delegate void CheckPointCollected(int total, int pickedUp);
	public static event CheckPointCollected OnCheckPointCollected;


	public delegate void TaskComplete();
	public static event TaskComplete OnTaskComplete;

	void OnEnable(){
		//CheckPoint.OnCheckPointEnter += UpdateCheckPoint;
	}

	void OnDisable(){
		//CheckPoint.OnCheckPointEnter -= UpdateCheckPoint/*;*/
	}
	// Use this for initialization
	void Start () {
		currentPoint = 0;
		for (int i = 0; i < allCheckPoints.transform.childCount; i++) {

			allCheckPoints.transform.GetChild (i).gameObject.SetActive (true);
		}
		allCheckPoints.transform.GetChild (currentPoint).gameObject.SetActive (true);
		if (OnCheckPointCollected != null) {
			OnCheckPointCollected (allCheckPoints.transform.childCount, currentPoint);
		}
	}

	void UpdateCheckPoint(){
		currentPoint++;
		if (OnCheckPointCollected != null) {
			OnCheckPointCollected (allCheckPoints.transform.childCount, currentPoint);
		}
		if (ismovingBaloon && currentPoint >= baloonCount) {
			if (OnTaskComplete != null) {
				OnTaskComplete ();
			}
		}


	}
}
