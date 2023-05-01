using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    //Refer TextMeshPro
    [SerializeField] private TextMeshProUGUI recipeTextName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake(){
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeTextSCAB(RecipesOrder_SCAB recipesOrder_SCAB){
        recipeTextName.text = recipesOrder_SCAB.recipeName;

        //clear up container
        foreach (Transform child in iconContainer){
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        //set all icon of recipes
        foreach (KitchenObjects_SCAB kitchenObjects_SCAB in recipesOrder_SCAB.list_KichenObject_SCAB){
            Transform spriteIconTransform = Instantiate(iconTemplate, iconContainer);
            spriteIconTransform.GetComponent<Image>().sprite = kitchenObjects_SCAB.sprite;
            spriteIconTransform.gameObject.SetActive(true);
        }
    }
}
