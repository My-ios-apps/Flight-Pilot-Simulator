using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public GameObject loading;

	void Start(){
		loading.SetActive (false);
	}
	public void LoadLevel(string name)
	{
		loading.SetActive (true);
		StartCoroutine(LoadLevel(0.1f,name));
	}
	IEnumerator LoadLevel(float delay, string sc)
	{
		yield return new WaitForSeconds(delay);
		AdsManager.Instance.ShowInterstitial("");
		yield return new WaitForSeconds(delay * 2);
		SceneManager.LoadScene(sc);
	}
}
