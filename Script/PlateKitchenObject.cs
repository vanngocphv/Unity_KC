using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    //this is still kitchent object

    //Event when add new Ingredient
    public event EventHandler<OnAddIngredientHandleArgs> OnAddIngredientHandle;
    //Because complete burger has bread, cabb, tomato, meat, cheese
    public class OnAddIngredientHandleArgs : EventArgs{
        public KitchenObjects_SCAB kitchenObjects_SCAB;
    }

    //Because customer only need sliced/cooked not raw material
    [SerializeField] private List<KitchenObjects_SCAB> list_validKO_SCAB;
    [SerializeField] private FriedRecipe_SCAB friedRecipe_SCAB;

    //list Ingredient when interact with another kitchenObject if in hand laf Plate
    private List<KitchenObjects_SCAB> list_IngredientKO_SCAB;


    //Awake()
    void Awake(){
        list_IngredientKO_SCAB = new List<KitchenObjects_SCAB>();
    }

    //Add more Ingredient in list for list request
    public bool TryAddIngredient(KitchenObjects_SCAB _additionKOIngredient_SCAB){
        //add a new ingredient into list

        //Check if this additionIngredient has existed in list valid KO
        if (!list_validKO_SCAB.Contains(_additionKOIngredient_SCAB)){
            return false;
        }
        //Check if this Ingredient has been existed in this plate
        if (list_IngredientKO_SCAB.Contains(_additionKOIngredient_SCAB)){
            //This Ingredient has been existed
            return false;
        }
        else if (list_IngredientKO_SCAB.Contains(friedRecipe_SCAB.cookedObject) && _additionKOIngredient_SCAB == friedRecipe_SCAB.burnedObject){
            return false;
        }
        else if (list_IngredientKO_SCAB.Contains(friedRecipe_SCAB.burnedObject) && _additionKOIngredient_SCAB == friedRecipe_SCAB.cookedObject){
            return false;
        }
        else {
            list_IngredientKO_SCAB.Add(_additionKOIngredient_SCAB);

            //fire a event, add this add object into this if it hasn't been existed
            OnAddIngredientHandle?.Invoke(this,new OnAddIngredientHandleArgs{
                kitchenObjects_SCAB = _additionKOIngredient_SCAB
            });
            return true;
        }
    }

    public List<KitchenObjects_SCAB> GetListKO_SCAB(){
        return list_IngredientKO_SCAB;
    }
}
