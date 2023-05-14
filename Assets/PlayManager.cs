using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
   [SerializeField] BallController ballController;
   [SerializeField] CameraController camController;

    private void Update()
    {
        var InputActve = Input.GetMouseButton (0) && ballController.IsMove() == false;
        camController.SetInputActive(InputActve);
    }
}
