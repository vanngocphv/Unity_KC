using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    //One game has one MainManager
    public static MainGameManager Instance{get; private set;}

    //Event when state change
    public event EventHandler OnStateChange;

    //Event ưhen pause game
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;


    //Create a state of game current be in
    public enum State{
        WaitingToStart,         //initial loading
        CountdownToStarting,    //running
        Playing,               //Starting
        GameOver,               //Over
    }

    //state variable, for the game
    private State currentState;

    //Waiting to start time count
    [SerializeField] private float coundownToStartTimer = 3f;
    [SerializeField] private float gameStartingTimerMax = 10f;
    private float gameStartingTimer = 0f;

    private bool isGamePause = false;

    public bool isGameStartCountDown = false;

    private void Awake(){
        Instance = this;
        //Alway set the state is waiting to start when the game start, the screen loading
        currentState = State.WaitingToStart;

    }

    private void Start(){
        GameInputController.Instance.OnPauseAction += OnPauseAction_Perform;
    }

    private void Update(){
        //create a switch case for checking state
        switch (currentState){
            case (State.WaitingToStart):
            ///     Waiting To starting     ///
                if ( isGameStartCountDown ){
                    currentState = State.CountdownToStarting;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            ///     End To starting         ///

            case (State.CountdownToStarting):
            ///     Countdown To Starting   ///
                coundownToStartTimer -= Time.deltaTime;
                if (coundownToStartTimer < 0f){
                    currentState = State.Playing;
                    gameStartingTimer = gameStartingTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);

                }
                break;
            ///     End Countdown to start  ///

            case (State.Playing):
            ///     starting                ///
                gameStartingTimer -= Time.deltaTime;
                if (gameStartingTimer < 0f){
                    currentState = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);

                }
                break;
            ///     End Starting            ///

            case (State.GameOver):
            ///     Game Over               ///
                //OnStateChange?.Invoke(this, EventArgs.Empty);
                break;
            ///     End Game Over           ///
        }
    }

    //Check if the game can in play
    public bool IsGamePlaying(){
        return currentState == State.Playing;
    }

    //Check if game still waiting to cooldown
    public bool IsWaitingToCountDown(){
        return currentState == State.WaitingToStart;
    }

    public bool IsCountdownToStartActive(){
        return currentState == State.CountdownToStarting;
    }

    public bool IsGameOVer(){
        return currentState == State.GameOver;
    }

    public float GetCountdownToStartTimer(){
        return coundownToStartTimer;
    }

    public float GetCurrentPlayingTimer(){
        return 1 - (gameStartingTimer / gameStartingTimerMax);
    }

    public void TogglePauseGame(){
        //set isPause = !isPaus, if true = false => false = true
        isGamePause = !isGamePause;
        if (isGamePause){
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else{
            Time.timeScale = 1f;
            OnGameUnpause?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnPauseAction_Perform(object sender, System.EventArgs e){
        //Event logic, handle pause logic
        TogglePauseGame();

    }
}
