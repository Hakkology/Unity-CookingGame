using DG.Tweening;
using UnityEngine;

public class SpringJump : IMechanism
{
    private bool isActive;
    private PinballSpoonDetails details;
    private Transform selfTransform;
    private Rigidbody rigidbody;
    private BallHealthBehaviour playerHealth; 
    private MechanismTimedBehaviour timedBehaviour;

    public SpringJump(MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.timedBehaviour = timedBehaviour; 
        this.rigidbody = rigidbody;
    }

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }
    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as PinballSpoonDetails;
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

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform, Rigidbody rigidBody = null)
    {
        throw new System.NotImplementedException();
    }
}
