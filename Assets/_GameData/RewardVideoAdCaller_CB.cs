
using System;
using System.Collections;
using System.Collections.Generic;
//using ToastPlugin;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardVideoAdCaller_CB : MonoBehaviour
{

    //public static bool isRewardAd = false;
    public bool showPopup;

    [DrawIf("showPopup", true)]
    public GameObject confirmationPopup;
    
    //[Tooltip("To Recognize Reward")]
    public int RewardInteger;
    public UnityEvent OnWatchVideoSuccess, OnWatchVideoFailed;
    //private void OnValidate()
    //{
    //    if (showPopup && confirmationPopup == null)
    //    {
    //        confirmationPopup = FindObjectOfType<RewardedVideoConfirmationPopup_CB>().gameObject;
    //        if (confirmationPopup == null)
    //        {
    //            Debug.LogError("Please Drag prefab ==> -Watch Video Confirmation popup- from Prefab folder in AdsScript.");
    //        }
    //    }
    //}

    public void CallRewardedVideo()
    {
        AdsManager.Instance.ShowRewarded(() =>
        {
            //OnRewardComplete?.Invoke();
            VideoWatches();

        }, "");
        //try
        //{
        //    if (Application.internetReachability != NetworkReachability.NotReachable)
        //    {
        //        WatchRewardedVideoAd_CB.callBackObject = gameObject;
        //        if (showPopup)
        //        {
        //            confirmationPopup.SetActive(true);
        //        }
        //        else
        //        {

        //            WatchRewardedVideoAd_CB.instance.CallRewardedAd();
        //        }

        //    }
        //    else
        //    {
        //        ToastHelper.ShowToast("No, Network Available", true);
        //        RewardedVideoFailed();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        //}


    }
    public void CallRewardedVideoLovin()
    {
        CallRewardedVideo();
    }
    void VideoWatches()
    {
        //try
        //{
            OnWatchVideoSuccess.Invoke();
            switch (RewardInteger)
            {
                case 0:
                    Debug.Log("Set rewarded here for index 0");
                    break;

            }

            if (showPopup)
            {
                confirmationPopup.SetActive(false);
            }
        //}
        //catch (Exception ex)
        //{
        //    GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        //}
    }

    public void RewardedVideoFailed()
    {
        //Debug.Log("Failure With int : " + RewardInteger);
        OnWatchVideoFailed.Invoke();
    }
}

