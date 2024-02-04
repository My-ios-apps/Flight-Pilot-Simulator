using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	public delegate void checkPoint();
	public static event checkPoint OnCheckPointEnter;
	public GameObject particlePrefab;
	public bool isParent;

	bool PickedUp;	
	void OnTriggerEnter( Collider other){
		
		if (other.gameObject.tag.Equals ("Player") && !PickedUp) {
				PickedUp = true;
			//Debug.Log (GameConstant.count);
				if (OnCheckPointEnter != null) {
					GameObject partcle =	Instantiate (particlePrefab, transform.position, Quaternion.identity);
					GameConstant.count+=1;
					
					Debug.Log (GameConstant.total);
				
					SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
					OnCheckPointEnter ();
					if (isParent) {
						gameObject.SetActive (false);
					} else {
						transform.parent.gameObject.SetActive (false);
					}

				}
		}
	}
}
