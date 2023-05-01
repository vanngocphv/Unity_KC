using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    private const string CONST_SOUND_EFFECT = "SOUND EFFECT: ";
    private const string CONST_BGM_SOUND = "BGM SOUND: ";
    [SerializeField] private Button soundEffectSFXButton;
    [SerializeField] private Button musicBGMButton;
    [SerializeField] private Button backButton;
    //Controller/movement button
    [SerializeField] private Button moveUpButton;               //Up
    [SerializeField] private Button moveDownButton;             //Down
    [SerializeField] private Button moveLeftButton;             //Left
    [SerializeField] private Button moveRightButton;            //Right
    [SerializeField] private Button interactButton;             //Interact
    [SerializeField] private Button altInteractButton;          //Interact Alternate
    [SerializeField] private Button interactGamePad;            //Interact
    [SerializeField] private Button altInteractGamePad;         //Interact Alternate
    //Text Mesh Pro
    [SerializeField] private TextMeshProUGUI soundSFXText;
    [SerializeField] private TextMeshProUGUI musicBGMText;
    // Text of the button setting
    [SerializeField] private TextMeshProUGUI moveUpText;        //Up
    [SerializeField] private TextMeshProUGUI moveDownText;      //Down
    [SerializeField] private TextMeshProUGUI moveLeftText;      //Left
    [SerializeField] private TextMeshProUGUI moveRightText;     //Right
    [SerializeField] private TextMeshProUGUI interactText;      //Interact
    [SerializeField] private TextMeshProUGUI altInteractText;   //Interact Alternate
    [SerializeField] private TextMeshProUGUI interactGPText;    //Interact
    [SerializeField] private TextMeshProUGUI altInteractGPText; //Interact Alternate

    //Setting Key UI
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action OnCloseAction;

    private void Awake(){
        Instance = this;
        soundEffectSFXButton.onClick.AddListener(OnClickSoundEffectSFXButton);
        musicBGMButton.onClick.AddListener(OnClickMusicBGMButton);
        backButton.onClick.AddListener(OnClickBackButton);

        moveUpButton.onClick.AddListener(OnClickMoveUpButton);
        moveDownButton.onClick.AddListener(OnClickMoveDownButton);
        moveLeftButton.onClick.AddListener(OnClickMoveLeftButton);
        moveRightButton.onClick.AddListener(OnClickMoveRightButton);
        interactButton.onClick.AddListener(OnClickInteractButton);
        altInteractButton.onClick.AddListener(OnClickInteractAltButton);
        interactGamePad.onClick.AddListener(OnClickInteractGamePad);
        altInteractGamePad.onClick.AddListener(OnClickInteractAltGamePad);

    }
    private void Start(){
        MainGameManager.Instance.OnGameUnpause += OnGameUnpauseClicked_Perform;
        //When this option has been called
        UpdateVisualUI();

        HidePressToRebindObject();
        HideGameObject();
    }



    private void ShowGameObject(Action _OnCloseAction){
        gameObject.SetActive(true);
        this.OnCloseAction = _OnCloseAction;

        //Set default selected
        soundEffectSFXButton.Select();
    }
    public void ShowUI(Action _OnCloseAction){
        ShowGameObject(_OnCloseAction);
    }
    private void HideGameObject(){
        gameObject.SetActive(false);
    }
    public void HideUI(){
        HideGameObject();
    }

    //show press to rebind scene
    private void ShowPressToRebindObject(){
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    //hide press to rebind scene
    private void HidePressToRebindObject(){
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void OnClickSoundEffectSFXButton(){
        //Change volume off sound, affect to SoundManager
        SoundEffectManager.Instance.ChangeVolumeSFX();
        //Then, update the visual text
        UpdateVisualUI();
    }

    private void OnClickMusicBGMButton(){
        //Change volume off sound, affect to SoundManager
        MusicBGMManager.Instance.ChangeVolumeBGM();
        //Then, update the visual text
        UpdateVisualUI();
    }

    private void OnClickBackButton(){
        HideGameObject();
        this.OnCloseAction();
    }

    private void OnGameUnpauseClicked_Perform(object sender, System.EventArgs e){
        HideGameObject();
    }

    //Rebinding key
    private void OnClickMoveUpButton(){
        RebindBindingKey(GameInputController.BindingKey.Move_Up);
    }
    private void OnClickMoveDownButton(){
        RebindBindingKey(GameInputController.BindingKey.Move_Down);
    }
    private void OnClickMoveLeftButton(){
        RebindBindingKey(GameInputController.BindingKey.Move_Left);
    }
    private void OnClickMoveRightButton(){
        RebindBindingKey(GameInputController.BindingKey.Move_Right);
    }
    private void OnClickInteractButton(){
        RebindBindingKey(GameInputController.BindingKey.Interact);
    }
    private void OnClickInteractAltButton(){
        RebindBindingKey(GameInputController.BindingKey.AltInteract);
    }
    private void OnClickInteractGamePad(){
        RebindBindingKey(GameInputController.BindingKey.GamePad_Interact);
    }
    private void OnClickInteractAltGamePad(){
        RebindBindingKey(GameInputController.BindingKey.GamePad_AltInteract);
    }


    private void UpdateVisualUI(){
        //when the code apply change for value of the volume, change the UI output
        soundSFXText.text = CONST_SOUND_EFFECT + Mathf.Round(SoundEffectManager.Instance.GetCurrentVolumeSFX() * 10f);
        musicBGMText.text = CONST_BGM_SOUND + Mathf.Round(MusicBGMManager.Instance.GetCurrentVolumeBGM() * 10f);

        moveUpText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Up);
        moveDownText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Down);
        moveLeftText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Left);
        moveRightText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Right);
        interactText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Interact);
        altInteractText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.AltInteract);
        interactGPText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.GamePad_Interact);
        altInteractGPText.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.GamePad_AltInteract);
    }

    private void RebindBindingKey(GameInputController.BindingKey bindingKey){
        ShowPressToRebindObject();
        //Send to a key need binding, and close the pop up which will ask the key you want to change to
        GameInputController.Instance.RebindBindingKey(bindingKey, () => {
            HidePressToRebindObject();
            UpdateVisualUI();
            });

    }
}
