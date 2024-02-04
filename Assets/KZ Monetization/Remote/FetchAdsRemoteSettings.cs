using Newtonsoft.Json;
using System;
using System.Collections;
using Unity.RemoteConfig;
using UnityEngine;

public class FetchAdsRemoteSettings : MonoBehaviour
{
    struct userAttributes { }
    struct appAttributes { }
    bool hasInitialized = false;

    public void Initialize()
    {
        hasInitialized = true;
        ConfigManager.FetchCompleted += OnFetchCompleted;
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    private void OnFetchCompleted(ConfigResponse response)
    {
        if (response.status == ConfigRequestStatus.Success)
        {
            string json = ConfigManager.appConfig.GetJson("AdsSettings");
            ParseData(json);
        }
    }

    void ParseData(string data)
    {
        if (string.IsNullOrEmpty(data)) return;

        if (data.Length > 2)
            AdsRemoteSettings.Instance.Save(data);
    }

    private void OnDestroy()
    {
        if (hasInitialized)
            ConfigManager.FetchCompleted -= OnFetchCompleted;
    }
}
