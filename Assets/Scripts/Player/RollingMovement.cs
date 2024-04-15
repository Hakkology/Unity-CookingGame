using System;
using UnityEngine;
public class RollingMovement : IMovement
{
    private Vector3 targetPosition;

    private MovementController movementController;
    private Transform ballTransform;
    private Rigidbody ballRB;

    private float journeyLength;
    private bool isGrounded;

    public RollingMovement(MovementController controller, Transform transform, Rigidbody rb)
    {
        movementController = controller;
        ballTransform = transform;
        ballRB = rb;
    }

    public void Init()
    {
        Debug.Log("Initialize Rolling");
        isGrounded = false;
        ballRB.drag = 0.4f;
    }

    public void Update()
    {
        CheckState();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            RollingJump();
        }
    }

    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ApplyForceTowardsMouse();
        }
    }

    public void Cancel()
    {
        Debug.Log("Exiting Rolling State");
    }

    private void ApplyForceTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        targetPosition = new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z);
        Vector3 forceDirection = (targetPosition - ballTransform.position).normalized;
        journeyLength = Vector3.Distance(ballTransform.position, targetPosition);

        if (journeyLength > MovementConstants.Epsilon)
        {
            if (ballRB.velocity.magnitude < MovementConstants.MaxMoveSpeed)
            {
                Vector3 force = forceDirection * MovementConstants.MaxForce;
                ballRB.AddForce(force, ForceMode.Force);
            }
            RotateBall(forceDirection);
        }
    }

    private void RotateBall(Vector3 direction)
    {
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction).normalized;
        float rotationSpeed = Mathf.Lerp(0, MovementConstants.MaxRotationSpeed, ballRB.velocity.magnitude / 10.0f); // Normalize by some factor of speed
        ballRB.AddTorque(rotationAxis * rotationSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void RollingJump()
    {
        ballRB.AddForce(Vector3.up * MovementConstants.MaxJumpForce, ForceMode.Impulse);
        Debug.Log("Jumping");
        isGrounded = false; 
    }

    private void CheckState()
    {
        RaycastHit hit;

        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 2))
        {
            isGrounded = hit.distance < 2f;

            switch (hit.collider.gameObject.layer)
            {
                case 7:  // layer 7 is Sliding
                    movementController.ChangeState(MovementState.Sliding);
                    break;
                case 4:  // layer 4 is Water
                    movementController.ChangeState(MovementState.Water);
                    break;
                default:
                    break;
            }
        }
        else
        {
            isGrounded = false;
            Debug.Log("Switching to Flying state.");
            movementController.ChangeState(MovementState.Flying);
        }
    }
}