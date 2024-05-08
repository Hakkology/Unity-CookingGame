using UnityEngine;

public class ContinuousFlames : IMechanism
{
    private bool isActive;
    private ContinuousFlamesDetails details;
    
    private Transform selfTransform;
    private Rigidbody rigidBody;
    private BallHealthBehaviour playerHealth; 
    private MechanismTimedBehaviour timedBehaviour;

    private float timer;
    private bool isOpen = false;
    public bool IsActive
    { 
        get => isActive; 
        set => isActive = value;
    }

    public ContinuousFlames(BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.playerHealth = playerHealth;
        this.timedBehaviour = timedBehaviour;
        rigidBody = rigidbody;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as ContinuousFlamesDetails;
        this.selfTransform = selfTransform;

        timer = this.details.closedDuration;
        isOpen = false;
    }

    public void ActivateMechanism(float delay = 0)
    {

    }

    public void DeactivateMechanism(float delay = 0)
    {

    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (isOpen && isActive)
        {
            playerHealth.TakeDamage((int)details.damage);
        }
    }

    public void UpdateMechanism()
    {

    }
}
