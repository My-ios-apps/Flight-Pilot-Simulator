using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane{

public class Speed : MonoBehaviour {
		public GameObject flames;
		//public GameObject smoke;

	private AeroplaneController speedController;
	void Start(){
			speedController = GetComponent<AeroplaneController> ();	
			//smoke.SetActive (false);
			flames.SetActive (false);
	}
	void Update () {
			//Debug.Log (speedController.newVelocity.z);
			if (speedController.newVelocity.z > 10) {
				flames.SetActive (true);
			//	smoke.SetActive (true);	
			} else {
				flames.SetActive (false);
			//	smoke.SetActive (false);
			}
	}
	}
}
