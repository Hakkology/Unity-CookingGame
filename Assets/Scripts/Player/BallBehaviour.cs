using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private MovementController movementController;
    private Transform ballTransform;
    private Rigidbody ballRigidBody;

    void Start()
    {
        ballTransform = GetComponent<Transform>();
        ballRigidBody = GetComponent<Rigidbody>();

        movementController = new MovementController(ballTransform, ballRigidBody);
        movementController.ChangeState(MovementState.Rolling); // Initial state
    }

    public void ChangeState(MovementState newState){
        movementController.ChangeState(newState);
    }

    void Update(){
        movementController.Update();
    }

    void FixedUpdate(){
        movementController.FixedUpdate();
    }
}
