using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IF_HasProgress
{
    //Event when start
    public event EventHandler<IF_HasProgress.OnProgressBarChangeUIArgs> OnProgressBarChangeUI;
    public event EventHandler<OnStoveInStateArgs> OnStoveInState;
    public class OnStoveInStateArgs: EventArgs{
        public StoveCounter.State stateInChange;
    }


    //State machine enum
    public enum State{
        Idle,           //Nothing in top
        Frying,         //is cooking
        Fried,          //cooking finished
        Burned,         //burned
    }


    [SerializeField] private FiringRecipe_SCAB[] array_FiringRecipeSCAB;    //array of Frying recipe
    [SerializeField] private FriedRecipe_SCAB[] array_FriedRecipeSCAB;      //array of Fried recipe

    private float maxTimeFrying;
    private float maxBurningTime;       //the time when fried state started
    //State of object in stove
    private State currentState; //current state;
    private FiringRecipe_SCAB fryingKitchenObjectSCAB;      //the object frying
    private FriedRecipe_SCAB friedKitchenObjectSCAB;        //the object fried 

    void Start(){
        currentState = State.Idle;      // initial declare, the current state always set idle when start the first of frame
    }

    void Update(){
        if(CheckKitchenObject()){
            //State change
            switch (currentState){
                case State.Idle:        //in idle state, what stove do?
                    break;
                case State.Frying:      //in Frying state, what stove do?
                //// case Frying logic ////
                    //Object alway has firingrecipe
                    maxTimeFrying += Time.deltaTime;
                    if (maxTimeFrying > fryingKitchenObjectSCAB.maxTimeCooked){
                        //Need set 100% bar
                        OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                            changeAmount = maxTimeFrying / fryingKitchenObjectSCAB.maxTimeCooked
                        });
                        //Fried, this object has been cooked finished

                        GetCurrentKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingKitchenObjectSCAB.cookedObject,this);

                        //Change state
                        maxBurningTime = 0f;
                        currentState = State.Fried;
                        //Change event
                        OnStoveInState?.Invoke(this, new OnStoveInStateArgs {
                            stateInChange = currentState
                            });
                        }
                    else{
                        OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                            changeAmount = maxTimeFrying / fryingKitchenObjectSCAB.maxTimeCooked
                        });
                    }
                    break;
                //// End case Frying   ////

                case State.Fried:       //in Fried state, what stove do?
                //// case Frying logic ////
                    //Object alway has firingrecipe
                    maxBurningTime += Time.deltaTime;
                    if (maxBurningTime > friedKitchenObjectSCAB.maxTimeBurned){
                        //Fried, this object has been cooked finished

                        GetCurrentKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(friedKitchenObjectSCAB.burnedObject,this);

                        //Change state
                        currentState = State.Burned;
                        //Turn off Stove
                        OnStoveInState?.Invoke(this, new OnStoveInStateArgs {
                            stateInChange = currentState
                        });

                        //Set bar down to 0
                        OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                        changeAmount = 0
                        });
                    }
                    OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                        changeAmount = maxBurningTime / friedKitchenObjectSCAB.maxTimeBurned
                    });
                    break;
                //// End case Frying   ////

                case State.Burned:      //in Burned state, what stove do?
                    break;
            }
        }

    }
    //interact function
    public override void Interact(PlayerController playerController)
    {
        //This cutting counter doesn't have kitchen object
        if (!this.CheckKitchenObject()){
            //Check if this kitchen object has frying version
            if (playerController.CheckKitchenObject() && HasRecipeFiring(playerController.GetCurrentKitchenObject().GetKitchenObjects_SCAB())){
                
                playerController.GetCurrentKitchenObject().SetKitchenObjectParent(this);

                this.fryingKitchenObjectSCAB = GetFiringRecipe_SCAB(GetCurrentKitchenObject().GetKitchenObjects_SCAB());
                this.friedKitchenObjectSCAB = GetFriedRecipe_SCAB(fryingKitchenObjectSCAB.cookedObject);


                //First start firing state
                //The state set is Frying
                currentState = State.Frying;
                maxTimeFrying = 0f;         //reset frying time to 0, start in next frame

                //Turn on Stove
                OnStoveInState?.Invoke(this, new OnStoveInStateArgs {
                    stateInChange = currentState
                });
                //Start cooked
                OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                        changeAmount = maxTimeFrying / fryingKitchenObjectSCAB.maxTimeCooked
                });
            }
            else{
                
            }
        }
        //This frying counter has kitchen object
        else {
            if (!playerController.CheckKitchenObject()){
                //Player doesn't carry any kitchen object
                //Give current counter object to player
                //After finish animation frying
                
                this.GetCurrentKitchenObject().SetKitchenObjectParent(playerController);

                //Reset state after user pickup this
                currentState = State.Idle;
                //Turn off Stove
                OnStoveInState?.Invoke(this, new OnStoveInStateArgs {
                    stateInChange = currentState
                });

                //Stop Cooked
                OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                        changeAmount = 0
                });
                
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

                        //Reset state after user pickup this
                        currentState = State.Idle;
                        //Turn off Stove
                        OnStoveInState?.Invoke(this, new OnStoveInStateArgs {
                            stateInChange = currentState
                        });
                        //Stop Cooked
                        OnProgressBarChangeUI?.Invoke(this, new IF_HasProgress.OnProgressBarChangeUIArgs {
                                changeAmount = 0
                        });
                    };

                }
            }
        }
    }

    private bool HasRecipeFiring(KitchenObjects_SCAB _inputKitchenObjectSCAB){
        //Check if this object has recipe for frying
        return GetFiringRecipe_SCAB(_inputKitchenObjectSCAB) != null;
    }

    private FiringRecipe_SCAB GetFiringRecipe_SCAB(KitchenObjects_SCAB _inputKitchenObjectSCAB){
        //Get recipe for frying in Serializefield FiringRecipe_SCAB
        foreach (FiringRecipe_SCAB rawObject in array_FiringRecipeSCAB){
            if (rawObject.rawObject == _inputKitchenObjectSCAB){
                return rawObject;
            }
        }
        return null;
    }

    private KitchenObjects_SCAB GetCookedObjectFromRawObject(KitchenObjects_SCAB _inputKitchenObjectSCAB){
        //Get Cooked Object SCAB from input as raw Object
        KitchenObjects_SCAB cookedObject = GetFiringRecipe_SCAB(_inputKitchenObjectSCAB).cookedObject;
        return cookedObject;
    }

    private FriedRecipe_SCAB GetFriedRecipe_SCAB(KitchenObjects_SCAB _inputKitchenObjectSCAB){
        //From cooked object, get friedRecipe_SCAB if available
        foreach (FriedRecipe_SCAB recipe_SCAB in array_FriedRecipeSCAB){
            if (recipe_SCAB.cookedObject == _inputKitchenObjectSCAB){
                return recipe_SCAB;
            }
        }
        return null;
    }

    private KitchenObjects_SCAB GetBurnedKitchenObject_SCAB(KitchenObjects_SCAB _cookedKitchenObjectSCAB){
        //Get burned object from input as cooked object
        KitchenObjects_SCAB burnedObject = GetFriedRecipe_SCAB(_cookedKitchenObjectSCAB).burnedObject;
        return burnedObject;
    }

    public bool isBurning(){
        return currentState == State.Fried;
    }
}
