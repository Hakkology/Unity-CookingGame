using UnityEngine;

public class RollingMovement : IMovement
{
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private MovementController movementController;
    private Transform ballTransform;

    private float journeyLength;
    private float currentSpeed;

    public RollingMovement(MovementController controller, Transform transform)
    {
        movementController = controller;
        ballTransform = transform;
    }

    public void Init()
    {
        Debug.Log("Initialize Rolling");
        startPosition = ballTransform.position;
        // Use the current target location from the movement controller
        targetPosition = movementController.TargetLocation;
        currentSpeed = movementController.CurrentSpeed;
        AdjustBallHeightToGround();
    }

    public void Update()
    {
        RollingMovementUpdate();
        CheckGround();
    }

    public void Cancel()
    {
        Debug.Log("Exiting Rolling State");
    }
    
    private void RollingMovementUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            movementController.TargetLocation = new Vector3(mousePosition.x, 0, mousePosition.z); // Y value will be adjusted based on terrain
            startPosition = ballTransform.position;
            targetPosition = movementController.TargetLocation;
            journeyLength = Vector3.Distance(startPosition, targetPosition);

            if (journeyLength == 0)
            {
                journeyLength = MovementConstants.Epsilon;
            }
        }

        if (!ballTransform.position.Equals(targetPosition))
        {
            float remainingDistance = Vector3.Distance(ballTransform.position, targetPosition);

            if (remainingDistance > 0)
            {
                float distanceCovered = Vector3.Distance(startPosition, ballTransform.position);

                if (distanceCovered < journeyLength / 2)
                {
                    currentSpeed += MovementConstants.MaxMoveSpeed * Time.deltaTime;
                }
                else
                {
                    currentSpeed -= MovementConstants.MaxMoveSpeed * Time.deltaTime;
                }

                currentSpeed = Mathf.Clamp(currentSpeed, 0, MovementConstants.MaxMoveSpeed);
                movementController.CurrentSpeed = currentSpeed;

                // Calculate next position ignoring y-value changes for now
                Vector3 nextPosition = Vector3.MoveTowards(ballTransform.position, new Vector3(targetPosition.x, ballTransform.position.y, targetPosition.z), currentSpeed * Time.deltaTime);

                // Adjust y-value based on terrain height
                AdjustBallHeightToGround();

                ballTransform.position = nextPosition;

                // Handle rotation
                if (remainingDistance > 0)
                {
                    Vector3 direction = (targetPosition - ballTransform.position).normalized;
                    float targetRotationSpeed = Mathf.Lerp(0, MovementConstants.MaxRotationSpeed, currentSpeed / MovementConstants.MaxMoveSpeed);
                    float rotationAmount = targetRotationSpeed * Time.deltaTime;
                    Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);
                    ballTransform.Rotate(rotationAxis, rotationAmount, Space.World);
                }
            }
        }
    }

    private void AdjustBallHeightToGround() {
        RaycastHit hit;
        // Start the raycast well above the ball to ensure it hits the ground even if the ball is below the terrain
        if (Physics.Raycast(ballTransform.position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity)) {
            ballTransform.position = new Vector3(ballTransform.position.x, hit.point.y, ballTransform.position.z);
        }
    }

    private void CheckGround()
    {
        RaycastHit hit;
        // Adjust the raycast length as necessary
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 2f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == 4)  // Water layer
            {
                movementController.ChangeState(MovementState.Water);
            }
            else if (hitLayer == 7)  // Sliding layer
            {
                movementController.ChangeState(MovementState.Sliding);
            }
            else
            {
                movementController.ChangeState(MovementState.Rolling);
            }
        }
    }
}
