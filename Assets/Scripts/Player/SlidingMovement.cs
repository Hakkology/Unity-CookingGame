using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlidingMovement : IMovement
{
    private Vector3 currentDirection;

    private MovementController ballMovementController;
    private BallMovementModifiers ballMovementModifiers;
    private BallStateController ballStateController;
    private Transform ballTransform;
    private Rigidbody ballRB;
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

    public SlidingMovement(MovementController controller, Transform transform, Rigidbody rb, BallStateController stateController, BallMovementModifiers movementModifiers)
    {
        ballMovementController = controller;
        ballMovementModifiers = movementModifiers;
        ballStateController = stateController;
        ballTransform = transform;
        ballRB = rb;
        groundLayerMask = LayerMask.GetMask("Ground");
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
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
        {
            Vector3 inputDirection = (new Vector3(hit.point.x, ballTransform.position.y, hit.point.z) - ballTransform.position).normalized;
            currentDirection = Vector3.Lerp(currentDirection, inputDirection, ballMovementModifiers.SlideEfficiencyConstant);
        }
    }

    private void ApplyMinimalControl()
    {   
        /*
        float forceMagnitude = ballMovementModifiers.SlideMoveSpeed;
        Vector3 force = currentDirection * forceMagnitude;
        ballRB.AddForce(force, ForceMode.Force);
        */

        float rotationAmount = ballMovementModifiers.SlideRotationSpeed * Time.deltaTime; 
        ballTransform.Rotate(0, rotationAmount, 0, Space.Self);
        

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
            inpY = -1-Input.acceleration.y -0.3f;
        }
        else
        {
            inpY = 0.0f;
        }
        Vector3 forceDirection = new Vector3(inpX, 0.0f, inpY).normalized * ballMovementModifiers.SlideMoveSpeed;
        ballRB.AddForce(forceDirection, ForceMode.Force);

        
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
