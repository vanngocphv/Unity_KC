using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance {get; private set;}

    private const string CONST_SFX_VOLUME = "SFX volume";
    [SerializeField] private SoundEffectClipsRefs_SCAB allSoundEffectClips_SCAB;

    private float currentVolumeSFX = 1f;

    private void Awake(){
        Instance = this;

        float defaultVolume = 1f;
        currentVolumeSFX = PlayerPrefs.GetFloat(CONST_SFX_VOLUME, defaultVolume);

    }
    private void Start(){
        DeliveryManager.Instance.OnSucessRecipeSound += OnSuccessDeliverySound_Perform;
        DeliveryManager.Instance.OnFailedRecipeSound += OnFailedDeliverySound_Perform;

        CuttingCounter.OnCuttingSoundEffect += OnCuttingSoundEffect_Perform;
        PlayerController.Instance.OnPickUpKitchenObject += OnPickUpKitchenObject_Perform;

        BaseCounter.OnAnyObjectPlaceSound += OnAnyObjectPlaceSound_Perform;

        TrashCounter.OnDropToTrashBinSFX += OnDropToTrashBinSFX_Perform;

        
    }
    
    //This code will play the sound effect in exactly the position in 3D world space with volume 1f as default (can change)
    private void PlayAudioClip(AudioClip[] array_audioClip, Vector3 position, float volume = 1f){
        AudioSource.PlayClipAtPoint(array_audioClip[Random.Range(0, array_audioClip.Length)], position, currentVolumeSFX * volume);
    }

    private void PlayAudioClip(AudioClip audioClip, Vector3 position, float volume = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, currentVolumeSFX * volume);
    }

    //play success sound effect
    private void OnSuccessDeliverySound_Perform(object sender, System.EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayAudioClip(allSoundEffectClips_SCAB.deliverySuccess, deliveryCounter.transform.position);
    }

    //play failed sound effect
    private void OnFailedDeliverySound_Perform(object sender, System.EventArgs e){
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayAudioClip(allSoundEffectClips_SCAB.deliveryFail, deliveryCounter.transform.position);
    }

    private void OnCuttingSoundEffect_Perform(object sender, System.EventArgs e){
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayAudioClip(allSoundEffectClips_SCAB.chop, cuttingCounter.transform.position);
    }

    private void OnPickUpKitchenObject_Perform(object sender, System.EventArgs e){
        PlayerController player = PlayerController.Instance;
        PlayAudioClip(allSoundEffectClips_SCAB.objectPickup, player.transform.position);
    }

    private void OnAnyObjectPlaceSound_Perform(object sender, System.EventArgs e){
        BaseCounter counter = sender as BaseCounter;
        PlayAudioClip(allSoundEffectClips_SCAB.objectDrop, counter.transform.position);
    }

    private void OnDropToTrashBinSFX_Perform(object sender, System.EventArgs e){
        TrashCounter counter = sender as TrashCounter;
        PlayAudioClip(allSoundEffectClips_SCAB.trashInteract, counter.transform.position);
    }

    public void PlayFootStepSFX(Vector3 position, float volume = 1f){
        PlayAudioClip(allSoundEffectClips_SCAB.footStep, position, currentVolumeSFX * volume);
    }

    public void PlayCountDownSFX(){
        PlayAudioClip(allSoundEffectClips_SCAB.warning, Vector3.zero);
    }
    
    public void PlayWarningSound(Vector3 position){
        PlayAudioClip(allSoundEffectClips_SCAB.warning[1], position);
    }

    public void ChangeVolumeSFX(){
        currentVolumeSFX = (float) System.Math.Round(currentVolumeSFX + 0.1f, 1);
        if (currentVolumeSFX > 1f) currentVolumeSFX = 0f;

        PlayerPrefs.SetFloat(CONST_SFX_VOLUME, currentVolumeSFX);
        PlayerPrefs.Save();
    }
    public float GetCurrentVolumeSFX(){
        return currentVolumeSFX;
    }

}
