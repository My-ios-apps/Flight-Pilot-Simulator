using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Aeroplane;

public class PlaneController : MonoBehaviour
{

    [Header("****************Boolean****************")]

    public bool CollideOneTime;
    public static bool FinishReached;
    bool ForMultiCallCutScene;
    [Header("**************** RigidBody ****************")]
    public Rigidbody My_RigidBody;
    [Header("**************** Aeroplane Scripts ****************")]
    public AeroplaneUserControl4Axis aeroplaneUserControl4Axis;
    public AeroplaneController AeroplaneController;
    public AeroplaneAudio _AeroplaneAudio;
    [Header("**************** Intergers ****************")]
    public int CheckPointCounter;
    [Header("**************** Particles ****************")]
    public GameObject BlastParticle;
    public GameObject BlastParticleRingBall;
    public GameObject CheckpointReachParticle;
    public GameObject TiresSmokes;
    public GameObject WaterParticle;
    [Header("**************** Layer ****************")]
    [SerializeField] LayerMask CollideLayer;

    int rand;
    public static int randRefHold;
    private void Awake()
    {
        FinishReached = false;
        // aeroplaneUserControl4Axis = GetComponent<AeroplaneUserControl4Axis>();

    }
    #region ___________ Collision && Triggers ___________
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") && !CollideOneTime)                 //////////////////////////////////// < ----------------------------------- Checking Checkpoint Trigger
        {
            SoundManager.PlaySound(SoundManager.NameOfSounds.CheckPoint);
            SoundManager.PlaySound(SoundManager.NameOfSounds.GamePlay);

            if (levelSelectionScript.CurrentLevelIndex != 4)
            {
                other.gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("FinishPoint"))
        {
            FinishReached = true;
            // SoundManager.instance.PlaySoundsOneShot("Checkpoint");


            switch (levelSelectionScript.CurrentLevelIndex)
            {
                case 1:
                    GameManager.instance.BeforeCompleteLevelActions();
                    break;
                case 2:
                    GameManager.instance.PlaneLandingActions();
                    break;
                case 3:
                    GameManager.instance.CallEndCutScene = true;
                    GameManager.instance.StartCutScene();
                    break;
                case 4:
                    GameManager.instance.BeforeCompleteLevelActions();
                    break;
                case 5:
                    GameManager.instance.PlaneLandingActions();
                    break;
                case 6:
                    GameManager.instance.PlaneLandingActions();
                    break;
            }

            other.gameObject.SetActive(false);
        }

        //if (other.CompareTag("StopPoint"))                 //////////////////////////////////// < ----------------------------------- Checking Checkpoint Trigger
        //{
        //    GameManager.instance.EndCutSceneObject[0].SetActive(true);
        //    GameManager.instance.EndCutSceneObject[1].SetActive(false);
        //   // GameManager.instance.PlaneRescueActions();
        //}

        //if (other.CompareTag("ReachPoint"))                 //////// < --------Checking Reach Point For RescueMode
        //{
        //    CheckpointReachParticle.SetActive(true);
        //    other.gameObject.SetActive(false);
        //   // SoundManager.instance.PlaySoundsOneShot("Checkpoint");
        //    StartCoroutine(DisableCheckPointParticle());


        //}


        //if (other.CompareTag("Water") && !CollideOneTime)                 //////////////////////////////////// < ----------------------------------- Checking Water Trigger For Level Failed
        //{
        //    WaterParticle.SetActive(true);

        //    FailedLevel();
        //    CollideOneTime = true;
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CollideLayer == (CollideLayer | (1 << collision.gameObject.layer))) // check if the collided object is on the fail layer
        {
            Debug.Log(collision.gameObject.name);
            FailedLevel(); // load the fail scene
        }
        // if (!CollideOneTime)
        //{
        //  //  FinishReached = true;
        //    //BlastParticle.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //    //BlastParticle.SetActive(true);
        //    FailedLevel();
        //    CollideOneTime = true;
        //}
    }
    #endregion
    IEnumerator DisableCheckPointParticle()
    {
        yield return new WaitForSeconds(2f);
        CheckpointReachParticle.SetActive(false);
    }


    public void FailedLevel()
    {
        //BlastParticle.transform.SetParent(null);
        GameManager.instance.LevelFailed();
        GameManager.instance.PlaneCamera.m_Follow = null;


    }

    IEnumerator LevelFailedDelayCall(int time)
    {
        yield return new WaitForSeconds(time);
        // GameManager.instance.LevelFailed("PlaneCrash");
    }



}

