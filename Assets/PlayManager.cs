using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
   [SerializeField] BallController ballController;
   [SerializeField] CameraController camController;

       bool isBallOutSide;
       bool isBallTeleporting;
       Vector3 lastBallPosition;
       bool isGoal;

    private void Update()
    {
        if (ballController.ShootingMode)
        {
            lastBallPosition = ballController.transform.position;
        }

        var InputActve = Input.GetMouseButton (0) 
            && ballController.IsMove() == false
            && ballController.ShootingMode == false
            && isBallOutSide == false;

        camController.SetInputActive(InputActve);
    }

    public void OnBallEnter()
    {
        isGoal = true; 
        ballController.enabled =  false;
        // TODO player win window popup
    }

    public void OnBallOutSide()
    {
        if (isGoal)
            return; 

        if (isBallTeleporting == false)
            Invoke("TeleportBallLastPosition", 3);
        
        ballController.enabled = false;
        isBallOutSide = true;
        isBallTeleporting = true;
        
    }

    public void TeleportBallLastPosition()
    {
        TeleportBall(lastBallPosition);
    }

    public void TeleportBall(Vector3 targetPosition)
    {
        var rb =  ballController.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ballController.transform.position = targetPosition;
        rb.isKinematic = false;

        ballController.enabled = true;
        isBallOutSide =  false;
        isBallTeleporting =  false;
    }
}
