using DG.Tweening;
using UnityEngine;

public class PinballSpoon : IMechanism
{
    private bool isActive;
    private PinballSpoonDetails details;
    private Transform selfTransform;
    private Transform playerTransform;
    private Rigidbody rigidbody;
    private MechanismTimedBehaviour timedBehaviour;

    public PinballSpoon(MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
        this.timedBehaviour = timedBehaviour;
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
}
