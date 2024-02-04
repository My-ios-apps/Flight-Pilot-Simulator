using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrivacyPanel_CB : MonoBehaviour {
    //public Sprite yes, no;
    //public Image AdsButton;
    public GameObject PPPanel;
    bool yesFlag = false;
    public UnityEvent OnAcceptPolicy;
    // Use this for initialization
    void Awake () {
        // ShowPolicy();
        AcceptOurPrivacyPolicy();

    }
    public void ShowPolicy()
    {
        //AdsButton.sprite = yes;
        yesFlag = true;
//        AdsInitilizer.PersonlizedAds = yesFlag;
        if (PlayerPrefs.GetInt("PrivacyPolicy", 0) == 1)
        {
            OnAcceptPolicy.Invoke();
            PPPanel.SetActive(false);
        }
        else
        {
            
            PPPanel.SetActive(true);
        }
    }
    public void VisitWebsite()
    {
        Application.OpenURL(AdsManager.Instance.PrivacyPolicy);
    }
    //public void Ads()
    //{
    //    if (yesFlag)
    //    {
    //        yesFlag = false;
    //        AdsButton.sprite = no;
    //        AdsInitilizer.PersonlizedAds = yesFlag;
    //    }
    //    else
    //    {
    //        AdsButton.sprite = yes;
    //        yesFlag = true;
    //        AdsInitilizer.PersonlizedAds = yesFlag;
    //    }
    //}
    public void AcceptOurPrivacyPolicy()
    {
        OnAcceptPolicy.Invoke();
        PlayerPrefs.SetInt("PrivacyPolicy", 1);
        PPPanel.SetActive(false);
    }
}
