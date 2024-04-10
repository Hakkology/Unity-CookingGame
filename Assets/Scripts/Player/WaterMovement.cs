using UnityEngine;

public class WaterMovement : IMovement
{
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private MovementController movementController;
    private Transform ballTransform;

    private float journeyLength;
    private float currentSpeed;

    // Constructor to pass the MovementController reference
    public WaterMovement(MovementController controller, Transform transform)
    {
        movementController = controller;
        ballTransform = transform;
    }
    public void Init()
    {
        Debug.Log("Initialize Water State");
        startPosition = ballTransform.position;
        targetPosition = movementController.TargetLocation;
        currentSpeed = movementController.CurrentSpeed;
    }

    public void Update()
    {
        WaterMovementUpdate();
        CheckGround();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Water State");
    }

    private void WaterMovementUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            movementController.TargetLocation = new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z);
            startPosition = ballTransform.position;
            targetPosition = movementController.TargetLocation; 
            journeyLength = Vector3.Distance(startPosition, targetPosition);

            if (journeyLength == 0)
            {
                journeyLength = MovementConstants.Epsilon;
            }
        }

        // Check if the ball is not at the target position to avoid unnecessary calculations
        if (!ballTransform.position.Equals(targetPosition))
        {
            float remainingDistance = Vector3.Distance(ballTransform.position, targetPosition);
            
            // Adjust speed only if necessary
            if (remainingDistance > 0)
            {
                float distanceCovered = Vector3.Distance(startPosition, ballTransform.position);

                if (distanceCovered < journeyLength / 2)
                {
                    currentSpeed += MovementConstants.WaterMoveSpeed * Time.deltaTime;
                }
                else
                {
                    currentSpeed -= MovementConstants.WaterMoveSpeed * Time.deltaTime;
                }

                currentSpeed = Mathf.Clamp(currentSpeed, 0, MovementConstants.WaterMoveSpeed);
                movementController.CurrentSpeed = currentSpeed;
            }

            // Move the ball towards the target position
            ballTransform.position = Vector3.MoveTowards(ballTransform.position, targetPosition, currentSpeed * Time.deltaTime);

            // Rotate the ball
            if (remainingDistance > 0)
            {
                Vector3 direction = targetPosition - ballTransform.position;
                float targetRotationSpeed = Mathf.Lerp(0, MovementConstants.WaterRotationSpeed, currentSpeed / MovementConstants.WaterRotationSpeed);
                float rotationAmount = targetRotationSpeed * Time.deltaTime;
                Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction.normalized);
                ballTransform.Rotate(rotationAxis, rotationAmount, Space.World);
            }
        }
    }

    private void CheckGround()
    {
        RaycastHit hit;
        // Adjust the raycast length as necessary
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 2f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == 6)  // Ground layer
            {
                movementController.ChangeState(MovementState.Rolling);
            }
            else if (hitLayer == 4)  // Water layer
            {
                // Stay in water layer
            }
            else if (hitLayer == 7)  // Sliding layer
            {
                movementController.ChangeState(MovementState.Sliding);
            }
        }
    }
}