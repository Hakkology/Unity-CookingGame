using UnityEngine;

public enum MovementState{
    Rolling,
    Sliding,
    Flying,
    Water
}

public class MovementController
{
    public Vector3 TargetLocation { get; set; }
    
    private MovementState currentState;
    
    private IMovement rollingMovement;
    private IMovement waterMovement;
    private IMovement slidingMovement;
    private IMovement flyingMovement;
    private IMovement currentMovement;

    public MovementController(Transform ballTransform, Rigidbody ballRigidbody, BallCoroutineController coroutineController)
    {

        // Initialize all movement instances
        rollingMovement = new RollingMovement(this, ballTransform, ballRigidbody);
        waterMovement = new WaterMovement(this, ballTransform, ballRigidbody);
        slidingMovement = new SlidingMovement(this, ballTransform, ballRigidbody);
        flyingMovement = new FlyingMovement(this, ballTransform, ballRigidbody, coroutineController);

        // Set the default state
        currentState = MovementState.Rolling;
        currentMovement = rollingMovement;
        currentMovement.Init();
    }

    public void ChangeState(MovementState newState)
    {
        if (currentState == newState) return;

        // Call Cancel on the current state before switching
        currentMovement.Cancel();

        currentState = newState;
        switch (currentState)
        {
            case MovementState.Rolling:
                currentMovement = rollingMovement;
                break;
            case MovementState.Water:
                currentMovement = waterMovement;
                break;
            case MovementState.Sliding:
                currentMovement = slidingMovement;
                break;
            case MovementState.Flying:
                currentMovement = flyingMovement;
                break;
        }

        currentMovement.Init();
    }

    public void Update()
    {
        currentMovement.Update();
    }

    public void FixedUpdate(){

        currentMovement.FixedUpdate();
    }
}
