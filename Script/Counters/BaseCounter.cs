using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is parent class, the child class will be inheirit from all method and variable
public class BaseCounter : MonoBehaviour, IF_KitchenObjectParent
{
    //play sound effect event
    public static event EventHandler OnAnyObjectPlaceSound;

    public static void ResetStaticData(){
        OnAnyObjectPlaceSound = null;
    }
    [SerializeField] private Transform counterTopPoint;             // The top of this counter
    private KitchenObject kitchenObject;                            // Kitchen Object
    
    public virtual void Interact(PlayerController playerController){
    }

    public virtual void InteractAlternate(PlayerController playerController){
    }

    //Get Current Top Point of this current KitchenParent
    public Transform GetCurrentTopPoint(){
        return counterTopPoint;
    }
    //Set, get, clear kitchent object
    public KitchenObject GetCurrentKitchenObject(){
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject _kitchenObject){
        kitchenObject = _kitchenObject;
        if (kitchenObject != null){
            OnAnyObjectPlaceSound?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ClearCurrentKitchenObject(){
        kitchenObject = null;
    }

    //Check if this KitchenParent has object inside
    public bool CheckKitchenObject(){
        return kitchenObject != null;
    }
}
