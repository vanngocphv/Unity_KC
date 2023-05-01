using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This SCAB will store all soundeffect available for all purpose
[CreateAssetMenu()]
public class SoundEffectClipsRefs_SCAB : ScriptableObject
{
    public AudioClip[] chop;                //when cutting
    public AudioClip[] deliveryFail;        //when send delivery wrong order
    public AudioClip[] deliverySuccess;     //when delivery true order
    public AudioClip[] footStep;            //when player run 
    public AudioClip[] objectDrop;          //when drop object in counter
    public AudioClip[] objectPickup;        //when pickup object from counter
    public AudioClip   panSizzle_Loop;      //when put meat in stove
    public AudioClip[] trashInteract;       //when interact KO with trash
    public AudioClip[] warning;             //warning something be bad

}
