using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Main Counter
    [SerializeField] private BaseCounter mainCounter;
    // Visual Selected Counter
    [SerializeField] private GameObject[] visualSelectedCounter;

    // Start is called before the first frame update
    void Start()
    {
        //this code will be run exactly because Instance has been set before
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounter;
    }

    private void Player_OnSelectedCounter(object sender, PlayerController.OnSelectedCounterChangedEventArgs e){
        if (e.selectedCounterArgs == mainCounter){
            ShowVisualSeleted();
        }
        else{
            HideVisualSelected();
        }
    }

    private void ShowVisualSeleted(){
        foreach (GameObject gameObject in visualSelectedCounter){
            gameObject.SetActive(true);
        }
    }
    
    private void HideVisualSelected(){
        foreach (GameObject gameObject in visualSelectedCounter){
            gameObject.SetActive(false);
        }
    }
}
