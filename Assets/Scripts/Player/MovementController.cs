using UnityEngine;

public enum MovementState{
    Rolling,
    Sliding,
    Flying,
    Water
}

public class MovementController
{
    private MovementState currentState;
    private IMovement rollingMovement;
    private IMovement waterMovement;
    private IMovement slidingMovement;
    private IMovement flyingMovement;
    private IMovement currentMovement;

    public MovementController()
    {
        // Initialize all movement instances
        rollingMovement = new RollingMovement(this);
        waterMovement = new WaterMovement(this);
        slidingMovement = new SlidingMovement(this);
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
