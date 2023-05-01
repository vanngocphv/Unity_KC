using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IF_KitchenObjectParent
{
    //kitchen Scriptable object

    private void Update(){
        
    }
    public override void Interact(PlayerController playerController){
        //Clear counter has object
        if (CheckKitchenObject())
        {
            if (playerController.CheckKitchenObject()){
                //Player carry something, check if it is plate
                if (playerController.GetCurrentKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //Player carries a plate in hand
                    //and then, add this kitchen in clear counter into Ingredient list
                    //If add success, destroy prefab after then
                    if (plateKitchenObject.TryAddIngredient(GetCurrentKitchenObject().GetKitchenObjects_SCAB()) ){
                        //destroy the current in counter kitchen object
                        GetCurrentKitchenObject().DestroySelf();
                    };

                }
                else{
                    //in the clear counter if has plate above
                    if (GetCurrentKitchenObject().TryGetPlate(out plateKitchenObject)){
                        //try add ingredient into this above;
                        if (plateKitchenObject.TryAddIngredient(playerController.GetCurrentKitchenObject().GetKitchenObjects_SCAB()) ){
                            //destroy the current in counter kitchen object
                            playerController.GetCurrentKitchenObject().DestroySelf();
                        };
                    }
                    else{
                        Debug.Log("Your hand still carry another kitchen object");
                    }
                }
            }
            else{
                //Player doesn't carry anything
                this.GetCurrentKitchenObject().GetComponent<KitchenObject>().SetKitchenObjectParent(playerController);
            }
        }
        //Clear counter doesn't has any object
        else {
            //If player has kitchen object in hand, leave it to clear counter
            if (playerController.CheckKitchenObject()){
                //Player carry something
                playerController.GetCurrentKitchenObject().SetKitchenObjectParent(this);

            }
            else{
                //Player doesn't carry anything
            }
        }
    }
}
