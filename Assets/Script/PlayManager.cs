using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
   [SerializeField] BallController ballController;
   [SerializeField] CameraController camController;
   [SerializeField] GameObject finishWindow;
   [SerializeField] TMP_Text finishText;
   [SerializeField] TMP_Text shootCountText;

       bool isBallOutSide;
       bool isBallTeleporting;
       Vector3 lastBallPosition;
       bool isGoal;
       Vector3 lastBallPositions;

       private void OnEnable()
       {
            ballController.onBallShooted.AddListener(UpdateShootCount);
       }

       private void OnDisable()
       {
            ballController.onBallShooted.RemoveListener(UpdateShootCount);
       }

    private void Update()
    {
       // Debug.Log(
       //     ballController.ShootingMode.ToString() + " " +
       //     ballController.IsMove() + " " +
       //     isBallOutSide + " " +
       //     ballController.enabled + " " +
       //     isBallTeleporting + " " +
       //     isGoal
       //     );

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

    public void OnBallGoalEnter()
    {
        isGoal = true; 
        ballController.enabled =  false;

        finishWindow.gameObject.SetActive(true);
        finishText.text = "Finished!!!\n" + "Shoot Count: " + ballController.ShootCount; 
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
    
    public void UpdateShootCount(int shootCount)
    {
        shootCountText.text = shootCount.ToString();
    }

}
