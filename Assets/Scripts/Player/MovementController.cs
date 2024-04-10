using UnityEngine;

public enum MovementState{
    Rolling,
    Sliding,
    Flying,
    Water
}

public class MovementController
{
    private float currentSpeed;
    private Vector3 currentDirection;

    public float CurrentSpeed
    {
        get => currentSpeed;
        set => currentSpeed = Mathf.Clamp(value, 0, MovementConstants.MaxMoveSpeed);
    }
    public Vector3 TargetLocation { get; set; }
    
    private MovementState currentState;
    
    private IMovement rollingMovement;
    private IMovement waterMovement;
    private IMovement slidingMovement;
    private IMovement flyingMovement;
    private IMovement currentMovement;

    public MovementController(Transform ballTransform)
    {
        // Initialize all movement instances
        rollingMovement = new RollingMovement(this, ballTransform);
        waterMovement = new WaterMovement(this, ballTransform);
        slidingMovement = new SlidingMovement(this, ballTransform);
        flyingMovement = new FlyingMovement(this);

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
        // Delegate the update to the current movement state
        currentMovement.Update();
    }
}
