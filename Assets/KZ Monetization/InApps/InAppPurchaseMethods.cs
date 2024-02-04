using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InAppPurchaseMethods
{
    public static void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        PlayerPrefs.Save();
        AdsManager.Instance.HideAllBanners();
    }

    public static void UnlockAll()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        PlayerPrefs.SetInt("UnlockAll", 1);
        PlayerPrefs.Save();
        AdsManager.Instance.HideAllBanners();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
    public static void AddCoins1000()
    {
        Garage.instance.Reward(1000);

    }
    public static void Add_Coins2500()
    {
        Garage.instance.Reward(2500);
    }
    public static void Add_Coins5000()
    {
        Garage.instance.Reward(5000);
    }
    public static void Add_Coins10000()
    {
        Garage.instance.Reward(10000);

    }

}

