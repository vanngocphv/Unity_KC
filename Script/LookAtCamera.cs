using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    //A enum which will let to create with hard code
    private enum Mode{
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;

    //This update will be run after regular update has been updated
    private void LateUpdate() {
        switch (mode){
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform); break;
            case Mode.LookAtInverted:
                Vector3 cameraDir = transform.position - Camera.main.transform.position; //Create a vector from camera position
                transform.LookAt(transform.position + cameraDir);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
