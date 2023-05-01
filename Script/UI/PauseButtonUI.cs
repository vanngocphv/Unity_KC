using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image backGround;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite unpauseSprite;

    void Awake(){
        
    }
    void Start(){
        buttonImage.GetComponent<Image>().sprite = pauseSprite;

        MainGameManager.Instance.OnGamePause += OnGamePause_Perform;
        MainGameManager.Instance.OnGameUnpause += OnGameUnpause_Perform;
    } 

    private void OnGamePause_Perform(object sender, System.EventArgs e){
        buttonImage.GetComponent<Image>().sprite = unpauseSprite;
        backGround.GetComponent<Image>().color = Color.yellow;
        
    }

    private void OnGameUnpause_Perform(object sender, System.EventArgs e){
        buttonImage.GetComponent<Image>().sprite = pauseSprite;
        backGround.GetComponent<Image>().color = Color.white;
    }
}
