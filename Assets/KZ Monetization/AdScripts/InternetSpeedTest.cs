using KZ.Utils;
using System;
using System.Collections;
using UnityEngine;

public class InternetSpeedTest : MonoBehaviour
{
    public float Timeout = 1.5f;
    const string pingIP = "8.8.8.8";
    Ping m_Ping;

    public void CheckPings(Action<bool> result)
    {
        StopAllCoroutines();
        DestroyPing();
        StartCoroutine(PingRoutine(result));
    }

    void DestroyPing()
    {
        if (m_Ping != null)
            m_Ping.DestroyPing();
    }

    IEnumerator PingRoutine(Action<bool> result)
    {
        yield return new WaitForEndOfFrame();
        m_Ping = new Ping(pingIP);
        float timer = 0;
        while (!m_Ping.isDone)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > Timeout)
            {
                MobileToast.Show("Poor Internet Connections!", true);
                yield break;
            }

            yield return null;
        }

        //MobileToast.Show($"Ping {m_Ping.time}ms");

        //if (m_Ping.time > AdsRemoteSettings.Instance.AdmobPings)
        //    yield break;

        //if (m_Ping.time > 0 && m_Ping.time < AdsRemoteSettings.Instance.MediationPings)
        //    result?.Invoke(true);
        //else
        //    result?.Invoke(false);
    }
}
