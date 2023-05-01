using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string CONST_ISINTERACT = "isInteract";
    [SerializeField] private ContainerCounter mainCounter;
    private Animator animator;

    void Start(){
        mainCounter.OnPlayerGrabbedObject += OnPlayerGrabbedObject_fired;
        animator = GetComponent<Animator>();
    }

    private void OnPlayerGrabbedObject_fired(object sender, System.EventArgs e){
        animator.SetTrigger(CONST_ISINTERACT);
    }
}
