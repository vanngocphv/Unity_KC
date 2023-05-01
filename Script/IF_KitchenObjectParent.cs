using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IF_KitchenObjectParent
{
    //Get Current Top Point of this current KitchenParent
    public Transform GetCurrentTopPoint();
    //Set, get, clear kitchent object
    public KitchenObject GetCurrentKitchenObject();
    public void SetKitchenObject(KitchenObject _kitchenObject);
    public void ClearCurrentKitchenObject();

    //Check if this KitchenParent has object inside
    public bool CheckKitchenObject();
}
