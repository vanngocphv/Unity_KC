using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject counterGameObject;
    [SerializeField] private Image progressBar;

    private IF_HasProgress hasProgressGameObject;

    void Start(){
        hasProgressGameObject = counterGameObject.GetComponent<IF_HasProgress>();
        if (hasProgressGameObject != null){
            //when this progress bar has been active or first frame, it will set event for cutting counter
            hasProgressGameObject.OnProgressBarChangeUI += OnProgressBarChangeUI_Performed;
            progressBar.fillAmount = 0f;

            HideProgressBar();
        }
        else{
            Debug.LogError("This Counter doesn't have any IF_HasProgress Component");
        }
    }

    //This event will fire every the button interact alternate has been clicked
    private void OnProgressBarChangeUI_Performed(object sender, IF_HasProgress.OnProgressBarChangeUIArgs e){
        progressBar.fillAmount = e.changeAmount;
        if (progressBar.fillAmount == 0f || progressBar.fillAmount == 1f){
            HideProgressBar();
        }
        else{
            ShowProgressBar();
        }
    }

    private void ShowProgressBar(){
        gameObject.SetActive(true);
    }
    private void HideProgressBar(){
        gameObject.SetActive(false);
    }
}
