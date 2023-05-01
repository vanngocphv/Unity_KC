using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterVisual : MonoBehaviour
{
    private const string CONST_ISDELETING = "isDeleting";
    [SerializeField] private TrashCounter trashCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start(){
        trashCounter.OnTrashInBin += OnTrashInBin_Performed;
    }

    private void OnTrashInBin_Performed(object sender, System.EventArgs e){
        animator.SetTrigger(CONST_ISDELETING);
    }
}
