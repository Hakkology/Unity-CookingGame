using System;
using UnityEngine;

public class WaterMovement : IMovement
{
    private Vector3 targetPosition;

    private MovementController movementController;
    private Transform ballTransform;
    private Rigidbody ballRB;

    private float journeyLength;
    bool isGrounded;

    public WaterMovement(MovementController controller, Transform transform, Rigidbody rb)
    {
        movementController = controller;
        ballTransform = transform;
        ballRB = rb;
    }

    public void Init()
    {
        Debug.Log("Initialize Water State");
        ballRB.drag = 0.7f;
        targetPosition = movementController.TargetLocation;
        isGrounded= false;
    }

    public void Update()
    {
        CheckState();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            WaterJump();
        }
    }

    public void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ApplyForceTowardsMouseInWater();
        }
        ApplyWaterResistance();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Water State");
    }

    private void ApplyForceTowardsMouseInWater()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        targetPosition = new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z);
        Vector3 forceDirection = (targetPosition - ballTransform.position).normalized;
        journeyLength = Vector3.Distance(ballTransform.position, targetPosition);

        if (journeyLength > MovementConstants.Epsilon)
        {
            if (ballRB.velocity.magnitude < MovementConstants.MaxMoveSpeed)
            {
                Vector3 force = forceDirection * MovementConstants.WaterForce;
                ballRB.AddForce(force, ForceMode.Force);
                RotateBallInWater(forceDirection, force.magnitude);
            }
        }
    }

    private void RotateBallInWater(Vector3 direction, float appliedForce)
    {
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction).normalized;
        float forceRatio = appliedForce / MovementConstants.WaterForce;
        float rotationSpeed = Mathf.Lerp(MovementConstants.WaterMinRotationSpeed, MovementConstants.WaterRotationSpeed, forceRatio);
        ballRB.AddTorque(rotationAxis * rotationSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void WaterJump()
    {
        ballRB.AddForce(Vector3.up * MovementConstants.WaterJumpForce, ForceMode.Impulse);
        isGrounded = false; 
    }

    private void ApplyWaterResistance()
    {
        float resistanceFactor = MovementConstants.WaterResistanceForce * ballRB.velocity.sqrMagnitude;
        Vector3 resistanceForce = -ballRB.velocity.normalized * resistanceFactor * Time.fixedDeltaTime;
        resistanceForce.y = 0;
        ballRB.AddForce(resistanceForce, ForceMode.Force);
    }

    private void CheckState()
    {
        RaycastHit hit;

        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 2f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            isGrounded = hit.distance < 2f;

            if (hitLayer == 7)  // Sliding layer
            {
                movementController.ChangeState(MovementState.Sliding);
            }
            else if (hitLayer == 6)  // Ground Layer
            {
                movementController.ChangeState(MovementState.Rolling);
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
