using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PrefData
{
    static string Coins = "Cash";
    static string UnlockLevels = "ForestLevelUnlocked";
    static string CurrentLevel = "CurrentLevel";
    static int LevelIndex;
    #region ________Coins_____________
    public static int GetCoinsAmount()
    {
        return PlayerPrefs.GetInt(Coins);
    }
    public static int SetCoinsAmount(int Amount, bool IncreaseCoins)
    {
        if (IncreaseCoins)
        {
            PlayerPrefs.SetInt(Coins, PlayerPrefs.GetInt(Coins) + Amount);
        }
        else
        {
            PlayerPrefs.SetInt(Coins, Amount);
        }
        return PlayerPrefs.GetInt(Coins);
    }
    #endregion
    #region ________CurrentLevels_____________
    public static int GetCurrentLevel()
    {
        LevelIndex = PlayerPrefs.GetInt(CurrentLevel);
        return LevelIndex;
    }
    public static int SetCurrentLevel(int index, bool IncreaseLevelIndex)
    {
        if (IncreaseLevelIndex)
        {
            PlayerPrefs.SetInt(CurrentLevel, PlayerPrefs.GetInt(CurrentLevel) + index);
        }
        else
        {
            PlayerPrefs.SetInt(CurrentLevel, index);
        }
        return PlayerPrefs.GetInt(CurrentLevel);
    }
    #endregion
    #region ________UnlockLevels_____________
    public static int GetUnlockLevel()
    {
        return PlayerPrefs.GetInt(UnlockLevels);
    }
    public static int SetUnlockLevel(int index, bool IncreaseLevelIndex)
    {

        if (IncreaseLevelIndex)
        {
            PlayerPrefs.SetInt(UnlockLevels, PlayerPrefs.GetInt(UnlockLevels) + index);
        }
        else
        {
            PlayerPrefs.SetInt(UnlockLevels, index);
        }
        return PlayerPrefs.GetInt(UnlockLevels);
    }
    #endregion
}
public class levelSelectionScript : MonoBehaviour
{
    public Button[] AllLevels;
    public Image[] Lock;
    public Image[] Glow;
    public GameObject Modes;
    public GameObject LevelSel;
    public GameObject Loading;
    public Image fillBar;
    public Text cash;
    int totalUnlockedLevel = 0;
    public static int CurrentLevelIndex;
    bool SelectLevel;

