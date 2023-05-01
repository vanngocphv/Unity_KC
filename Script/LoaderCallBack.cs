using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstFrameUpdate = true;

    //only run 1 frame
    private void Update(){
        if (isFirstFrameUpdate){
            isFirstFrameUpdate = false;

            Loader_Static.LoaderCallBack();
        }
    }
}
