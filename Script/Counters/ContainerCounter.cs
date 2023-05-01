using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IF_KitchenObjectParent
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjects_SCAB kitchenObjects;

    // When player using button E for interact with this object
    public override void Interact(PlayerController playerController){
        //Spawn Object
        //Set spawn object into top up this counter
        if (!playerController.CheckKitchenObject()) {
            KitchenObject.SpawnKitchenObject(kitchenObjects, playerController);
            //Fire a event when player grab for listener has known and called animation
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else {
            Debug.Log("Your hand still has object, drop or put in some clear counter plz!");
        }

    }

}
