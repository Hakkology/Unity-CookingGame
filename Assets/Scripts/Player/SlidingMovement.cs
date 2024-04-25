using System;
using UnityEngine;

public class SlidingMovement : IMovement
{
    private Vector3 currentDirection;

    private MovementController ballMovementController;
    private BallMovementModifiers ballMovementModifiers;
    private BallStateController ballStateController;
    private Transform ballTransform;
    private Rigidbody ballRB;

    public SlidingMovement(MovementController controller, Transform transform, Rigidbody rb, BallStateController stateController, BallMovementModifiers movementModifiers)
    {
        ballMovementController = controller;
        ballMovementModifiers = movementModifiers;
        ballStateController = stateController;
        ballTransform = transform;
        ballRB = rb;
    }

    public void Init()
    {
        Debug.Log("Initializing Sliding");
        currentDirection = (ballMovementController.TargetLocation - ballTransform.position).normalized; 
        ballRB.drag = 0.1f; 
        Transform ballHolderTransform = ballTransform.parent;
        ballStateController.DoScale(ballHolderTransform, ballMovementModifiers.ballDefaultScale, ballMovementModifiers.ballScaleAdjustmentDuration);
    }

    public void Update(){
        CheckState();
        if (Input.GetMouseButtonDown(0))
        {
            AdjustDirectionBasedOnInput();
        }
    }
    public void FixedUpdate(){

        ApplyMinimalControl();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Sliding State");
    }

    private void AdjustDirectionBasedOnInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        Vector3 inputDirection = (new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z) - ballTransform.position).normalized;
        currentDirection = Vector3.Lerp(currentDirection, inputDirection, ballMovementModifiers.SlideEfficiencyConstant);
    }

    private void ApplyMinimalControl()
    {
        float forceMagnitude = ballMovementModifiers.SlideMoveSpeed;
        Vector3 force = currentDirection * forceMagnitude;
        ballRB.AddForce(force, ForceMode.Force);

        float rotationAmount = ballMovementModifiers.SlideRotationSpeed * Time.deltaTime; 
        ballTransform.Rotate(0, rotationAmount, 0, Space.Self);
    }

    private void CheckState()
    {
        RaycastHit hit;
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 3f))
        {

            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == 6)  // Ground layer
            {
                ballMovementController.ChangeState(MovementState.Rolling);
            }
            else if (hitLayer == 4)  // Water layer
            {
                ballMovementController.ChangeState(MovementState.Water);
            }
        }
    }
}
