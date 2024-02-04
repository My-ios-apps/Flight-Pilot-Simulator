using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public GameObject PP, Loading;
    public Image loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        //AdsManager.Instance.HideBanner();
        QualitySettings.SetQualityLevel(2);
        //        Garage.instance.Reward(500000);
     //   PlayerPrefs.SetInt("RemoveAds", 1);
      //  PlayerPrefs.SetInt("UnlockAll", 1);

        PlayerPrefs.SetInt("PP", 1);

        if (PlayerPrefs.GetInt("PP",0) == 0)
        {
            PP.SetActive(true);
            Loading.SetActive(false);
        }
        else
        {
            PP.SetActive(false);
            Loading.SetActive(true);
            StartCoroutine(LoadScene(6f));
            //Application.LoadLevel(Application.loadedLevel+1);
        }
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        Start();
    }
    public void Accept()
    {
        Loading.SetActive(true);
        PlayerPrefs.SetInt("PP", 1);
        StartCoroutine(LoadScene(6f));
        
    }

    public void OpenPP()
    {
        Application.OpenURL(AdsManager.Instance.PrivacyPolicy);
    }

    IEnumerator LoadScene(float duration)
    {
        float timer = 0.0f;
        float speed;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            speed = timer / duration;
            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, speed);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
