using System;
using DG.Tweening;
using UnityEngine;

public class FlyingMovement : IMovement
{
    private MovementController ballMovementController;
    private BallStateController ballStateController;
    private BallMovementModifiers ballMovementModifiers;
    private Transform ballTransform;
    private Rigidbody ballRB;

    private Vector3 targetPosition;
    private Vector3 originalScale;


    public FlyingMovement(MovementController controller, Transform transform, Rigidbody rb, BallStateController coroutineController, BallMovementModifiers movementModifiers)
    {
        ballMovementController = controller;
        ballStateController = coroutineController;
        ballMovementModifiers = movementModifiers;
        ballTransform = transform;
        ballRB = rb;
    }

    public void Init()
    {
        Debug.Log("Initialize Flying");
        originalScale = ballTransform.parent.localScale;
        ballRB.drag = 0.5f;
    }

    public void Update()
    {
        CheckState();
    }

    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ApplyMinimalForceTowardsMouse();
        }
    }

    public void Cancel()
    {
        Debug.Log("Exiting Flying State");
        BounceBallVertically();

    }

    private void BounceBallVertically()
    {
        Transform ballHolderTransform = ballTransform.parent;
        ballStateController.DoScale(ballHolderTransform, 
                                    new Vector3(originalScale.x, ballMovementModifiers.FlyingDownScale, originalScale.y), 
                                    ballMovementModifiers.ballScaleAdjustmentDuration);
        DOVirtual.DelayedCall(ballMovementModifiers.ballScaleAdjustmentDuration, 
        () => ballStateController.DoScale(ballHolderTransform, originalScale, ballMovementModifiers.ballScaleAdjustmentDuration));
    }
    private void ApplyMinimalForceTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        targetPosition = new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z);
        Vector3 forceDirection = (targetPosition - ballTransform.position).normalized;

        Vector3 force = forceDirection * ballMovementModifiers.FlyingForce;
        ballRB.AddForce(force, ForceMode.Force);
    }

    private void CheckState()
    {
        RaycastHit hit;
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 20f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hit.distance < 1.5f) 
            {
                switch (hitLayer)
                {
                    case 6:  // Ground Layer
                        ballMovementController.ChangeState(MovementState.Rolling);
                        break;
                    case 7:  // Sliding layer
                        ballMovementController.ChangeState(MovementState.Sliding);
                        break;
                    case 4:  // Water layer
                        ballMovementController.ChangeState(MovementState.Water);
                        break;
                }
            }
        }
    }
}
