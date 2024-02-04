using UnityEngine;
using System.Collections;

public class RateUsScript : MonoBehaviour {
	public GameObject rateDialogue;
	public bool isMainMenu;
	int count=0;
	public delegate void rate ();
	public static rate ShowRateDailogue;
	void OnEnable(){

		ShowRateDailogue += showDialogue ;
	}
	void OnDisable(){
		ShowRateDailogue -= showDialogue ;

	}
	void Awake(){
		HideDialogue ();
		if (isMainMenu) {
			count = PlayerPrefs.GetInt ("RateCount", 0);
			count++;
			if (count%3==0){
				StartCoroutine (showrateDialogue (1f));
			}
			PlayerPrefs.SetInt ("RateCount", count);
		}

	}

	public void RateNow(){
		SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
		PlayerPrefs.SetInt ("RateStatus", 1);
		Application.OpenURL ("https://play.google.com/store/apps/details?id="+Application.identifier);
		HideDialogue ();
	}

	public void RateLater(){
		HideDialogue ();
		SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
	}

	public void RateNever(){

		PlayerPrefs.SetInt ("RateStatus", 1);
		SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
		HideDialogue ();
	}


	IEnumerator showrateDialogue( float delay ){
		yield return new WaitForSeconds (delay);
		showDialogue ();
	}

	void showDialogue(){
		int i =PlayerPrefs.GetInt ("RateStatus", 0);
		if (i == 0) {
			rateDialogue.SetActive (true);
		}
	}
	void HideDialogue(){
		rateDialogue.SetActive (false);
	}
}
