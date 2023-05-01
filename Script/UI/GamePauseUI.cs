using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button mainMenuButton;
    private void Awake(){
        resumeButton.onClick.AddListener(OnClickResumeButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        mainMenuButton.onClick.AddListener(OnClickMainMenuButton);

    }
    private void Start() {
        HideGameObject();

        MainGameManager.Instance.OnGamePause += OnGamePause_Perform;
        MainGameManager.Instance.OnGameUnpause += OnGameUnpause_Perform;
    }


    private void ShowGameObject(){
        gameObject.SetActive(true);

        //Set default selected
        resumeButton.Select();
    }
    public void ShowUI(){
        ShowGameObject();
    }
    private void HideGameObject(){
        gameObject.SetActive(false);
    }

    private void OnGamePause_Perform(object sender, System.EventArgs e){
        ShowGameObject();
    }

    private void OnGameUnpause_Perform(object sender, System.EventArgs e){
        HideGameObject();
    }

    private void OnClickResumeButton(){
        MainGameManager.Instance.TogglePauseGame();
        HideGameObject();
        
    }
    private void OnClickOptionButton(){
        //Hide the Pause menu
        HideGameObject();
        OptionsUI.Instance.ShowUI(ShowGameObject);
        //Open the Option menu
        
    }
    private void OnClickMainMenuButton(){
        Loader_Static.Load(Loader_Static.Scene.MenuScene);
    }
}
