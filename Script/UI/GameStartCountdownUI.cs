using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string CONST_TRIGGER_NUMBER_POPUP = "triggerNumberPopup";
    //reference to text mesh pro object
    [SerializeField] private TextMeshProUGUI countDownTextNumber;
    [SerializeField] private Animator animator;

    private int previousNumber = -1;

    void Awake(){
        animator = GetComponent<Animator>();
    }
    private void Start() {
        MainGameManager.Instance.OnStateChange += OnStateChange_Perfom;

        HideCountdown();
    }

    private void Update(){
        int currentNumber = Mathf.CeilToInt(MainGameManager.Instance.GetCountdownToStartTimer());
        //Check if the countdown current different with the previous number, if true, set trigger animation run
        if (currentNumber != previousNumber){
            previousNumber = currentNumber;
            animator.SetTrigger(CONST_TRIGGER_NUMBER_POPUP);
            SoundEffectManager.Instance.PlayCountDownSFX();
        }
        countDownTextNumber.text = currentNumber.ToString();
    }
    private void OnStateChange_Perfom(object sender, System.EventArgs e){
        if (MainGameManager.Instance.IsCountdownToStartActive()){
            //show countdown to start
            ShowCountdown();
        }
        else{
            //hide countdown to start
            HideCountdown();
        }
    }

    private void ShowCountdown(){
        //Show the child gameobject
        gameObject.SetActive(true);
    }

    private void HideCountdown(){
        //Hide the child gameobject
        gameObject.SetActive(false);
    }
}
