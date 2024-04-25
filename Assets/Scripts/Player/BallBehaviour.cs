using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private MovementController ballMovementController;
    private BallStateController ballStateController;
    [SerializeField] private BallMovementModifiers ballMovementModifiers;
    
    private Transform ballTransform;
    private Rigidbody ballRigidBody;


    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    void Start()
    {
        ballTransform = GetComponent<Transform>();
        ballRigidBody = GetComponent<Rigidbody>();
        ballStateController = GetComponent<BallStateController>();

        ballMovementController = new MovementController(ballTransform, ballRigidBody, ballStateController, ballMovementModifiers);
        ballMovementController.ChangeState(MovementState.Rolling); // Initial state
    }

    public void ChangeState(MovementState newState){
        ballMovementController.ChangeState(newState);
    }

    void Update(){
        ballMovementController.Update();
    }

    void FixedUpdate(){
        ballMovementController.FixedUpdate();
    }

    public void PauseBall()
    {
        savedVelocity = ballRigidBody.velocity;
        savedAngularVelocity = ballRigidBody.angularVelocity;
        
        ballRigidBody.isKinematic = true;
    }

    public void ResumeBall()
    {
        ballRigidBody.isKinematic = false;
        
        ballRigidBody.velocity = savedVelocity;
        ballRigidBody.angularVelocity = savedAngularVelocity;
    }
}
