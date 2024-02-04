using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour {


	public GameObject[] jet;
	int currentJet=0;


	public void PressButton(int indexs)
	{
		currentJet = indexs;
		for (int i = 0; i < jet.Length; i++) {
			if (i == indexs - 1) {
				jet [i].SetActive (true);
			} else {
				jet [i].SetActive (false);
			}
		}
	}

	public void Next(){
		currentJet++;
		if (currentJet > jet.Length) {
			currentJet = 1;
		}
		PressButton (currentJet);
	}
	public void Previous(){
		currentJet--;
		if (currentJet <= 0) {
			currentJet = jet.Length;
		}
		PressButton(currentJet);
	}


}
