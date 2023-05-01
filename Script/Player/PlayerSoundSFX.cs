using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSFX : MonoBehaviour
{
    //refer to main object
    private PlayerController player;
    //footstep time count
    private float footstepTimer;
    //footstep time count maximum can;
    [SerializeField] private float footstepTimeMax = 0.15f;

    //volume for footstep
    [SerializeField] private float volume = 1f;

    private void Awake() {
        player = GetComponent<PlayerController>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f){
            footstepTimer = footstepTimeMax;

            //check variable isWalking
            if (player.isWalking)
                SoundEffectManager.Instance.PlayFootStepSFX(player.transform.position, volume);
        }
    }
}
