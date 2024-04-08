using UnityEngine;

public class SlidingMovement : IMovement
{
    private MovementController movementController;

    // Constructor to pass the MovementController reference
    public SlidingMovement(MovementController controller) => movementController = controller;
    public void Init()
    {
        Debug.Log("Initialize Sliding");
    }

    public void Update()
    {
        // Check for input to change state. This is just an example; replace it with your actual input logic.
        if (Input.GetKeyDown(KeyCode.R))
        {
            movementController.ChangeState(MovementState.Water);
        }
    }

    public void Cancel()
    {
        // Cleanup if needed when exiting rolling state
    }
}
