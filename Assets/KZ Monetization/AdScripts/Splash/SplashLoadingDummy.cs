using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoadingDummy : SplashBase
{
    public GameObject LoadingPanel;
    public Image LoadingSlider;
    public override void OnAccept()
    {
        LoadingPanel.gameObject.SetActive(true);
    }

    public override void OnLoadingLevel(float value)
    {
        LoadingSlider.fillAmount = value;
    }
}
