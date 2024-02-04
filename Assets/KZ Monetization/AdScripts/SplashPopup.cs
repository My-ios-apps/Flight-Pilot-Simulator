using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SplashPopup : MonoBehaviour
{
    [SerializeField] GameObject PolicyPanel;
    [SerializeField] SplashBase m_SplashBase;
    [SerializeField] int LevelToLoadIndex;
    [SerializeField] float LoadingDuration = 7f;


    void Start()
    {
        if (AdConstants.PolicyAccepted)
            OnPolicyAccepted();
        else
            PolicyPanel.gameObject.SetActive(true);
    }

    public void Accept()
    {
        AdConstants.AcceptPolicy();
        OnPolicyAccepted();
    }

    public void VisitWebsite()
    {
        AdsManager.Instance.VisitWebsite();
    }

    void OnPolicyAccepted()
    {
        m_SplashBase.OnAccept();
        StartCoroutine(LoadNextLevel());

        PolicyPanel.gameObject.SetActive(false);
        AdsManager.Instance.Initialize_Consent();
    }

    IEnumerator LoadNextLevel()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(LevelToLoadIndex);
        async.allowSceneActivation = false;

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / LoadingDuration;
            m_SplashBase.OnLoadingLevel(timer);
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
