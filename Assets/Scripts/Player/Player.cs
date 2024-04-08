using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementController movementController;

    void Start()
    {
        movementController = new MovementController();
        movementController.ChangeState(MovementState.Rolling); // Initial state
    }

    public void ChangeState(MovementState newState)
    {
        movementController.ChangeState(newState);
    }

    void Update()
    {
        movementController.Update();
    }
}
