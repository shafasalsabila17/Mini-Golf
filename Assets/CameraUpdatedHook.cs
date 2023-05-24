using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

 
public class CameraUpdatedHook : MonoBehaviour
{
    private void OnEnable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }
 
    private void OnDisable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
    }
 
    void OnCameraUpdated(CinemachineBrain brain)
    {
        Camera camera = brain.OutputCamera;
 
        // Do your stuff here with the camera
    }
}