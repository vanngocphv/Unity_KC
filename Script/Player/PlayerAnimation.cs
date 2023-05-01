using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string CONST_ISWALKING = "isWalking";

    [SerializeField]
    private GameObject playerController;
    private Animator anim;
    private bool isWalking;
    void Awake(){
        isWalking = playerController.GetComponent<PlayerController>().isWalking;
        anim = GetComponent<Animator>();
        anim.SetBool(CONST_ISWALKING, isWalking);
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = playerController.GetComponent<PlayerController>().isWalking;
        anim.SetBool(CONST_ISWALKING, isWalking);
    }
}
