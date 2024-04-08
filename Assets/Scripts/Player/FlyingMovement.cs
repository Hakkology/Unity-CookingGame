public class FlyingMovement : IMovement
{
    private MovementController movementController;

    // Constructor to pass the MovementController reference
    public RollingMovement(MovementController controller) => movementController = controller;
    public void Init()
    {
        Debug.Log("Initialize Rolling");
    }

    public void Update()
    {
        // Check for input to change state. This is just an example; replace it with your actual input logic.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            movementController.ChangeState(MovementState.Rolling);
        }
        Debug.Log("Update Rolling");
    }

    public void Cancel()
    {
        // Cleanup if needed when exiting flying state
    }
}