using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IF_HasProgress
{
    //Event handle for cutting counter progress
    public event EventHandler<IF_HasProgress.OnProgressBarChangeUIArgs> OnProgressBarChangeUI;
    public event EventHandler OnCutAnimation;

    //Event Chop/Cutting sound effect
    public static event EventHandler OnCuttingSoundEffect;

    //reset static event
    new public static void ResetStaticData(){
        OnCuttingSoundEffect = null;
    }


    [SerializeField] private CuttingRecipe_SCAB[] cuttingKitchenObjectSCAB_Array;

    private int counterCuttingProgress;         //counter cutting interact, if 100%, this cutting will be completed
    public override void Interact(PlayerController playerController)
    {
        //This cutting counter doesn't have kitchen object
        if (!this.CheckKitchenObject()){
            //Check if this kitchen object has cutting version
            if (playerController.CheckKitchenObject() && HasRecipeCutting(playerController.GetCurrentKitchenObject())){
                playerController.GetCurrentKitchenObject().SetKitchenObjectParent(this);
                counterCuttingProgress = 0;

                //First start firing event
                CuttingRecipe_SCAB cuttingRecipe_SCAB = currentCuttingScriptObject_SCAB(GetCurrentKitchenObject().GetKitchenObjects_SCAB());//this value only use for get max Counter
                OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                    changeAmount = (float)counterCuttingProgress / cuttingRecipe_SCAB.cuttingProgressMaxCounter
                });
            }
            else{
                
            }
        }
        //This cutting counter has kitchen object
        else {
            if (!playerController.CheckKitchenObject()){
                //Player doesn't carry any kitchen object
                //Give current counter object to player
                //After finish animation cutting
                OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                    changeAmount = 0
                });
                this.GetCurrentKitchenObject().SetKitchenObjectParent(playerController);
            }
            else{
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
            }
        }
    }

    public override void InteractAlternate(PlayerController playerController)
    {
        if (this.CheckKitchenObject() && HasRecipeCutting(this.GetCurrentKitchenObject())){
            //Has kitchenObject, perform a action and change prefabs

            //For each InteractAlternate, the counterCuttingProgress will be up 1
            counterCuttingProgress++;

            OnCutAnimation?.Invoke(this,EventArgs.Empty);
            //Get cuttingKitchenObject_SCAB
            CuttingRecipe_SCAB cuttingKitchenObjectSCAB = currentCuttingScriptObject_SCAB(this.GetCurrentKitchenObject().GetKitchenObjects_SCAB());
            if (counterCuttingProgress >= cuttingKitchenObjectSCAB.cuttingProgressMaxCounter){
                //intial, got cutting objectSCAB
                KitchenObjects_SCAB outputSlice_SCAB = cuttingKitchenObjectSCAB.cuttingObject;
                //First, destroy current raw object
                this.GetCurrentKitchenObject().DestroySelf();
                //Spawn the cutting object which the objest replace the raw object
                KitchenObject.SpawnKitchenObject(outputSlice_SCAB, this);
            }
            else{
                
            }
            OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                changeAmount = (float) counterCuttingProgress / cuttingKitchenObjectSCAB.cuttingProgressMaxCounter
            });
            //On cutting sound effect
            OnCuttingSoundEffect?.Invoke(this, EventArgs.Empty);
            
        }
        else{
            //No object or no recipe
        }
    }

    private KitchenObjects_SCAB GetOutputFromInputSCAB(KitchenObjects_SCAB _inputKitchenObject_SCAB){
        CuttingRecipe_SCAB cuttingKitchenObjectSCAB = currentCuttingScriptObject_SCAB(_inputKitchenObject_SCAB);
        if (cuttingKitchenObjectSCAB != null){
            return cuttingKitchenObjectSCAB.cuttingObject;
        }
        return null;
    }

    private bool HasRecipeCutting(KitchenObject _inputKitchenObject){
        return GetOutputFromInputSCAB(_inputKitchenObject.GetKitchenObjects_SCAB()) != null;
    }

    private CuttingRecipe_SCAB currentCuttingScriptObject_SCAB(KitchenObjects_SCAB _inputObject_SCAB){
        foreach (CuttingRecipe_SCAB each in cuttingKitchenObjectSCAB_Array){
            if (each.rawObject == _inputObject_SCAB){
                return each;
            }
        }
        return null;
    }
}