using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImageUI;

    private void Update() {
        if (MainGameManager.Instance.IsGamePlaying()){
            float currentAmount = MainGameManager.Instance.GetCurrentPlayingTimer();
            if(currentAmount > 0.4f && currentAmount < 0.8f){
                clockImageUI.color = Color.yellow;
            }
            else if (currentAmount >= 0.8f){
                clockImageUI.color = Color.red;
            }
            clockImageUI.fillAmount = currentAmount;
        }
    }
}
