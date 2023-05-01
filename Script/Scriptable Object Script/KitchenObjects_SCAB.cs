using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is scriptable object class
[CreateAssetMenu()]
public class KitchenObjects_SCAB : ScriptableObject
{
    public Transform prefabs;           //this is prefabs object  
    public Sprite sprite;               //this is image of object
    public string prefabsName;          //this is name of object
}
