using UnityEngine;

public class RollingMovement : IMovement
{
    private Vector2 inputDirection;
    private float maxSpeed = 5.0f;

    private MovementController movementController;
    private Rigidbody ballRigidbody;

    // Constructor to pass the MovementController reference
    public RollingMovement(MovementController controller, Rigidbody rigidbody)
    {
        movementController = controller;
        ballRigidbody = rigidbody;
    }

    public void Init()
    {
        Debug.Log("Initialize Rolling");
        // Initialization logic specific to the Rolling state
    }

    public void Update()
    {
        TouchMovement();
        ClickMovement();

    }

    public void Cancel()
    {
        Debug.Log("Exiting Rolling State");
        // Cleanup logic specific to the Rolling state
    }

    void TouchMovement(){
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Convert touch position to a direction vector in world space
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10)); // Assuming the camera is 10 units above the plane
            inputDirection = new Vector2(touchPosition.x, touchPosition.z).normalized;

            // Apply a force based on the input direction
            Vector3 movement = new Vector3(inputDirection.x, 0, inputDirection.y) * maxSpeed;
            ballRigidbody.AddForce(movement - ballRigidbody.velocity, ForceMode.VelocityChange);
        }

        // Rotate the ball based on its movement
        if (ballRigidbody.velocity != Vector3.zero)
        {
            // Calculate angular velocity
            Vector3 angularVelocity = new Vector3(ballRigidbody.velocity.z, 0, -ballRigidbody.velocity.x) * maxSpeed;

            // Apply the rotation
            ballRigidbody.angularVelocity = angularVelocity;
        }
    }

    void ClickMovement(){
        if (Input.GetMouseButton(0))  // Check if the left mouse button is held down
        {
            // Convert mouse position to a direction vector in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));  // Adjust the Z value based on your camera setup
            inputDirection = new Vector2(mousePosition.x, mousePosition.z).normalized;

            // Apply a force based on the input direction
            Vector3 movement = new Vector3(inputDirection.x, 0, inputDirection.y) * maxSpeed;
            ballRigidbody.AddForce(movement - ballRigidbody.velocity, ForceMode.VelocityChange);
        }

        // Rotate the ball based on its movement
        if (ballRigidbody.velocity != Vector3.zero)
        {
            // Calculate angular velocity
            Vector3 angularVelocity = new Vector3(ballRigidbody.velocity.z, 0, -ballRigidbody.velocity.x) * maxSpeed;
            // Apply the rotation
            ballRigidbody.angularVelocity = angularVelocity;
        }
    }
}
