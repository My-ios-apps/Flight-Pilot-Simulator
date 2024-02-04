using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
public struct PlaneSpecification
{
    [Range(0, 1)]
    public float Speed;
    [Range(0, 1)]
    public float Handling;
    [Range(0, 1)]
    public float Brakes;
}
public class Garage : MonoBehaviour
{
    public static Garage instance;
    public GameObject loading;
    public Image fillBar;
    public PlaneSpecification[] planeSpecifications;
    public GameObject[] Planes;
    [Header(" **************** All Planes **************** ")]
    public static int PlaneIndex = 0;

    [Header(" *********** Int ***********")]
    public int preSelectedPlane;
    [Header(" *********** UI ***********")]
    //public GameObject Canvas;
    //public GameObject[] SelectedPlane;
    //public Image[] BarPlaneSelect;
    ////public Sprite Selected;
    ////public Sprite UnSelected;
    //public Slider Speed;
    //public Slider Handling;
    //public Slider Brakes;
    public GameObject PlaneSelectionSlider;
    public GameObject RequiredPlanePanel;

    [Header("Buttons")]
    [SerializeField] GameObject next;
    [SerializeField] GameObject prev;
    //public GameObject TryFreeRideBtn;
    [Header("Purchasing")]
    //  public Image locked;
    public Button play;
    public Button buyButton;
    public Text priceText;

    [Header("MoneyManager")]
    public Text currency;
    public Text currency1;

    int currentCash;

    static string cashPref = "Cash";

    [Header("Selection Scenario")]
    [SerializeField] GameObject NotHaveEnoughMoneyPopUp;

    [Header("Camera OffSet")]
    [SerializeField] Vector3 CamOffSet;

    string[] planeNames = { "Plane1", "Plane2", "Plane3", "Plane4", "Plane5", "Plane6", "Plane7", "Plane8" }; /// // pref names To Store plane Purchase Data neither purchased or not 

    [Header("**************** PLANES Prices ****************")]
    int[] planePrice = { 0, 1500, 2000, 2700, 3500, 4000, 4500, 5000 };

    //public Transform[] PlaneSpawnPoint;
    [SerializeField] LevelsDataHandler _LevelsDataHandler;
    private void Start()
    {
        instance = this;
        AdsManager.Instance.ShowBanner();
        loading.SetActive(false);
        SoundManager.PlaySound(SoundManager.NameOfSounds.MainMenu);
        //SoundManager.instance.PlayMusic("gameplaymusic");
        currency.text = PrefData.GetCoinsAmount().ToString();
        currency1.text = PrefData.GetCoinsAmount().ToString();
        currentCash = PrefData.GetCoinsAmount();
        priceText.gameObject.SetActive(false);
        PlayerPrefs.SetInt(planeNames[0], 1);
        Debug.Log(PrefData.GetCurrentLevel());

        if (_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].RequireSpecificPlane)
        {
            Plane(_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].PlaneIndex);
            //if (PlayerPrefs.GetInt(planeNames[PlaneIndex]) == 0)
            //{
            //    TryFreeRideBtn.SetActive(true);
            //}
            //  RequiredPlanePanel.SetActive(true);

