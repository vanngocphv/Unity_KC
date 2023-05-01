using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    //this script will reset all static data in the game if have any
    private void Start() {
        BaseCounter.ResetStaticData(); //reset sound pickup effect
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();

    }
}
