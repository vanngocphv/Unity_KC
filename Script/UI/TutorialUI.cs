using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    //Get all key text in the tutorial screen
    [SerializeField] private TextMeshProUGUI kb_MoveUp;
    [SerializeField] private TextMeshProUGUI kb_MoveDown;
    [SerializeField] private TextMeshProUGUI kb_MoveLeft;
    [SerializeField] private TextMeshProUGUI kb_MoveRight;
    [SerializeField] private TextMeshProUGUI kb_Interact;
    [SerializeField] private TextMeshProUGUI kb_AltInteract;
    //Pause button is set ESC as default value;
    [SerializeField] private TextMeshProUGUI gp_Interact;
    [SerializeField] private TextMeshProUGUI gp_AltInteract;

    //Close Tutorial
    [SerializeField] private Button closeButton;


    private void Awake(){
        closeButton.onClick.AddListener(OnClickCloseButton);
        
    }
    private void Start(){
        //Set keybutton 
        UpdateVisual();

        MainGameManager.Instance.OnStateChange += OnStateChange_Perform;
        GameInputController.Instance.OnRebindingKey += OnRebindingKey_Perform;

        ShowGameObject();

        closeButton.Select();
    }

    private void UpdateVisual(){
        kb_MoveUp.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Up);
        kb_MoveDown.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Down);
        kb_MoveLeft.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Left);
        kb_MoveRight.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Move_Right);
        kb_Interact.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.Interact);
        kb_AltInteract.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.AltInteract);
        gp_Interact.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.GamePad_Interact);
        gp_AltInteract.text = GameInputController.Instance.GetBindingText(GameInputController.BindingKey.GamePad_AltInteract);
    }

    private void OnClickCloseButton(){
        MainGameManager.Instance.isGameStartCountDown = true;
        
    }
    private void OnRebindingKey_Perform(object sender, System.EventArgs e){
        UpdateVisual();
    }

    private void OnStateChange_Perform(object sender, System.EventArgs e){
        if (MainGameManager.Instance.IsCountdownToStartActive()){
            HideGameObject();
        }
    }

    private void ShowGameObject(){
        gameObject.SetActive(true);
    }
    private void HideGameObject(){
        gameObject.SetActive(false);
    }
}