    int totalLevels = 6;
    void Start()
    {
        //if (PlayerPrefs.GetInt("UnlockAll", 0) == 1)

        //    for (int i = 0; i < AllLevels.Length; i++)
        //    {
        //        //if (i = totalUnlockedLevel)
        //        {
        //            AllLevels[i].interactable = true;
        //            Lock[i].gameObject.SetActive(false);
        //        }
        //      //  else
        //        {
        //          //  AllLevels[i].interactable = false;
        //          //  Lock[i].gameObject.SetActive(true);
        //        }
        //    }


        // totalUnlockedLevel = 6;
        AdsManager.Instance.ShowBanner();
        //PrefData.SetCoinsAmount(50000, false);
        cash.text = PrefData.GetCoinsAmount().ToString();
        totalUnlockedLevel = PrefData.GetUnlockLevel();
        Debug.Log(totalUnlockedLevel);
        InactiveAllButtons();
        if (totalUnlockedLevel >= totalLevels)
            totalUnlockedLevel = totalLevels;
        if (PlayerPrefs.GetInt("UnlockAll", 0) == 1)
        {
            for (int i = 0; i < AllLevels.Length; i++)
            {
              //  if (i <= totalUnlockedLevel)
                {
                    AllLevels[i].interactable = true;
                    Lock[i].gameObject.SetActive(false);
                }
              //  else
                {
                   // AllLevels[i].interactable = false;
                   // Lock[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < AllLevels.Length; i++)
            {
                if (i <= totalUnlockedLevel)
                {
                    AllLevels[i].interactable = true;
                    Lock[i].gameObject.SetActive(false);
                }
                else
                {
                    AllLevels[i].interactable = false;
                    Lock[i].gameObject.SetActive(true);
                }
            }
        }
        //for (int i = 0; i < AllLevels.Length; i++)
        //    {
        //        if (i <= totalUnlockedLevel)
        //        {
        //            AllLevels[i].interactable = true;
        //            Lock[i].gameObject.SetActive(false);
        //        }
        //        else
        //        {
        //            AllLevels[i].interactable = false;
        //            Lock[i].gameObject.SetActive(true);
        //        }
        //    }
        
        Glow[totalUnlockedLevel].gameObject.SetActive(true);
        Modes.SetActive(true);
        LevelSel.SetActive(false);
        SoundManager.PlaySound(SoundManager.NameOfSounds.MainMenu);
        GameConstant.sceneToLoad = "GamePlayForest";
        //forestLevelClicked(PrefData.GetUnlockLevel());

        //GameConstant.sceneToLoad = "GamePlayForest";
    }
    public void CarrerMode()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        GameConstant.isCareerMode = true;
        Modes.SetActive(false);
        LevelSel.SetActive(true);
    }

    public void RaceMode()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        GameConstant.isCareerMode = false;
        forestLevelClicked(4);
        //city.SetActive (true);
    }
    public void RescueMode()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        GameConstant.isCareerMode = false;
        forestLevelClicked(5);
        //city.SetActive (true);
    }
    public void forestMode()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Modes.SetActive(false);
        LevelSel.SetActive(true);

    }

    public void cityMode()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Modes.SetActive(false);
        LevelSel.SetActive(false);
        //city.SetActive(true);
    }

    public void HomePage()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        if (Modes.activeSelf)
        {
            Loading.SetActive(true);
            StartCoroutine(LoadLevel(0.1f, "MainMenu"));
        }
        else
        {
            Modes.SetActive(true);
            LevelSel.SetActive(false);
        }
    }
    public void modeSelectmenu()
    {
        //Debug.Log ("Escape button pressed and game is quit ");
        //SceneManager.LoadScene ("MainMenu");
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        //city.SetActive(false);
        LevelSel.SetActive(false);
        Modes.SetActive(true);
    }
    void InactiveAllButtons()
    {
        for (int i = 0; i < AllLevels.Length; i++)
        {
            AllLevels[i].interactable = false;
            Lock[i].gameObject.SetActive(false);
            Glow[i].gameObject.SetActive(false);
        }




        //forestLevel.SetActive(false);
        //city.SetActive(false);

    }

    IEnumerator LoadLevel(float delay, string name1)
    {
        yield return new WaitForSeconds(delay);
        AdsManager.Instance.ShowInterstitial("");
        yield return new WaitForSeconds(delay * 2);
        GameConstant.isTruckButtonClicked = false;
        SceneManager.LoadScene(name1);
    }
    public void ForestLevelClicked()
    {
        //SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
        LevelSel.SetActive(true);

    }


    public void BackToModeSelection()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        LevelSel.SetActive(false);
    }
    int Levelindex;
    public void forestLevelClicked(int index)
    {
        CurrentLevelIndex = index;
        SelectLevel = true;
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        GameConstant.currentLevel = index;
        GameConstant.isCityMode = false;
        GameConstant.sceneToLoad = "GamePlayForest";
        Loading.SetActive(true);
        StartCoroutine(LoadLevel(0.1f));
        // SceneManager.LoadScene("TruckSelection");

    }
    IEnumerator LoadLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        AdsManager.Instance.ShowInterstitial("");
        yield return new WaitForSeconds(delay * 2);
        StartCoroutine(LoadSceneAsync("PlaneSelection"));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            fillBar.fillAmount = async.progress;
            yield return null;
        }
    }

    /*
      public void Home(){
          //SoundManager.PlaySound (SoundManager.NameOfSounds.Button);
          //SceneManager.LoadScene("MainMenu");

      }
  */
    public void playuttonlicked()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Loading.SetActive(true);
        StartCoroutine(LoadLevel(0.1f, "PlaneSelection"));

    }

    IEnumerator showAd(float delay)
    {
        yield return new WaitForSeconds(delay);

    }

    //void purchaseSuccessful(IAPResult result, string ID){
    //	if (result.Equals (IAPResult.success)) {
    //		switch (ID) {
    //		case "unlockalllevelsofcity":
    //			unlockallCityLevel ();
    //			break;

    //		case "unlockalllevelsofisland":
    //			unlockAllIslandLevels ();
    //			break;


    //		}
    //	}
    //}

    public void unlockallCityLevel()
    {
        //PlayerPrefs.SetInt("CityLevelUnlocked", cityLevels.Length);
        //totalUnlockedLevel = PlayerPrefs.GetInt("CityLevelUnlocked", 1);
        //for (int i = 0; (i < totalUnlockedLevel && i < cityLevels.Length); i++)
        //{
        //    cityLevels[i].SetActive(false);
        //}
        //unlockCityLevelbtn.SetActive(false);
    }

    public void unlockAllIslandLevels()
    {
        //PlayerPrefs.SetInt("ForestLevelUnlocked", forestLevels.Length);
        //totalUnlockedLevel = PlayerPrefs.GetInt("ForestLevelUnlocked", 1);
        //for (int i = 0; (i < totalUnlockedLevel && i < forestLevels.Length); i++)
        //{
        //    forestLevels[i].SetActive(false);
        //}
        //unlockIslandLevelbtn.SetActive(false);

    }
    public void unlockCityLevels()
    {

    }
    public void unlockIslandLevels()
    {
        //AdsSDKManager obj = GameObject.FindObjectOfType<AdsSDKManager> ();
        //if (obj != null) {
        //	obj.BuyInApp (GameConstant.unlockalllevelsofisland);
        //}
    }

}
