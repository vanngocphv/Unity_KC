using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarAnimationUI : MonoBehaviour
{
    private const string CONST_IS_WARNING = "isWarning";
    [SerializeField] private StoveCounter stoveCounter;
    private Animator progressBarAnimator;

    private bool isWarning = false;

    private void Awake() {
        progressBarAnimator = GetComponent<Animator>();
    }

    private void Start(){
        stoveCounter.OnProgressBarChangeUI += OnProgressBarChangeUI_Perform;
        progressBarAnimator.SetBool(CONST_IS_WARNING, false);
    }

    private void OnProgressBarChangeUI_Perform(object sender, IF_HasProgress.OnProgressBarChangeUIArgs e){
        isWarning = stoveCounter.isBurning() && e.changeAmount >= 0.5f;
        if (isWarning){
            progressBarAnimator.SetBool(CONST_IS_WARNING, isWarning);
        }
        else{
            progressBarAnimator.SetBool(CONST_IS_WARNING, false);
        }
    }
}
