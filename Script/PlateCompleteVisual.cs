using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    //Using for check and render
    [Serializable]
    public struct KitchenObjects_SCAB_Complete{

        public KitchenObjects_SCAB scriptDefineObject;  //Object with script define for search, checking
        public GameObject renderDefineObject;   //object with render define, for render only

    }

    //Refer to main Plate
    [SerializeField] private PlateKitchenObject plateKO;
    //Create a new list for store all KitchenObjects_SCAB_Complete
    [SerializeField] private List<KitchenObjects_SCAB_Complete> list_KOSCAB_Complete;
    

    void Start(){
        plateKO.OnAddIngredientHandle += OnAddIngredientHandle_Perform;
        foreach (KitchenObjects_SCAB_Complete kitchenPlateComplete in list_KOSCAB_Complete){
            kitchenPlateComplete.renderDefineObject.SetActive(false);
        }
    }

    private void OnAddIngredientHandle_Perform(object sender, PlateKitchenObject.OnAddIngredientHandleArgs e){
        KitchenObjects_SCAB_Complete kitchenObjects_SCAB_Complete = TryGetRenderAndScript(e.kitchenObjects_SCAB);
        if (kitchenObjects_SCAB_Complete.renderDefineObject != null){
            kitchenObjects_SCAB_Complete.renderDefineObject.SetActive(true);
        }
    }

    private KitchenObjects_SCAB_Complete TryGetRenderAndScript(KitchenObjects_SCAB _input){
        foreach (KitchenObjects_SCAB_Complete kitchenPlateComplete in list_KOSCAB_Complete){
            if (kitchenPlateComplete.scriptDefineObject == _input){
                return kitchenPlateComplete;
            }
        }
        return new KitchenObjects_SCAB_Complete();
    }
}
