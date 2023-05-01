using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingKnifeVisual : MonoBehaviour
{
    private const string CONST_ISCUTTING = "isCutting";
    [SerializeField] private CuttingCounter cuttingCounter;
    
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }
    void Start(){
        //when this progress bar has been active or first frame, it will set event for cutting counter
        cuttingCounter.OnCutAnimation += OnCutAnimation_Performed;
    }

    private void OnCutAnimation_Performed(object sender, System.EventArgs e){
        animator.SetTrigger(CONST_ISCUTTING);
    }

}
