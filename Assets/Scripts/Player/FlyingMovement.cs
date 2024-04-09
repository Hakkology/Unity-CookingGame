using UnityEngine;

public class FlyingMovement : IMovement
{
    private MovementController movementController;

    // Constructor to pass the MovementController reference
    public FlyingMovement(MovementController controller) => movementController = controller;
    public void Init()
    {
        Debug.Log("Initialize Flying");
    }

    public void Update()
    {
        // Check for input to change state. This is just an example; replace it with your actual input logic.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            movementController.ChangeState(MovementState.Rolling);
        }
    }

    public void Cancel()
    {
        Debug.Log("Exiting Flying State");
        // Cleanup if needed when exiting flying state
    }
}