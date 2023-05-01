using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBGMManager : MonoBehaviour
{
    public static MusicBGMManager Instance {get; private set;}

    private const string CONST_BGM_VOLUME = "Bgm volume";

    //refer to audio
    private AudioSource bgmAudio;
    private float currentVolume = 0.3f;

    private void Awake(){
        //get component audio from game object
        Instance = this;
        bgmAudio = GetComponent<AudioSource>();

        float defaultVolume = 0.3f;
        currentVolume = PlayerPrefs.GetFloat(CONST_BGM_VOLUME, defaultVolume);
        bgmAudio.volume = currentVolume;

    }

    private void Update() {
        
    }

    public void ChangeVolumeBGM(){
        currentVolume = (float) System.Math.Round(currentVolume + 0.1f, 1);
        if (currentVolume > 1f) currentVolume = 0f;

        bgmAudio.volume = currentVolume;

        PlayerPrefs.SetFloat(CONST_BGM_VOLUME, currentVolume);
        PlayerPrefs.Save();
    }
    public float GetCurrentVolumeBGM(){
        return currentVolume;
    }
}
