using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter mainCounter;
    
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject stoveParticleOnObject;


    void Awake(){
        
        
    }
    void Start(){
        mainCounter.OnStoveInState += OnStoveInState_Performed;
    }

    private void OnStoveInState_Performed(object sender, StoveCounter.OnStoveInStateArgs e){
        if (e.stateInChange == StoveCounter.State.Frying || e.stateInChange == StoveCounter.State.Fried){
            TurnOnStove();
            
        }
        else{
            TurnOffStove();
        }
    }

    private void TurnOffStove(){
        stoveOnGameObject.SetActive(false);
        stoveParticleOnObject.SetActive(false);
        
    }

    private void TurnOnStove(){
        stoveOnGameObject.SetActive(true);
        stoveParticleOnObject.SetActive(true);
    }

    

    

}
