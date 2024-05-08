using UnityEngine;

public class ExplodingFlourBags : IMechanism
{
    private bool isActive;
    private ExplodingFlourBagsDetails details;
    private Transform selfTransform;
    private Rigidbody rigidbody;
    private BallHealthBehaviour playerHealth; 
    private MechanismTimedBehaviour timedBehaviour;
    public bool IsActive
    { 
        get => isActive; 
        set => isActive = value;
    }

    public ExplodingFlourBags(BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.playerHealth = playerHealth;
        this.timedBehaviour = timedBehaviour;
        this.rigidbody = rigidbody;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as ExplodingFlourBagsDetails;
        this.selfTransform = selfTransform;
    }

    public void ActivateMechanism(float delay = 0)
    {
        throw new System.NotImplementedException();
    }

    public void DeactivateMechanism(float delay = 0)
    {
        throw new System.NotImplementedException();
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateMechanism()
    {
        throw new System.NotImplementedException();
    }
}
