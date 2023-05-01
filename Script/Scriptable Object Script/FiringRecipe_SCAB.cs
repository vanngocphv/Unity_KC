using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FiringRecipe_SCAB : ScriptableObject
{
    //Normal, raw meat
    public KitchenObjects_SCAB rawObject;

    //Cooked object
    public KitchenObjects_SCAB cookedObject;

    //Max time for object has been cooked
    public float maxTimeCooked;
}
