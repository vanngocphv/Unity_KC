using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FriedRecipe_SCAB : ScriptableObject
{
    //Object has been cooked as input object
    public KitchenObjects_SCAB cookedObject;

    //Burned object when the time has come
    public KitchenObjects_SCAB burnedObject;

    //Max time for object has been cooked
    public float maxTimeBurned;
}
