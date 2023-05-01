using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    //because this script in a game object which is child of the plateKitchenObject
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    //Duplicate UI
    [SerializeField] private Transform iconUITemp;

    //Because this plateKitchen when add new ingredent alway fire a event, just listen this event

    void Awake(){
        iconUITemp.gameObject.SetActive(false);
    }

    void Start(){
        plateKitchenObject.OnAddIngredientHandle += OnAddIngredientIconUI_Perform;

    }

    private void OnAddIngredientIconUI_Perform(object sender, PlateKitchenObject.OnAddIngredientHandleArgs e){
        UpdateVisualIcon();
    }

    private void UpdateVisualIcon(){
        //Clear all UI icon in parent object
        foreach (Transform childObject in transform){
            if (childObject == iconUITemp) continue; //skip if same with template icon child
            Destroy(childObject.gameObject);
        }

        //Spawn all UI icon
        foreach (KitchenObjects_SCAB kitchenObjects_SCAB in plateKitchenObject.GetListKO_SCAB()){
            Transform iconTransfom = Instantiate(iconUITemp, transform); //alway spawn as child of this object
            iconTransfom.gameObject.SetActive(true);    //active show icon in group layout
            iconTransfom.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjects_SCAB);
        }
    }

}
