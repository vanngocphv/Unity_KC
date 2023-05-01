using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    //Icon image
    [SerializeField] private Image prefabIcon;

    public void SetKitchenObjectSO(KitchenObjects_SCAB kitchenObjects_SCAB){
        prefabIcon.sprite = kitchenObjects_SCAB.sprite;
    }
}
