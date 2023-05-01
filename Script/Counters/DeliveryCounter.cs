using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {get; private set;}
    public event EventHandler OnDeliveryInteract;

    private void Awake(){
        Instance = this;
    }
    public override void Interact(PlayerController playerController)
    {
        //Check if player has object in hand
        if(playerController.CheckKitchenObject()){
            //check if player in hand is plate object
            if (playerController.GetCurrentKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                //check if in plate is true recipe
                //View console log if match with list recipe order
                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
                OnDeliveryInteract?.Invoke(this, EventArgs.Empty);
                
                playerController.GetCurrentKitchenObject().DestroySelf();
            }

        }
    }
}
