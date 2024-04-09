using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private MovementController movementController;
    private Rigidbody ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        movementController = new MovementController(ballRigidbody);
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
