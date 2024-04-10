using UnityEngine;

public class SlidingMovement : IMovement
{
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private MovementController movementController;
    private Transform ballTransform;

    private float journeyLength;
    private float currentSpeed;

    public SlidingMovement(MovementController controller, Transform transform)
    {
        movementController = controller;
        ballTransform = transform;
    }

    public void Init()
    {
        Debug.Log("Initializing Sliding");
        // Use the target location and current speed from the movement controller
        startPosition = ballTransform.position;
        targetPosition = movementController.TargetLocation;
        currentSpeed = movementController.CurrentSpeed;
    }

    public void Update()
    {
        SlidingMovementUpdate();
        CheckGround();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Sliding State");
    }

    private void SlidingMovementUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            Vector3 targetDirection = (new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z) - ballTransform.position).normalized;
            // Adjust the target location very slightly based on input
            targetPosition = Vector3.Lerp(targetPosition, ballTransform.position + targetDirection * 10f, 0.1f); // 10f is an arbitrary distance towards the target direction
            movementController.TargetLocation = targetPosition;
        }

        journeyLength = Vector3.Distance(startPosition, ballTransform.position);

        // Apply slow acceleration/deceleration
        if (journeyLength > 0)
        {
            if (currentSpeed < MovementConstants.SlideMoveSpeed)
            {
                currentSpeed += MovementConstants.SlideMoveSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= MovementConstants.SlideMoveSpeed * Time.deltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0, MovementConstants.SlideMoveSpeed);
            movementController.CurrentSpeed = currentSpeed;
        }

        // Move the ball towards the target position
        Vector3 movementDirection = (targetPosition - ballTransform.position).normalized;
        ballTransform.position += movementDirection * currentSpeed * Time.deltaTime;

        // Always rotate at maximum rotation speed
        ballTransform.Rotate(Vector3.up, MovementConstants.MaxRotationSpeed * Time.deltaTime);
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
                movementController.ChangeState(MovementState.Water);
            }
            else if (hitLayer == 7)  // Sliding layer
            {
                // Stay in Sliding State
            }
        }
    }
}

