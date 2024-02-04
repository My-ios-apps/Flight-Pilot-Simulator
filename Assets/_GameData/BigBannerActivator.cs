using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBannerActivator : MonoBehaviour
{
    public BannerStatus Enable = BannerStatus.Show;
    public BannerStatus Disable = BannerStatus.Hide;
    public AdPosition NewBannerPosition = AdPosition.Top;

    private void OnEnable()
    {
        if(Enable== BannerStatus.Show)
        { 
            AdsManager.Instance.RepositionMREC(NewBannerPosition);
            AdsManager.Instance.ShowMREC();
        }
        else
        {
            AdsManager.Instance.HideMREC();
        }
    }

    private void OnDisable()
    {
        if (Disable != BannerStatus.Hide)
        {
            AdsManager.Instance.RepositionMREC(NewBannerPosition);

        }
        else
        {
            AdsManager.Instance.HideMREC();

        }
    }
    public enum BannerStatus
    {
        Hide,Show
    }
}
