using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    //Top of spawn
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualPrefabList;         //Need intial in awake
    private float heightIdt = 0.1f;

    void Awake(){
        plateVisualPrefabList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnSpawnedPlate += OnSpawnedPlate_Performed;
        platesCounter.OnDespawnPlate += OnDespawnPlate_Performed;
    }

    //Event
    private void OnSpawnedPlate_Performed(object sender, System.EventArgs e){
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        //set local position
        plateVisualTransform.localPosition = new Vector3(0, heightIdt * plateVisualPrefabList.Count, 0);
        plateVisualPrefabList.Add(plateVisualTransform.gameObject);
        
    }

    private void OnDespawnPlate_Performed(object sender, System.EventArgs e){
        //despawn last one
        GameObject lastPlateInList = plateVisualPrefabList[plateVisualPrefabList.Count - 1];
        plateVisualPrefabList.Remove(lastPlateInList);
        Destroy(lastPlateInList);
    }
}
