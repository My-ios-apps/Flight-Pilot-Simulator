using System;
using System.Collections;
using System.Collections.Generic;
//using ToastPlugin;
using UnityEngine;
using UnityEngine.UI;


public class DestroyableRewardedAdCaller_CB : MonoBehaviour {
    public bool showPopup = false;

    //[Tooltip("Confrimation popup will show to ask user, if he really want to want video. If you want to show popup then drag drop confirmation popup and check -Show Popup- to True.")]
    [DrawIf("showPopup", true)]
    public GameObject confirmationPopup;
    
    public RewardedVideoType rewardedVideoType;

    [DrawIf("rewardedVideoType", RewardedVideoType.VideoCountUnlock)]
    public int unLockOnVideoCount;
    [DrawIf("rewardedVideoType", RewardedVideoType.VideoCountUnlock)]
    public Text textToShowCount;

    //private void OnValidate()
    //{
    //    if (showPopup && confirmationPopup == null)
    //    {
    //        if (FindObjectOfType<RewardedVideoConfirmationPopup_CB>() == null)
    //        {
    //            Debug.LogError("Please Drag prefab ==> -Watch Video Confirmation popup- from Prefab folder in AdsScript.");
    //        }
    //        else
    //        {
    //            confirmationPopup = FindObjectOfType<RewardedVideoConfirmationPopup_CB>().gameObject;
    //            if (confirmationPopup == null)
    //            {
    //                Debug.LogError("Please Drag prefab ==> -Watch Video Confirmation popup- from Prefab folder in AdsScript.");
    //            }
    //        }
            
    //    }
    //}

    // Use this for initialization
    void OnEnable () {
        //try
        //{
            if (PlayerPrefs.GetInt("UnlockAll",0) == 0)
            {
                if (PlayerPrefs.GetInt(gameObject.name,0) == 0)
                {
                    if (transform.parent.GetComponent<BoxCollider2D>())
                    {
                        transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else if (transform.parent.GetComponent<BoxCollider>())
                    {
                        transform.parent.GetComponent<BoxCollider>().enabled = false;
                    }
                    else if (transform.parent.GetComponent<Button>())
                    {
                        transform.parent.GetComponent<Button>().enabled = false;
                    }

                    int count = PlayerPrefs.GetInt(gameObject.name, 0);
                    textToShowCount.text = "" + count + "/" + unLockOnVideoCount;
                }
                else
                {
                    if (rewardedVideoType == RewardedVideoType.VideoCountUnlock)
                    {
                        int count = PlayerPrefs.GetInt(gameObject.name, 0);
                        textToShowCount.text = "" + count + "/" + unLockOnVideoCount;

                        if (count >= unLockOnVideoCount)
                        {
                            VideoWatches();
                        }
                        else
                        {
                            if (transform.parent.GetComponent<BoxCollider2D>())
                            {
                                transform.parent.GetComponent<BoxCollider2D>().enabled = false;
                            }
                            else if (transform.parent.GetComponent<BoxCollider>())
                            {
                                transform.parent.GetComponent<BoxCollider>().enabled = false;
                            }
                            else if (transform.parent.GetComponent<Button>())
                            {
                                transform.parent.GetComponent<Button>().enabled = false;
                            }
                        }
                    }
                    else
                    {
                        VideoWatches();
                    }
                }
            }
            else
            {
                VideoWatches();
            }
          
        //}
        //catch (Exception ex)
        //{
        //    GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        //}
       
	}

	public void CallRewardedVideo(){
        AdsManager.Instance.ShowRewarded(() =>
        {
            // OnRewardComplete?.Invoke();
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
        //    }
        //}
        //catch (Exception ex)
        //{
        //    GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        //}


    }

	void VideoWatches () {
        //try
        //{
            //ToastHelper.ShowToast("Rewarded Ad Showed Give Reward", false);
            print("Rewarded Ad Showed Give Reward");
            if (rewardedVideoType == RewardedVideoType.PremanentUnlock)
            {
                #region Unlock Item
                PlayerPrefs.SetInt(gameObject.name, 1);
                if (transform.parent.GetComponent<BoxCollider2D>() != null)
                {
                    transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                }
                else if (transform.parent.GetComponent<BoxCollider>() != null)
                {
                    transform.parent.GetComponent<BoxCollider>().enabled = true;
                }
                else if (transform.parent.GetComponent<Button>() != null)
                {
                    transform.parent.GetComponent<Button>().enabled = true;
                }
                Destroy(gameObject);
                #endregion
            }
            else if (rewardedVideoType == RewardedVideoType.VideoCountUnlock)
            {
                #region Unlock Item
                int count = PlayerPrefs.GetInt(gameObject.name, 0);
                PlayerPrefs.SetInt(gameObject.name, count + 1);
                count = PlayerPrefs.GetInt(gameObject.name, 0);
                if (count >= unLockOnVideoCount || PlayerPrefs.GetInt("UnlockAll", 0) == 0)
                {
                    if (transform.parent.GetComponent<BoxCollider2D>() != null)
                    {
                        transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    else if (transform.parent.GetComponent<BoxCollider>() != null)
                    {
                        transform.parent.GetComponent<BoxCollider>().enabled = true;
                    }
                    else if (transform.parent.GetComponent<Button>() != null)
                    {
                        transform.parent.GetComponent<Button>().enabled = true;
                    }
                    Destroy(gameObject);
                }
                else
                {
                    textToShowCount.text = "" + count + "/" + unLockOnVideoCount;
                }
                #endregion
            }
            else if (rewardedVideoType == RewardedVideoType.TempUnlock)
            {
                #region Unlock Item
                if (transform.parent.GetComponent<BoxCollider2D>() != null)
                {
                    transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                }
                else if (transform.parent.GetComponent<BoxCollider>() != null)
                {
                    transform.parent.GetComponent<BoxCollider>().enabled = true;
                }
                else if (transform.parent.GetComponent<Button>() != null)
                {
                    transform.parent.GetComponent<Button>().enabled = true;
                }
                Destroy(gameObject);
                #endregion
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
        //try
        //{
        //    ToastHelper.ShowToast("Rewarded Video Ad Failed", true);
        //}
        //catch (Exception ex)
        //{
        //    GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        //}
        
    }

    


}

public enum RewardedVideoType
{
    TempUnlock,
    PremanentUnlock,
    VideoCountUnlock,
}
