using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public event EventHandler OnTrashInBin;
    public static EventHandler OnDropToTrashBinSFX;
    new public static void ResetStaticData(){
        OnDropToTrashBinSFX = null;
    }
    public override void Interact(PlayerController playerController)
    {
        if(playerController.CheckKitchenObject()){
            playerController.GetCurrentKitchenObject().DestroySelf();

            OnTrashInBin?.Invoke(this, EventArgs.Empty);
            OnDropToTrashBinSFX?.Invoke(this, EventArgs.Empty);
        }
    }
}
