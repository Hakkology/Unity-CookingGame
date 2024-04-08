using UnityEngine;

public class WaterMovement : IMovement
{
    private MovementController movementController;

    // Constructor to pass the MovementController reference
    public RollingMovement(MovementController controller) => movementController = controller;
    public void Init()
    {
        Debug.Log("Initialize Water");
    }

    public void Update()
    {
        Debug.Log("Update Water");

        // Check for input to change state. This is just an example; replace it with your actual input logic.
        if (Input.GetKeyDown(KeyCode.E))
        {
            movementController.ChangeState(MovementState.Flying);
        }
    }

    public void Cancel()
    {
        // Cleanup if needed when exiting water state
    }
}