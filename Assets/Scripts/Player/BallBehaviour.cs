using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private MovementController movementController;
    private Transform ballTransform;
    private Rigidbody ballRigidBody;

    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

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