            prev.SetActive(false);
            next.SetActive(false);
            PlaneSelectionSlider.SetActive(false);
        }
        else
        {
            prev.SetActive(false);
            Plane(PlaneIndex);
        }

    }



    public void NextPlane()
    {
        // SoundManager.instance.PlaySoundsOneShot("click");
        PlaneIndex++;
        if (PlaneIndex == Planes.Length - 1)
        {
            Planes[PlaneIndex - 1].gameObject.SetActive(false);
            next.SetActive(false);
        }
        else
        {
            Planes[PlaneIndex - 1].gameObject.SetActive(false);
            next.SetActive(true);
        }

        if (PlaneIndex >= 1)
            prev.SetActive(true);
        Plane(PlaneIndex);
    }
    public void PrePlane()
    {
        // SoundManager.instance.PlaySoundsOneShot("click");
        PlaneIndex--;
        if (PlaneIndex <= 0)
        {
            Planes[PlaneIndex + 1].gameObject.SetActive(false);
            prev.SetActive(false);
        }
        else
        {
            Planes[PlaneIndex + 1].gameObject.SetActive(false);
            prev.SetActive(true);
        }
        if (PlaneIndex <= Planes.Length - 1)
            next.SetActive(true);
        Plane(PlaneIndex);
    }


    //Planes Changes
    /// <summary>
    /// This Methode Control Plane Spawn and other Working.
    /// </summary>
    /// <param name="index"></param>
    public void Plane(int index)
    {
        PlaneIndex = index;
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);


        #region ******** Specification ********

        //Speed.value = planeSpecifications[PlaneIndex].Brakes;
        //Handling.value = planeSpecifications[PlaneIndex].Speed;
        //Brakes.value = planeSpecifications[PlaneIndex].Brakes;
        #endregion
        Planes[preSelectedPlane].SetActive(false);
        Planes[index].SetActive(true);

        if (preSelectedPlane != index)
        {
            Debug.Log(index);
            preSelectedPlane = index;
        }
        // Purchasing

        if (PlayerPrefs.GetInt(planeNames[PlaneIndex]) >= 1)
        {
            buyButton.gameObject.SetActive(false);
            priceText.gameObject.SetActive(true);
            play.gameObject.SetActive(true);
            priceText.text = "Owned";
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            priceText.gameObject.SetActive(true);
            priceText.text = $"${planePrice[PlaneIndex]}";
            play.gameObject.SetActive(false);
        }

    }

    public void BackFromPlaneSelection()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        loading.SetActive(true);
        StartCoroutine(LoadLevel(0.1f, 2));
    }
    public void ClosePopUp(GameObject gameObject)
    {
        //SoundManager.instance.PlaySoundsOneShot("click");
        //AdsManager.Instance.CallInterstitialAd(Adspref.JustStatic, "_RewardPopClose");
        gameObject.SetActive(false);
    }


    public void PlaneSelected()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        loading.SetActive(true);
        StartCoroutine(LoadLevel(0.1f, 4));
        //SceneManager.LoadSceneAsync(4);
    }
    IEnumerator LoadLevel(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        AdsManager.Instance.ShowInterstitial("");
        yield return new WaitForSeconds(delay * 2);
        StartCoroutine(LoadSceneAsync(index));
    }
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        while (!async.isDone)
        {
            fillBar.fillAmount = async.progress;
            yield return null;
        }
    }
    public void RewardCoins(int rewardAmount)
    {
        currency.text = currentCash.ToString();
        currency1.text = currentCash.ToString();

    }

    public void BuyPlane()
    {

        if (currentCash >= planePrice[PlaneIndex])
        {

            PlayerPrefs.SetInt(planeNames[PlaneIndex], 1);
            Plane(PlaneIndex);
            currentCash -= planePrice[PlaneIndex];
            PrefData.SetCoinsAmount(currentCash, false);
            currency.text = PrefData.GetCoinsAmount().ToString();
            currency1.text = PrefData.GetCoinsAmount().ToString();

            buyButton.gameObject.SetActive(false);
            play.gameObject.SetActive(true);
            //TryFreeRideBtn.SetActive(false);
        }
        else
        {

            NotHaveEnoughMoneyPopUp.SetActive(true);
            Debug.Log("Not Enough Cash");
        }

    }

    public void UpdateCoins()
    {
        currency.text = PrefData.GetCoinsAmount().ToString();
        currency1.text = PrefData.GetCoinsAmount().ToString();

        currentCash = PrefData.GetCoinsAmount();
    }

    public void Reward(int amount)
    {
        int oldCash = PlayerPrefs.GetInt(cashPref);
        int newCash = oldCash + amount;
        PlayerPrefs.SetInt(cashPref, newCash);
        currency.text = PlayerPrefs.GetInt(cashPref).ToString();
        // currency.text = PrefData.GetCoinsAmount().ToString();
        UpdateCoins();

    }
    
    public void Selectedplane(int index)
    {

        PlaneIndex = index;
        if (PlaneIndex == 0)
        {
            prev.SetActive(false);
            next.SetActive(true);
        }
        else if (PlaneIndex == Planes.Length - 1)
        {
            next.SetActive(false);
            prev.SetActive(true);
        }
        Plane(PlaneIndex);


    }

    public void PlayClickSound()
    {
        //SoundManager.instance.PlaySoundsOneShot("click");
    }

    public void UnlockAllPlanes()
    {
        //SoundManager.instance.PlaySoundsOneShot("click");
    }

}
