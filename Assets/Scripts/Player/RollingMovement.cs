using System;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;

public class RollingMovement : IMovement
{
    private Vector3 targetPosition;
    private MovementController ballMovementController;
    private BallMovementModifiers ballMovementModifiers;
    private BallStateController ballStateController;
    private Transform ballTransform;
    private Rigidbody ballRB;

    private float journeyLength;
    private bool isGrounded;
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


    public RollingMovement(MovementController controller, Transform transform, Rigidbody rb, BallStateController stateController, BallMovementModifiers movementModifiers)
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
        Debug.Log("Initialize Rolling");
        isGrounded = false;
        ballRB.drag = 0.4f;
        Transform ballHolderTransform = ballTransform.parent;
        ballStateController.DoScaleAdjustment(ballHolderTransform, ballMovementModifiers.ballDefaultScale, ballMovementModifiers.ballScaleAdjustmentDuration);
    }

    public void Update()
    {
        CheckState();
        if (Input.GetMouseButtonDown(0) && isGrounded) RollingJump();
    }

    public void FixedUpdate()
    {
        ApplyForceTowardsMouse();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Rolling State");
    }

    private void ApplyForceTowardsMouse()
    {
        
#if UNITY_EDITOR
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
        {
            targetPosition = new Vector3(hit.point.x, ballTransform.position.y, hit.point.z);
            Vector3 forceDirection = (targetPosition - ballTransform.position).normalized;
            journeyLength = Vector3.Distance(ballTransform.position, targetPosition);

            if (journeyLength > ballMovementModifiers.Epsilon)
            {
                if (ballRB.velocity.magnitude < ballMovementModifiers.MaxMoveSpeed)
                {
                    Vector3 force = forceDirection * ballMovementModifiers.MaxForce;
                    ballRB.AddForce(force, ForceMode.Force);
                }
                RotateBall(forceDirection);
            }
        } 
        
#endif

#if UNITY_ANDROID

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
        Vector3 forceDirection = new Vector3(inpX, 0.0f, inpY).normalized * ballMovementModifiers.MaxForce;
        ballRB.AddForce(forceDirection, ForceMode.Force);
        RotateBall(forceDirection);

#endif

    }

    private void RotateBall(Vector3 direction)
    {
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction).normalized;
        float rotationSpeed = Mathf.Lerp(0, ballMovementModifiers.MaxRotationSpeed, ballRB.velocity.magnitude / 10.0f); // Normalize by some factor of speed
        ballRB.AddTorque(rotationAxis * rotationSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void RollingJump()
    {
        ballRB.AddForce(Vector3.up * ballMovementModifiers.MaxJumpForce, ForceMode.Impulse);
        Debug.Log("Jumping");
        isGrounded = false;
    }

    private void CheckState()
    {
        RaycastHit hit;

        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 3))
        {
            isGrounded = hit.distance < 3f;

            switch (hit.collider.gameObject.layer)
            {
                case 7:  // layer 7 is Sliding
                    ballMovementController.ChangeState(MovementState.Sliding);
                    break;
                case 4:  // layer 4 is Water
                    ballMovementController.ChangeState(MovementState.Water);
                    break;
                default:
                    break;
            }
        }
        else
        {
            isGrounded = false;
            Debug.Log("Switching to Flying state.");
            ballMovementController.ChangeState(MovementState.Flying);
        }
    }
}
