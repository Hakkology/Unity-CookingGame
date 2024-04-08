public class RollingMovement : IMovement
{
    private MovementController movementController;

    // Constructor to pass the MovementController reference
    public RollingMovement(MovementController controller) => movementController = controller;

    public void Init()
    {
        Debug.Log("Initialize Rolling");
        // Initialization logic specific to the Rolling state
    }

    public void Update()
    {
        Debug.Log("Update Rolling");

        // Check for input to change state. This is just an example; replace it with your actual input logic.
        if (Input.GetKeyDown(KeyCode.W))
        {
            movementController.ChangeState(MovementState.Sliding);
        }
    }

    public void Cancel()
    {
        Debug.Log("Exiting Rolling State");
        // Cleanup logic specific to the Rolling state
    }
}
