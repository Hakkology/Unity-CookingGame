using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyingMovement : IMovement
{
    private MovementController ballMovementController;
    private BallStateController ballStateController;
    private BallMovementModifiers ballMovementModifiers;
    private Transform ballTransform;
    private Rigidbody ballRB;

    private Vector3 targetPosition;
    private Vector3 originalScale;

    private int groundLayerMask;

    private float inpX;
    private float inpY;
   
    Gyroscope m_Gyro;

    public void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }

    public FlyingMovement(MovementController controller, Transform transform, Rigidbody rb, BallStateController coroutineController, BallMovementModifiers movementModifiers)
    {
        ballMovementController = controller;
        ballStateController = coroutineController;
        ballMovementModifiers = movementModifiers;
        ballTransform = transform;
        ballRB = rb;
        groundLayerMask = LayerMask.GetMask("Ground");
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
        /* if (Input.GetMouseButton(0))
        {
            ApplyMinimalForceTowardsMouse();
        } */

        ApplyMinimalForceTowardsMouse();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Flying State");
        if(CheckBounce()) BounceBallVertically();
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
        
#if UNITY_EDITOR || UNITY_STANDALONE
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
        {
            targetPosition = new Vector3(hit.point.x, ballTransform.position.y, hit.point.z);
            Vector3 forceDirection = (targetPosition - ballTransform.position).normalized;

            Vector3 force = forceDirection * ballMovementModifiers.FlyingForce;
            ballRB.AddForce(force, ForceMode.Force);
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Mathf.Abs(Input.acceleration.x) >= 0.1)
        {
            inpX = Input.acceleration.x;
        }
        else
        {
            inpX = 0.0f;
        }

        if (Mathf.Abs(Input.acceleration.y + 0.7f) >= 0.1 && Input.acceleration.z <= 0)
        {
            inpY = Input.acceleration.y + 0.7f;
        }
        else if (Input.acceleration.z > 0)
        {
            inpY = -1 - Input.acceleration.y - 0.3f;
        }
        else
        {
            inpY = 0.0f;
        }

        Vector3 forceDirection = new Vector3(inpX, 0.0f, inpY).normalized * ballMovementModifiers.FlyingForce;
        ballRB.AddForce(forceDirection, ForceMode.Force);
#endif
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

    bool CheckBounce(){
        RaycastHit hit;
        Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 20f);
        if (hit.distance > 2f){
            return true;
        } 
        else{
            return false;
        }
    }
}
