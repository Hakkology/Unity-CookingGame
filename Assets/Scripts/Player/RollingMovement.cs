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
            movementController.TargetLocation = new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z);
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
            }

            ballTransform.position = Vector3.MoveTowards(ballTransform.position, targetPosition, currentSpeed * Time.deltaTime);

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

    private void CheckGround()
    {
        RaycastHit hit;
        // Adjust the raycast length as necessary
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 2f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == 6)  // Ground layer
            {
                // Stay in RollingMovement
            }
            else if (hitLayer == 4)  // Water layer
            {
                movementController.ChangeState(MovementState.Water);
            }
            else if (hitLayer == 7)  // Sliding layer
            {
                movementController.ChangeState(MovementState.Sliding);
            }
        }
    }
}
