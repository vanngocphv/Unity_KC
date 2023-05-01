using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryStatusUI : MonoBehaviour
{
    private const string CONST_DELIVERY_SUCESS = "Delivery\nSuccess";
    private const string CONST_DELIVERY_FAILED = "Delivery\nFailed";
    private const string CONST_IS_POPUP_DELIVERY = "isPopupDelivery";
    [SerializeField] private Sprite successImage;
    [SerializeField] private Sprite FailedImage;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private TextMeshProUGUI deliveryMessage;
    [SerializeField] private Image deliveryBackground;
    [SerializeField] private Image deliveryStatus;

    private Animator deliveryAnimtor;

    private void Awake(){
        deliveryAnimtor = GetComponent<Animator>();
    }

    private void Start(){
        Hide();
        //user send a plate into delivery
        
        DeliveryManager.Instance.OnFailedRecipeSound += OnFailedRecipe_Perform;
        DeliveryManager.Instance.OnSucessRecipeSound += OnSucessRecipe_Perform;
        
    }

    private void Update(){
        
    }

    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }

    private void OnFailedRecipe_Perform(object sender, System.EventArgs e){

        deliveryBackground.color = failedColor;
        deliveryMessage.text = CONST_DELIVERY_FAILED;
        deliveryStatus.sprite = FailedImage;
        deliveryAnimtor.SetTrigger(CONST_IS_POPUP_DELIVERY);

        Show();
    }

    private void OnSucessRecipe_Perform(object sender, System.EventArgs e){
        deliveryBackground.color = successColor;
        deliveryMessage.text = CONST_DELIVERY_SUCESS;
        deliveryStatus.sprite = successImage;
        deliveryAnimtor.SetTrigger(CONST_IS_POPUP_DELIVERY);
        Show();
    }
    
}
