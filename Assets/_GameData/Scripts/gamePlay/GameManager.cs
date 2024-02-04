using Cinemachine;
using ControlFreak2;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Aeroplane;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelsDataHandler _LevelsDataHandler;
    public delegate void SliderValueChanged(float val);
    public static event SliderValueChanged onSliderValueChanged;
    #region -------------------------------------GameObjects && Other Components----------------------------
    [Space(10)]
    [Header("************* planes *************")]
    public PlaneController[] PlanePrefabs;
    [Header("************* Camera *************")]
    public GameObject CameraMainObject;
    public CinemachineBrain PlaneCinemachineBrain;
    public CinemachineVirtualCamera PlaneCamera;
    [HideInInspector] public CinemachineOrbitalTransposer OrbitalTransposer;
    [Header("************* Particles *************")]
    public GameObject ConffetiParticles;
    public GameObject FuelWarningParticle;
    [Header("************* CheckPoint *************")]
    public GameObject CheckPoint;
    [Header("************* Scene Ref Holder *************")]
    AsyncOperation AdditiveSceneRefHolder;
    [Header("************* Environment *************")]
    public GameObject Environment;
    [Header("************* CheckPoint_Object *************")]
    public GameObject[] CheckPointObject;
    public GameObject[] EndCutSceneObject;
    [Header("************* AiPlane *************")]
    public AeroplaneAiControl AIPlane;
    public GameObject Level5AiPlaneObject;
    #endregion
    #region -------------------------------------Integers----------------------------
    [SerializeField] int LevelIndex;
    [SerializeField] int PlaneIndex;
    [SerializeField] int TempLevel;
    #endregion
    #region ____Texts_____
    [Header("************* UI *************")]
    [Header("**** Text ****")]
    public Text[] CoinText;
    public Text[] TotalCoins;
    [SerializeField] Text Objective;
    public Text Altitude;

    #endregion
    #region _____Sliders_______
    [Header("**** Slider ****")]
    public Slider ThrottleSlider;
    #endregion

    #region _____Controls_____
    [Header("**** Plane Controls Buttons And Target Indecators ****")]
    public GameObject PlaneControlsButton;
    public GameObject CanvasMainObject;
    public GameObject BrakeBtn;
    public GameObject PlaneLandingPositionHolder;
    public GameObject LandingCutScene;
    public GameObject MainCutScene;
    public GameObject[] LevelStartCutScenes;
    public GameObject[] LevelEndCutScenes;

    #endregion
    #region _____LevelStatePanel_____
    [Header("**** LevelsStates ****")]
    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;
    public GameObject GamePausePanel;
    public GameObject Loading;
    public Image fillBar;
    public Text lvlCompReward;
    public Text lvlFailReward;
    #endregion
    #region _____ Images _____
    [Header("**** UI_Image ****")]
    //public DOTweenAnimation FadeScreenLevelComplete;
    // public DOTweenAnimation PlaneSelectionFadeScreen;
    [SerializeField] GameObject ThrottleBtn;
    [SerializeField] GameObject JoyStick;
    [SerializeField] GameObject Brake;
    [SerializeField] GameObject DragScreenTutorial;

    public Sprite[] RankNumbers;
    public Image BadgeNum;
    public Image FuelImage;
    public Image ReviveTimeToRefillPlane;
    public GameObject FuelUIMainElement;
    public GameObject BrakeClickBlocker;
    #endregion
    #region _____ Buttons _____
    [Header("**** UI_Buttons ****")]
    public GameObject LandingGearBTN;
    public GameObject NextLevelLoadBTN;
    public Image BrakesObject;
    public GameObject _2xMultiplySpeedBtn;
    #endregion
    #region ________ Panels __________
    [Header("**** Panels ****")]
    [SerializeField] GameObject FuelRefillPanel;
    #endregion
    public bool BlockMultiplyCompleteCall = false;
    public bool CallEndCutScene;
    bool blockCutSceneCall;
    private void Awake()
    {
        instance = this;
        AdsManager.Instance.ShowBanner();
        //LevelIndex = PrefData.GetCurrentLevel();
    }
    private void Start()
    {
        //PrefData.SetCurrentLevel(5, false);
        SoundManager.PlaySound(SoundManager.NameOfSounds.GamePlay);

        Debug.Log(levelSelectionScript.CurrentLevelIndex);

        OrbitalTransposer = PlaneCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        if (levelSelectionScript.CurrentLevelIndex == 0)
        {
            ActivePlane(PlaneIndex, true);
            LevelWorking();
        }
        else
        {
            if (levelSelectionScript.CurrentLevelIndex == 3 || levelSelectionScript.CurrentLevelIndex == 4
                                || levelSelectionScript.CurrentLevelIndex == 5 || levelSelectionScript.CurrentLevelIndex == 6)
            {
                Debug.Log("Req");
                ActivePlane(_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].PlaneIndex, true);
            }
            else
            {
                Debug.Log("NotReq");
                ActivePlane(PlaneIndex, true);
            }
            LevelWorking();
        }
    }
    public void ActivePlane(int Index, bool Check)
    {

        if (Check)
        {
            PlaneIndex = Index;
            // PlaneControlsButton.SetActive(true);
            PlanePrefabs[Index].gameObject.SetActive(true);
            PlaneCamera.m_Follow = PlanePrefabs[Index].transform;
            PlaneCamera.m_LookAt = PlanePrefabs[Index].transform;
            PlanePrefabs[Index].transform.position = _LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].PlaneSpawnPos;
            PlanePrefabs[Index].transform.rotation = _LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].PlaneSpawnRot;
            PlanePrefabs[Index].AeroplaneController.m_PitchEffect = _LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].PlanePitchEffect;
            PlanePrefabs[Index].aeroplaneUserControl4Axis.enabled = true;
            PlanePrefabs[Index].AeroplaneController.enabled = true;
            PlanePrefabs[Index].My_RigidBody.constraints = RigidbodyConstraints.None;
        }
    }
    void LevelWorking()                                                                //////////// < --------------------------- Level Conditions Wise Working
    {
        if (!blockCutSceneCall)
        {


            if (_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].ChangeInputValuesDefault)
            {
                ThrottleSlider.value = 50f;
            }
            if (_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].StopRigidBodyPositions)        ///// <------ If there is Any Need To Set plane in Air and Start Level From Air also Set in " Levels Data Handler " Scriptable Object.
            {
                PlanePrefabs[PlaneIndex].My_RigidBody.isKinematic = true;
            }
            if (_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].LoadAdditiveScene)
            {
                if (!SceneManager.GetSceneByName(_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].AdditiveSceneName).isLoaded)
                {
                    AdditiveSceneRefHolder = SceneManager.LoadSceneAsync(_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].AdditiveSceneName, LoadSceneMode.Additive);   ///// <--------- Check level Have Additive Scene to load In " Levels Data Handler " Scriptable Object.
                }
            }
            //if (LevelIndex != 0)
            //{
            //    PlanePrefabs[PlaneIndex].AeroplaneController.m_YawEffect = 0;
            //    PlanePrefabs[PlaneIndex].AeroplaneController.m_RollEffect = 0;              ////// < -------------------- For Avoid To Rotate Plane && Move Left or Right Before Take Of
            //}


            switch (levelSelectionScript.CurrentLevelIndex)                                         ///// <------- levels Working ----------------
            {
                case 0:
                    ThrottleBtn.SetActive(true);
                    StartCoroutine(DisableThrottle(5f));
                    break;
                case 1:
                    CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);

                    Debug.Log("DoNoting");
                    break;
                case 2:
                    CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);
                    Debug.Log("DoNoting");
                    break;
                case 3:
                    LevelStartCutScenes[0].SetActive(true);
                    StartCutScene();
                    CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);
                    Debug.Log("DoNoting");
                    break;
                case 4:
                    CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);
                    PlaneCinemachineBrain.gameObject.SetActive(false);
                    LevelStartCutScenes[1].SetActive(true);

                    Level5AiPlaneObject.SetActive(true);
                    StartCutScene();
                    break;
                case 5:
                    LevelStartCutScenes[2].SetActive(true);
                    StartCutScene();
                    CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);
                    break;
                case 6:

                    CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);

                    Debug.Log("DoNoting");
                    break;
            }
            blockCutSceneCall = true;
            return;
        }
    }
    public void StartCutScene()                                                           //////////// < --------------------------- CutScene Start 
    {
        Time.timeScale = .8f;
        blockCutSceneCall = true;
        CameraMainObject.SetActive(false);
        PlaneCamera.gameObject.SetActive(false);
        MainCutScene.SetActive(true);
        PlaneControlsButton.SetActive(false);
        if (!CallEndCutScene)
        {
            PlaneControlsButton.SetActive(false);
            //ActivePlane(PlaneIndex, true);

            LevelWorking();
        }
        else
        {
            ActivePlane(PlaneIndex, false);
            EndCutSceneObject[0].SetActive(true);
        }
        /////// Disable All Ui Buttons                                                                                                      

    }
    public void EndCutScene()                                                            //////////// < ---------------------------  CutScene End 
    {
        Time.timeScale = 1;
        MainCutScene.SetActive(false);
        PlaneControlsButton.SetActive(true);
        CameraMainObject.SetActive(true);
        PlaneCamera.gameObject.SetActive(true);
        PlanePrefabs[PlaneIndex].gameObject.SetActive(true);
        // FuelUIMainElement.SetActive(true);
        PlaneControlsButton.SetActive(true);
        if (levelSelectionScript.CurrentLevelIndex == 4)
        {
            AIPlane.enabled = true;
        }
        for (int i = 0; i < LevelStartCutScenes.Length; i++)
        {
            LevelStartCutScenes[i].SetActive(false);
        }
        CheckPointObject[levelSelectionScript.CurrentLevelIndex].SetActive(true);
        PlaneCinemachineBrain.gameObject.SetActive(true);
        //CheckPointObject[LevelIndex].SetActive(true);
    }
    IEnumerator DisableThrottle(float time)
    {
        yield return new WaitForSeconds(time);
        ThrottleBtn.SetActive(false);
        StartCoroutine(ableJoyStick(4f));
    }
    IEnumerator ableJoyStick(float time)
    {
        yield return new WaitForSeconds(time);
        JoyStick.SetActive(true);
        StartCoroutine(DisableJoyStick(5f));
    }
    IEnumerator DisableJoyStick(float time)
    {
        yield return new WaitForSeconds(time);
        JoyStick.SetActive(false);
        StartCoroutine(ableBrake(4f));
    }
    IEnumerator ableBrake(float time)
    {
        yield return new WaitForSeconds(time);
        Brake.SetActive(true);
        StartCoroutine(DisableBrake(4f));
    }
    IEnumerator DisableBrake(float time)
    {
        yield return new WaitForSeconds(time);
        Brake.SetActive(false);
        StartCoroutine(ableDragScreen(4f));
    }
    IEnumerator ableDragScreen(float time)
    {
        yield return new WaitForSeconds(time);
        DragScreenTutorial.SetActive(true);
        StartCoroutine(DisableDragScreen(4f));
    }
    IEnumerator DisableDragScreen(float time)
    {
        yield return new WaitForSeconds(time);
        DragScreenTutorial.SetActive(false);
        BeforeCompleteLevelActions();
    }
    public void RotateCamera(bool Check)    ////////////////////////////////////// <----------------------------- Camera Rotataion
    {
        if (Check)
        {
            OrbitalTransposer.m_XAxis.Value += Input.GetTouch(0).deltaPosition.x * Time.fixedDeltaTime * 5f;
            OrbitalTransposer.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
    public void BeforeCompleteLevelActions()
    {
        PlaneControlsButton.SetActive(false);
        //FadeScreenLevelComplete.gameObject.SetActive(true);
        //FadeScreenLevelComplete.DOPlay();
        PlanePrefabs[PlaneIndex].AeroplaneController.m_MaxEnginePower = 100;
        PlanePrefabs[PlaneIndex].aeroplaneUserControl4Axis.ExternalControls = true;

        PlaneCamera.m_Follow = null;
        PlaneCamera.transform.position = new Vector3(PlaneCamera.transform.position.x - 5, PlaneCamera.transform.position.y + 5, PlaneCamera.transform.position.z + Random.Range(300, 350));
        StartCoroutine(LevelCompleteDelay(3f));

    }
    IEnumerator LevelCompleteDelay(float time)
    {
        yield return new WaitForSeconds(time);
        LevelComplete();
    }
    public void ThrottleValueChange()
    {
        if (onSliderValueChanged != null)
        {
            onSliderValueChanged(ThrottleSlider.value);
        }
        if (_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].StopRigidBodyPositions)
        {
            PlanePrefabs[PlaneIndex].My_RigidBody.isKinematic = false;

        }
        PlanePrefabs[PlaneIndex].AeroplaneController.m_MaxEnginePower = ThrottleSlider.value;
        PlanePrefabs[PlaneIndex].aeroplaneUserControl4Axis.enabled = true;
        PlanePrefabs[PlaneIndex].My_RigidBody.drag = 0;
        PlanePrefabs[PlaneIndex].My_RigidBody.isKinematic = false;
        if (_LevelsDataHandler.levelData[LevelIndex].ChangeInputValuesDefault)
        {
            //PlanePrefabs[PlaneIndex].AeroplaneController.m_PitchEffect = 1;
            PlanePrefabs[PlaneIndex].My_RigidBody.isKinematic = false;
        }


    }
    public void BreakPress(bool check)
    {
        if (check)
        {

            PlanePrefabs[PlaneIndex].aeroplaneUserControl4Axis.enabled = false;
            PlanePrefabs[PlaneIndex].AeroplaneController.m_MaxEnginePower = 0;
            ThrottleSlider.value = 0;
            PlanePrefabs[PlaneIndex].My_RigidBody.drag = 1;
        }
    }

    public void LevelComplete()
    {
        AdsManager.Instance.ShowInterstitial("");
        if (!BlockMultiplyCompleteCall)
        {
            SoundManager.PlaySound(SoundManager.NameOfSounds.LevelComplete);
            //  CheckPoint.SetActive(false);
            BlockMultiplyCompleteCall = true;

            LevelCompletePanel.SetActive(true);

            PlanePrefabs[PlaneIndex].gameObject.SetActive(false);
            //  SoundManager.instance.Music.Stop();
            if(GameConstant.isCareerMode)
            {
                if (levelSelectionScript.CurrentLevelIndex >= PrefData.GetUnlockLevel())
                {
                    PrefData.SetUnlockLevel(1, true);
                    Debug.Log(PrefData.GetUnlockLevel());
                }
            }
            

            PrefData.SetCoinsAmount((int)_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].OnCompleteRewardAmount, true);
            lvlCompReward.text = "Level Reward : " + _LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].OnCompleteRewardAmount;
            //TotalCoins[0].text = "$" + PrefData.GetCoinsAmount().ToString();
            //CoinText[0].text = "$" + _LevelsDataHandler.levelData[LevelIndex].OnCompleteRewardAmount.ToString();
            //if (!LevelSelection.TempUnlock)
            //{
            //    
            //    
            //    Debug.Log($"{PrefData.GetUnlockLevel()} Unlocked Levels");
            //}
            if (_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].LoadAdditiveScene)
            {
                SceneManager.UnloadSceneAsync(_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].AdditiveSceneName);
            }



            Time.timeScale = 0;
        }
    }
    public void PlaneLandingActions()
    {
        MainCutScene.SetActive(true);
        //StartCoroutine(ActiveTiresSmoke(1f));
        PlaneCinemachineBrain.gameObject.SetActive(false);
        // HudSystem.SetActive(false);
        // HUDCanvas.SetActive(false);
        PlaneControlsButton.SetActive(false);
        // FadeScreenLevelComplete.gameObject.SetActive(true);
        // FadeScreenLevelComplete.DOPlay();
        for (int i = 0; i < LevelStartCutScenes.Length; i++)
        {
            CheckPointObject[i].SetActive(true);
        }

        PlanePrefabs[PlaneIndex].enabled = false;
        PlanePrefabs[PlaneIndex].My_RigidBody.isKinematic = true;
        PlanePrefabs[PlaneIndex].aeroplaneUserControl4Axis.enabled = false;
        PlanePrefabs[PlaneIndex].transform.SetParent(PlaneLandingPositionHolder.transform);
        PlanePrefabs[PlaneIndex].transform.localPosition = new Vector3(0, 0, 0);
        PlanePrefabs[PlaneIndex].transform.localRotation = Quaternion.identity;
        PlaneCamera.gameObject.SetActive(false);
        LandingCutScene.SetActive(true);
    }

    public void LevelFailed()
    {
        AdsManager.Instance.ShowInterstitial("");
        SoundManager.PlaySound(SoundManager.NameOfSounds.LevelFail);
        PlanePrefabs[PlaneIndex].gameObject.SetActive(false);
        int reward = (int)_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].OnCompleteRewardAmount;
        lvlFailReward.text = "Level Reward : " + (reward * 0.1f);
        //SoundManager.instance.Music.Stop();
        LevelFailedPanel.SetActive(true);
        //PrefData.SetCoinsAmount((int)500, true);
        //CoinText[1].text = "$500";
        //TotalCoins[1].text = "$" + PrefData.GetCoinsAmount().ToString();
        Time.timeScale = 0;
    }
    public void TwoXReward()
    {
        PrefData.SetCoinsAmount((int)_LevelsDataHandler.levelData[levelSelectionScript.CurrentLevelIndex].OnCompleteRewardAmount, true);
    }
    public void ResumeBtn()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Time.timeScale = 1;
        GamePausePanel.SetActive(false);
    }
    public void PauseBtn()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        AdsManager.Instance.ShowInterstitial("");
        Time.timeScale = 0;
        GamePausePanel.SetActive(true);
    }
    public void NextBtn()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0.1f, 2));
        //SceneManager.LoadScene(2);
    }
    public void HomeBtn()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0.1f, 1));
    }
    public void RestartBtn()
    {
        SoundManager.PlaySound(SoundManager.NameOfSounds.Button);
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0.1f, SceneManager.GetActiveScene().buildIndex));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadLevel(float delay, int index)
    {
        Loading.SetActive(true);
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
}
