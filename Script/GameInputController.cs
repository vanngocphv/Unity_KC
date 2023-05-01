using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputController : MonoBehaviour
{
    public static GameInputController Instance{ get; private set;}

    private const string CONST_JSON_PLAYER_INPUT_SETTING = "Player-control-setting";
    public event EventHandler OnInteractAction;  //Event for interact from user
    public event EventHandler OnInteractAlternateAction;  //Event for interact from user

    public event EventHandler OnRebindingKey;

    //Enum for refer to input 
    public enum BindingKey{
        Move_Up,            //Up
        Move_Down,          //Down
        Move_Left,          //Left
        Move_Right,         //Right
        Interact,           //Interact
        AltInteract,        //Interact Alternate
        GamePad_Interact,   //Game pad interact
        GamePad_AltInteract,//Game pad interact alternate
    }

    InputManager inputActions;

    public event EventHandler OnPauseAction;

    void Awake(){
        Instance = this;
        inputActions = new InputManager();
        //Check if the player Prefs has the related key
        if (PlayerPrefs.HasKey(CONST_JSON_PLAYER_INPUT_SETTING)){
            //Load this data as json data into player input action
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(CONST_JSON_PLAYER_INPUT_SETTING));
        }

        inputActions.Movement.Interact.performed += InteractPerform;
        inputActions.Movement.InteractAlternate.performed += InteractAltPerform;
        inputActions.Movement.Pause.performed += InteractPausePerform;

        
        
    }

    //Manually destroy all event which has been subscribe
    void OnDestroy() {
        inputActions.Movement.Interact.performed -= InteractPerform;
        inputActions.Movement.InteractAlternate.performed -= InteractAltPerform;
        inputActions.Movement.Pause.performed -= InteractPausePerform;

        inputActions.Dispose();
    }
    public Vector2 InputNormalizedVector(){
        //Vector2 inputValue = new Vector2(0, 0);
        // //Up
        // if (Input.GetKey(KeyCode.W)){
        //     inputValue.y = +1;
        // }
        // //Down
        // if (Input.GetKey(KeyCode.S)){
        //     inputValue.y = -1;
        // }
        // //Left
        // if (Input.GetKey(KeyCode.A)){
        //     inputValue.x = -1;
        // }
        // //Right
        // if (Input.GetKey(KeyCode.D)){
        //     inputValue.x = +1;
        // }
        //Handle this Vector2 -> direction vector in normalize
        //inputValue = inputValue.normalized;
        
        // This line code will be return a Vector2 value in normalize vector2
        Vector2 inputValue = inputActions.Movement.Walking.ReadValue<Vector2>();

        return inputValue;
    }
    //Firing a event if button interact has been performed
    private void InteractPerform(InputAction.CallbackContext _ctx){
        //Call event, (sender object, target function event)
        //When event not null, this event will be called with exactly same parameter input in behind
        OnInteractAction?.Invoke(this, EventArgs.Empty);

    }
    
    //Firing a event if button alternate has been performed
    private void InteractAltPerform(InputAction.CallbackContext _ctx){
        //Call event, (sender object, target function event)
        //When event not null, this event will be called with exactly same parameter input in behind
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    }

    private void InteractPausePerform(InputAction.CallbackContext _ctx){
        Debug.Log("Esc interact");
        //fire a event
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public string GetBindingText(BindingKey bindingKey){
        string returnText = "";
        switch (bindingKey){
            case (BindingKey.Move_Up):
                returnText = inputActions.Movement.Walking.bindings[1].ToDisplayString();
                break;
            case (BindingKey.Move_Down):
                returnText = inputActions.Movement.Walking.bindings[2].ToDisplayString();
                break;
            case (BindingKey.Move_Left):
                returnText = inputActions.Movement.Walking.bindings[3].ToDisplayString();
                break;
            case (BindingKey.Move_Right):
                returnText = inputActions.Movement.Walking.bindings[4].ToDisplayString();
                break;
            case (BindingKey.Interact):
                returnText = inputActions.Movement.Interact.bindings[0].ToDisplayString();
                break;
            case (BindingKey.AltInteract):
                returnText = inputActions.Movement.InteractAlternate.bindings[0].ToDisplayString();
                break;
            case (BindingKey.GamePad_Interact):
                returnText = inputActions.Movement.Interact.bindings[1].ToDisplayString();
                break;
            case (BindingKey.GamePad_AltInteract):
                returnText = inputActions.Movement.InteractAlternate.bindings[1].ToDisplayString();
                break;
        }
        return returnText;
    }

    public void RebindBindingKey(BindingKey bindKey, Action onActionRebound){
        //First Disable input action map
        inputActions.Movement.Disable();

        //Get index binding key;
        InputAction inputAction_tmp = new InputAction();
        int bindingIndex = -1;
        switch (bindKey){
            case (BindingKey.Move_Up):
                inputAction_tmp = inputActions.Movement.Walking;
                bindingIndex = 1;
                break;
            case (BindingKey.Move_Down):
                inputAction_tmp = inputActions.Movement.Walking;
                bindingIndex = 2;
                break;
            case (BindingKey.Move_Left):
                inputAction_tmp = inputActions.Movement.Walking;
                bindingIndex = 3;
                break;
            case (BindingKey.Move_Right):
                inputAction_tmp = inputActions.Movement.Walking;
                bindingIndex = 4;
                break; 
            case (BindingKey.Interact):
                inputAction_tmp = inputActions.Movement.Interact;
                bindingIndex = 0;
                break; 
            case (BindingKey.AltInteract):
                inputAction_tmp = inputActions.Movement.InteractAlternate;
                bindingIndex = 0;
                break;  
        }

        //Rebinding again with PerformInteractiveRebinding
        inputAction_tmp.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                inputActions.Movement.Enable();
                onActionRebound();                               //Callback the event;

                PlayerPrefs.SetString(CONST_JSON_PLAYER_INPUT_SETTING, inputActions.SaveBindingOverridesAsJson());   
                PlayerPrefs.Save(); 
                OnRebindingKey?.Invoke(this, EventArgs.Empty);                               
            }).Start();

    }

    void OnEnable(){
        inputActions.Movement.Enable();
    }
    void OnDisable() {
        inputActions.Movement.Disable();
    }
}
