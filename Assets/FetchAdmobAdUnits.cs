using System.Collections;
using Unity.RemoteConfig;
using UnityEngine;

[System.Serializable]
public class FetchAdmobAdUnits : MonoBehaviour
{
    AdmobAdUnits RemoteAdUnits;
    const string AdUnitsKey = "AdmobSettings";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Load();
        yield return new WaitForSeconds(.2f);
        SetIDs();
    }

    private void OnEnable()
    {
        ConfigManager.FetchCompleted += OnFetchCompleted;
    }

    private void OnDisable()
    {
        ConfigManager.FetchCompleted -= OnFetchCompleted;
    }

    private void OnFetchCompleted(ConfigResponse response)
    {
        if (response.status == ConfigRequestStatus.Success)
        {
            string json = ConfigManager.appConfig.GetJson(AdUnitsKey);
            if (string.IsNullOrEmpty(json)) return;

            if (json.Length > 2)
            {
                Save(JsonUtility.FromJson<AdmobAdUnits>(json));

                //Debug.LogError($"{AdUnitsKey} Saved\n{PlayerPrefs.GetString(AdUnitsKey)}");
                SetIDs();
            }
        }
    }

    public void SetIDs()
    {
        AdsManager.Instance.MrecIDs[0] = RemoteAdUnits.MrecID;
        AdsManager.Instance.AppOpenIDs[0] = RemoteAdUnits.AppOpenID;
        AdsManager.Instance.BannerIDs[0] = RemoteAdUnits.BannerID;
        AdsManager.Instance.RewardedIDs[0] = RemoteAdUnits.RewardedID;
        AdsManager.Instance.InterstitialIDs[0] = RemoteAdUnits.InterstitialID;
    }


    #region Save N Load

    public void Load()
    {
        if (PlayerPrefs.HasKey(AdUnitsKey))
        {
            RemoteAdUnits = JsonUtility.FromJson<AdmobAdUnits>(PlayerPrefs.GetString(AdUnitsKey));
            //Debug.LogError($"{AdUnitsKey} Loaded Prefs\n{PlayerPrefs.GetString(AdUnitsKey)}");
        }
        else
        {
            Save(new AdmobAdUnits(true));
            //Debug.LogError($"{AdUnitsKey} Loaded New\n{PlayerPrefs.GetString(AdUnitsKey)}");
        }
    }

    public void Save(AdmobAdUnits value)
    {
        RemoteAdUnits = value;
        PlayerPrefs.SetString(AdUnitsKey, JsonUtility.ToJson(RemoteAdUnits));
        PlayerPrefs.Save();
    }

    #endregion


    [System.Serializable]
    public struct AdmobAdUnits
    {
        public string BannerID;
        public string MrecID;
        public string InterstitialID;
        public string RewardedID;
        public string AppOpenID;

        public AdmobAdUnits(bool value)
        {
            BannerID = "ca-app-pub-7807619507551678/2769360223";
            MrecID = "ca-app-pub-7807619507551678/9127773060";
            InterstitialID = "ca-app-pub-7807619507551678/6974051319";
            RewardedID = "ca-app-pub-7807619507551678/7300520224";
            AppOpenID = "ca-app-pub-7807619507551678/9351968490";
        }
    }
}
