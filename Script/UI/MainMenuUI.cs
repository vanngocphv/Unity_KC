using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;

    [SerializeField] private Button quitButton;


    //Seting event for the above buttons;
    private void Awake() {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        //Set default selected
        playButton.Select();
        //reset timescale
        Time.timeScale = 1f;
    }

    private void OnPlayButtonClicked(){
        //Change to gameplay scene
        Loader_Static.Load(Loader_Static.Scene.GameScene);
        
    }

    private void OnQuitButtonClicked(){
        Application.Quit();
    }
}
