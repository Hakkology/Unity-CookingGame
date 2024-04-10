using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private MovementController movementController;
    private Transform ballTransform;

    void Start()
    {
        ballTransform = GetComponent<Transform>();

        movementController = new MovementController(ballTransform);
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
