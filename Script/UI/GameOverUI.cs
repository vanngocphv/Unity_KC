using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    //reference to GameOver TextMeshPro
    //Recipes delivered text
    [SerializeField] private TextMeshProUGUI recipesDeliveredNumberText; //The total recipes has been delivered correctly


    private void Start() {
        MainGameManager.Instance.OnStateChange += OnStateChange_Perfom;

        HideCountdown();
    }

    private void Update(){
        
    }
    private void OnStateChange_Perfom(object sender, System.EventArgs e){
        if (MainGameManager.Instance.IsGameOVer()){
            //show countdown to start
            ShowCountdown();
            //this is over, cannot need update every frame
            recipesDeliveredNumberText.text = DeliveryManager.Instance.GetAmountDeliveredSuccessRecipe().ToString();
        }
        else{
            //hide countdown to start
            HideCountdown();
        }
    }

    private void ShowCountdown(){
        //Show the child gameobject
        gameObject.SetActive(true);
    }

    private void HideCountdown(){
        //Hide the child gameobject
        gameObject.SetActive(false);
    }
}
