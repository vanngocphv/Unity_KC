using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSoundSFX : MonoBehaviour
{
    //Get a reference to main counter
    [SerializeField] private StoveCounter stoveCounter;

    //Get Audio Source from main object
    private AudioSource audioSource;
    private float warningSoundTimer;
    private float warningSoundTimeMax = 0.2f;

    private bool isWarningSFXPlay = false;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    //with state from the stove, use for check if start sound or not
    private void Start(){
        stoveCounter.OnStoveInState += OnStoveInState_Perform;
        stoveCounter.OnProgressBarChangeUI += OnProgressBarChangeUI_Perform;
    }

    private void OnStoveInState_Perform(object sender, StoveCounter.OnStoveInStateArgs e){
        bool playSFX = e.stateInChange == StoveCounter.State.Frying || e.stateInChange == StoveCounter.State.Fried;
        if (playSFX){
            audioSource.Play();
        }
        else audioSource.Pause();
    }

    private void OnProgressBarChangeUI_Perform(object sender, IF_HasProgress.OnProgressBarChangeUIArgs e){
        isWarningSFXPlay = stoveCounter.isBurning() && e.changeAmount >= 0.5f;
    }

    private void Update(){
        if (isWarningSFXPlay){
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f){
                warningSoundTimer = warningSoundTimeMax;
                SoundEffectManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
