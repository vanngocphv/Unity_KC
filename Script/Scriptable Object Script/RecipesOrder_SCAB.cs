using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipesOrder_SCAB : ScriptableObject
{
    public List<KitchenObjects_SCAB> list_KichenObject_SCAB; //List all recipe for one order
    public string recipeName; //name this order
}
