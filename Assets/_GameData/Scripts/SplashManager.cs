using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    public GameObject Loading, PP;
    
    public GameObject privacyBtn, YesBtn, NoBtn;
    void Start()
    {
        if (PlayerPrefs.GetInt("Privacy") == 1)
        {
            Loading.SetActive(true);
            PP.SetActive(false);

            Application.LoadLevel(Application.loadedLevel+1);
        }
        else
        {
            Loading.SetActive(false);
            PP.SetActive(true);
        }

    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://brain-games-0.flycricket.io/privacy.html");
    }
    public void Yes()
    {
        PlayerPrefs.SetInt("Privacy", 1);
        Loading.SetActive(true);
        PP.SetActive(false);

        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
