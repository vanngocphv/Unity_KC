using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnSpawnedPlate;
    public event EventHandler OnDespawnPlate;
    [SerializeField] private KitchenObjects_SCAB platesObjects_SCAB;
    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlateTimerMax){
            spawnPlateTimer = 0f;

            //If plates spawn still not equal with plates spawn max; spawn another one
            if (MainGameManager.Instance.IsGamePlaying() && platesSpawnAmount < platesSpawnAmountMax){
                //spawn 
                OnSpawnedPlate?.Invoke(this, EventArgs.Empty);
                platesSpawnAmount++;
            }

        }
    }

    // When player using button E for interact with this object
    public override void Interact(PlayerController playerController){
        //Spawn Object
        //Set spawn object into top up this counter
        if (!playerController.CheckKitchenObject()) {
            if (platesSpawnAmount > 0) {
                KitchenObject.SpawnKitchenObject(platesObjects_SCAB, playerController);

                platesSpawnAmount--;
                OnDespawnPlate?.Invoke(this, EventArgs.Empty);
            }
            else{
                Debug.Log("Nothing in there");
            }
        }
        else {
            Debug.Log("Your hand still has object");
        }

    }
}
