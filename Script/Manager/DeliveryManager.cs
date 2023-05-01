using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    //Create a singleton for this object, because one game has one delivery manager
    public static DeliveryManager Instance {    get; private set;    }

    //Event when recipe Spawned
    public event EventHandler OnRecipeSpawned;
    //Event when recipe Completed
    public event EventHandler OnRecipeCompleted;

    //Sound completed event
    public event EventHandler OnSucessRecipeSound;
    //Sound failed event
    public event EventHandler OnFailedRecipeSound;

    [SerializeField] private RecipesList_SCAB list_waitingOrderListSCAB;
    //List all recipe for Order from delivery manager, this is using for refer and random choice order
    private List<RecipesOrder_SCAB> list_waitingRecipesOrderSCAB;

    private float recipeSpawnTime;
    private float recipeSpawnTimeMax = 4f;
    private int waitingRecipesMaxInList = 4;
    //successfully recipes delivered amount
    private int successDeliveredRecipeAmount;
    

    void Awake(){
        list_waitingRecipesOrderSCAB = new List<RecipesOrder_SCAB>();
        Instance = this;
    }

    void Start() {
        
    }
    void Update(){
        recipeSpawnTime -= Time.deltaTime;
        if (recipeSpawnTime <= 0f){
            recipeSpawnTime = recipeSpawnTimeMax;
            if (MainGameManager.Instance.IsGamePlaying() && list_waitingRecipesOrderSCAB.Count < waitingRecipesMaxInList){
                //get random recipe
                RecipesOrder_SCAB waitingRecipeSCAB = list_waitingOrderListSCAB.list_recipesOrderSCAB[UnityEngine.Random.Range(0, list_waitingOrderListSCAB.list_recipesOrderSCAB.Count)];
                //add this random into list showup recipe

                list_waitingRecipesOrderSCAB.Add(waitingRecipeSCAB);
                Debug.Log(waitingRecipeSCAB);
                //Fire a event spawn recipe
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject){
        //If delivery, check if this recipe in waiting list recipe order
        for (int i = 0; i < list_waitingRecipesOrderSCAB.Count; i++){
            bool plateContentMatches = true;
            //get one by one list recipe waiting order available
            RecipesOrder_SCAB waitingRecipeSCAB = list_waitingRecipesOrderSCAB[i];
            //Check one by one KitchenObject count = plateKitchenObject has been carry
            if (waitingRecipeSCAB.list_KichenObject_SCAB.Count == plateKitchenObject.GetListKO_SCAB().Count){
                //same carry count kitchen object, check if has same kitchen object inside
                foreach (KitchenObjects_SCAB kitchenObjects_SCAB in waitingRecipeSCAB.list_KichenObject_SCAB){
                    bool hasSameIngredient = false;

                    //for each data in list waiting recipe which has been get from each data in list waiting order
                    foreach(KitchenObjects_SCAB plateKO_SCAB in plateKitchenObject.GetListKO_SCAB()){
                        //check one by one, if has same ingredient, break, if not, return after
                        if (kitchenObjects_SCAB == plateKO_SCAB){
                            hasSameIngredient = true;
                            break;
                        }
                    }

                    if (hasSameIngredient == false){
                        plateContentMatches = false;
                        break;
                    }
                    
                }
                //If plate content delivery matches
                if (plateContentMatches == true){
                    Debug.Log(waitingRecipeSCAB + " is matching");

                    list_waitingRecipesOrderSCAB.RemoveAt(i);
                    //Fire a event Complete recipe
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnSucessRecipeSound?.Invoke(this, EventArgs.Empty);
                    successDeliveredRecipeAmount++;
                    return;
                }
            }
        }
        //if this code still run to this way => plate content didn't match, send fail
        OnFailedRecipeSound?.Invoke(this, EventArgs.Empty);
        Debug.Log("The Plate delivery didn't match with anything in list order");
    }

    public List<RecipesOrder_SCAB> GetListWatingRecipe_SCAB(){
        return list_waitingRecipesOrderSCAB;
    }

    public int GetAmountDeliveredSuccessRecipe(){
        return successDeliveredRecipeAmount;
    }

}
