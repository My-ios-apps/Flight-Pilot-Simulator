using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip button;
    public AudioClip MainMenu;
    public AudioClip GamePlay;
    public AudioClip LevelComplete;
    public AudioClip LevelFail;
    public AudioClip CheckPoint;
    public AudioSource soundSource;
    public AudioSource musicSource;
    public enum NameOfSounds
    {
        Button,
        MainMenu,
        CheckPoint,
        GamePlay,
        LevelComplete,
        LevelFail
    }
    public static SoundManager Instance;
    public static SoundManager soundInstance;

    static string soundPref = "Sounds";
    static string musicPref = "Music";
    // Use this for initialization
    void Awake()
    {
        Instance = this;
        if (soundInstance == null)
        {
            soundInstance = this;
        }
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey(soundPref))
        {
            PlayerPrefs.SetFloat(soundPref, 1);
            soundSource.volume = 1;
        }
        else
        {
            soundSource.volume = PlayerPrefs.GetFloat(soundPref);
        }
        if (!PlayerPrefs.HasKey(musicPref))
        {
            PlayerPrefs.SetFloat(musicPref, 1);
        }
        else
        {
            musicSource.volume = PlayerPrefs.GetFloat(musicPref);
        }
    }
    public void SoundSettings(float val)
    {
        soundSource.volume = val;
    }
    public void MusicSettings(float val)
    {
        musicSource.volume = val;
    }
    public static void PlaySound(NameOfSounds id)
    {
        switch (id)
        {
            case (NameOfSounds.Button):
                {
                    Instance.playSound(Instance.button, false);
                    break;
                }

            case (NameOfSounds.MainMenu):
                {
                    Instance.playMusic(Instance.MainMenu, true);
                    break;
                }
            case (NameOfSounds.GamePlay):
                {
                    Instance.playMusic(Instance.GamePlay, true);
                    break;
                }
            case (NameOfSounds.LevelComplete):
                {
                    Instance.playMusic(Instance.LevelComplete, false);
                    break;
                }
            case (NameOfSounds.LevelFail):
                {
                    Instance.playMusic(Instance.LevelFail, false);
                    break;
                }
            case (NameOfSounds.CheckPoint):
                {
                    Instance.playMusic(Instance.CheckPoint, false);
                    break;
                }
        }
    }


    void playSound(AudioClip sound, bool isLoop)
    {
        soundSource.clip = sound;
        //soundSource.volume = volume;
        soundSource.loop = isLoop;
        soundSource.Play();
    }
    void playMusic(AudioClip sound, bool isLoop)
    {
        musicSource.clip = sound;
        //musicSource.volume = volume;
        musicSource.loop = isLoop;
        musicSource.Play();
    }
}
