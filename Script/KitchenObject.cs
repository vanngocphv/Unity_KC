using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjects_SCAB kitchenObjects_SCAB;

    private IF_KitchenObjectParent kitchenObjectParent;

    public KitchenObjects_SCAB GetKitchenObjects_SCAB(){
        return this.kitchenObjects_SCAB;
    }

    public void SetKitchenObjectParent(IF_KitchenObjectParent _kitchenObjectParent){
        //First, check if this current counter still has object inside or not
        if (this.kitchenObjectParent != null){
            this.kitchenObjectParent.ClearCurrentKitchenObject();          //Clear if has
        }

        //Second, setting next current counter for this KitchentObject
        if (_kitchenObjectParent.CheckKitchenObject())
        {
            Debug.LogError("This KitchenObjectParent still has kitchenObject inside");
        }
        this.kitchenObjectParent = _kitchenObjectParent;

        //3th, seting next current KitchenObject = this KitchenObject
        _kitchenObjectParent.SetKitchenObject(this);

        //Setting parent position for this Kitchent Object, it will move to this kitchent automatically
        transform.parent = _kitchenObjectParent.GetCurrentTopPoint();
        transform.localPosition = Vector3.zero;
    }

    public IF_KitchenObjectParent GetKitchenObjectParent(){
        return this.kitchenObjectParent;
    }

    public void DestroySelf(){
        //First, clear kitchenObject in Parent
        kitchenObjectParent.ClearCurrentKitchenObject();
        //Next, Destroy all data in this gameobject
        Destroy(gameObject);
    }

    //this function will be returned 2 value, 1 is bool, and 2 is platekitchenobject
    //Try get plate, check if this kitchenobject in user hand is plate => true
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
        if (this is PlateKitchenObject){
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else{
            plateKitchenObject = null;
            return false;
        }
    } 


    public static KitchenObject SpawnKitchenObject(KitchenObjects_SCAB _kitchenObjects_SCAB, IF_KitchenObjectParent _kitchenObjectParent){
        //Set transform
        Transform kitchenTransform = Instantiate(_kitchenObjects_SCAB.prefabs);
        //Get object
        KitchenObject spawnResult = kitchenTransform.GetComponent<KitchenObject>();
        //Set location spawn
        spawnResult.SetKitchenObjectParent(_kitchenObjectParent);

        return spawnResult;
    }
}
