using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollider : MonoBehaviour
{
    //private gamePlay failLevel;
    //	public GameObject plane;
    public delegate void PlaneCrash();
    public static event PlaneCrash OnPlaneCrash;
    private AudioSource explosion;
    [SerializeField] bool AIPLane;
    void Start()
    {
        explosion = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log ("sfdfdfd");
            //Destroy (other.gameObject);
            if (!AIPLane)
            {
                GameManager.instance.LevelFailed();
                explosion.Play();
            }
            //GameConstant.gameFailReason = "Plane Crashed  Better Luck Next Time";

            //failLevel.GameFail ();
        }
    }
}
