using UnityEngine;
using DG.Tweening;

public class IceCreamLauncher : IMechanism
{
    private bool isActive;
    private IceCreamLauncherDetails details;
    
    private Transform selfTransform;
    private Rigidbody rigidbody;
    private BallHealthBehaviour playerHealth; 
    private MechanismTimedBehaviour timedBehaviour;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }
    public IceCreamLauncher(BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.playerHealth = playerHealth;
        this.timedBehaviour = timedBehaviour;
        this.rigidbody = rigidbody;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as IceCreamLauncherDetails;
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
