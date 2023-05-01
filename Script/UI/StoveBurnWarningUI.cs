using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;


    private void Awake(){
        
    }

    private void Start(){
        //Logic is, check if burning in code with bool return, and check event with time left
        stoveCounter.OnProgressBarChangeUI += OnProgressChangeUI;

        HideWarningObject();
    }

    private void OnProgressChangeUI(object sender, IF_HasProgress.OnProgressBarChangeUIArgs e){
        Debug.Log(stoveCounter.isBurning() + " " + e.changeAmount);
        bool isWarning = stoveCounter.isBurning() && e.changeAmount >= 0.5f;
        if (isWarning){
            //Active warning animation
            ShowWarningObject();
        }
        else{
            HideWarningObject();
        }
    }

    private void HideWarningObject(){
        gameObject.SetActive(false);
    }

    private void ShowWarningObject(){
        gameObject.SetActive(true);
    }
}
