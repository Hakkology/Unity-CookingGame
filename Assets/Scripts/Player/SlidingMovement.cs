using UnityEngine;

public class SlidingMovement : IMovement
{
    private Vector3 targetPosition;
    private Vector3 currentDirection;
    private float currentSpeed;

    private MovementController movementController;
    private Transform ballTransform;

    public SlidingMovement(MovementController controller, Transform transform)
    {
        movementController = controller;
        ballTransform = transform;
    }

    public void Init()
    {
        Debug.Log("Initializing Sliding");
        currentDirection = (movementController.TargetLocation - ballTransform.position).normalized; 
        currentSpeed = movementController.CurrentSpeed;
        targetPosition = movementController.TargetLocation;
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
            Vector3 inputDirection = (new Vector3(mousePosition.x, ballTransform.position.y, mousePosition.z) - ballTransform.position).normalized;
            
            // Gently adjust the current direction based on input
            currentDirection = Vector3.Lerp(currentDirection, inputDirection, MovementConstants.SlideEfficiencyConstant);
            targetPosition = ballTransform.position + currentDirection * 10f;
            movementController.TargetLocation = targetPosition;
        }

        ballTransform.position += currentDirection * currentSpeed * Time.deltaTime;
        ballTransform.Rotate(currentDirection, MovementConstants.MaxRotationSpeed * Time.deltaTime);

        // Update the current speed in the movement controller
        movementController.CurrentSpeed = currentSpeed;
    }

    private void CheckGround()
    {
        RaycastHit hit;
        // Adjust the raycast length as necessary
        if (Physics.Raycast(ballTransform.position, -Vector3.up, out hit, 3f))
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
            else
            {
                movementController.ChangeState(MovementState.Rolling);
            }
        }
    }
}
