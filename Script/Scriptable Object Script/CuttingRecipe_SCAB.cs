using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipe_SCAB : ScriptableObject
{
    //input
    public KitchenObjects_SCAB rawObject;       //raw Object;

    //output
    public KitchenObjects_SCAB cuttingObject;   //cutting Object;

    //Counter cutting progress
    public int cuttingProgressMaxCounter;       //Max counter for this object
}
