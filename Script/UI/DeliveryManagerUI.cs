using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    //location of container for this Delivery UI
    [SerializeField] private Transform containerUI;
    //Recipe Template UI, for recycles and add new Recipe
    [SerializeField] private Transform recipeTempUI;


    private void Awake() {
        //hide recipeTempUI when the game start
        recipeTempUI.gameObject.SetActive(false);
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned_Perform;
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted_Perform;

        UpdateVisual();
    }

    private void UpdateVisual(){
        //First, Destroy every recipeTempUI has exist in containerUI
        foreach (Transform childUI in containerUI){
            if (childUI == recipeTempUI) continue;
            
            Destroy(childUI.gameObject);
        }

        //Next, recheck the list Recipe and add new and then active it
        foreach (RecipesOrder_SCAB recipesOrder_SCAB in DeliveryManager.Instance.GetListWatingRecipe_SCAB()){
            //Spawn recipe
            //Debug.Log(recipesOrder_SCAB);
            Transform recipeOrderTransform =  Instantiate(recipeTempUI, containerUI);
            recipeOrderTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeTextSCAB(recipesOrder_SCAB);
            recipeOrderTransform.gameObject.SetActive(true);
            
        };
    }

    private void OnRecipeSpawned_Perform(object sender, System.EventArgs e){
        UpdateVisual();
    }
    private void OnRecipeCompleted_Perform(object sender, System.EventArgs e){
        UpdateVisual();
    }
}
