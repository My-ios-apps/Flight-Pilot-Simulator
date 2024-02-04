using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelData
{
    [SerializeField] string LevelName;
    [Header("__________Vectors__________")]
    public Vector3 PlaneSpawnPos;
    [Header("__________Quaternion__________")]
    public Quaternion PlaneSpawnRot;
    [Header("************* Boolean *************")]
    public bool ChangeInputValuesDefault;
    public bool StopRigidBodyPositions;
    public bool LoadAdditiveScene;
    public bool RequireSpecificPlane;
    [Header("************* Int *************")]
    public int PlaneIndex;
    [Header("************* Float *************")]
    public float OnCompleteRewardAmount;
    public float LevelTimerLenght;
    [Header("_____ Plane Assets _____")]
    public float PlanePitchEffect;
    [Header("_____ Additive Scene Name_____")]
    public string AdditiveSceneName;
    [Header("_____ Objectives ____")]
    [TextArea()]
    public string Objective;
}
[System.Serializable]
public class RescueLevelData
{
    [SerializeField] string LevelName;
    [Header("__________Vectors__________")]
    public Vector3 PlaneSpawnPos;
    [Header("__________Quaternion__________")]
    public Quaternion PlaneSpawnRot;
    [Header("_____ Plane Assets _____")]
    public float PlanePitchEffect;
    [Header("************* Boolean *************")]
    public bool ChangeInputValuesDefault;
    public bool StopRigidBodyPositions;
    public bool LoadAdditiveScene;
    public bool RequireSpecificPlane;
    public bool HaveTwoCheckpoints;  ////// <------- For Multiple Check points in Level After First Objective Done .
    [Header("************* Int *************")]
    public int PlaneIndex;
    [Header("************* Float *************")]
    public float OnCompleteRewardAmount;
    [Header("_____ Additive Scene Name_____")]
    public string AdditiveSceneName;
    [Header("_____ Objectives ____")]
    [TextArea()]
    public string Objective;
}
[CreateAssetMenu(menuName = "LevelDataHandler")]
public class LevelsDataHandler : ScriptableObject
{
    public LevelData[] levelData;
    public RescueLevelData[] RescueLevelData;

}

