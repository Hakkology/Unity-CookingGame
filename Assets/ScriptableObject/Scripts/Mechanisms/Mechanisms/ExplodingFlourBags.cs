using UnityEngine;

public class ExplosingFlourBags : IMechanism
{
    private bool isActive;
    private ExplodingFlourBagsDetails details;
    private Transform selfTransform;
    private Transform playerTransform;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null)
    {
        this.details = details as ExplodingFlourBagsDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;

        IsActive = details.isActiveAtStart;

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart()
    {
        if (selfTransform != null)
        {
            
        }
        MechanismActivate();
    }

    public void MechanismUpdate()
    {
        // Update behavior like moving, growing, or collision checks
    }

    public void MechanismActivate()
    {
        IsActive = true;
        // Implement activation logic, e.g., turning on particles or effects
    }

    public void MechanismDeactivate()
    {
        IsActive = false;
        // Implement deactivation logic, e.g., turning off particles or effects
    }

    public bool CheckActivationConditions()
    {
        // Define conditions for activation
        return false; // Placeholder
    }

    public bool CheckDeactivationConditions()
    {
        // Define conditions for deactivation
        return false; // Placeholder
    }

    public void HandlePlayerContact()
    {
        if (IsActive && playerTransform != null)
        {
            Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 pushDirection = (playerTransform.position - selfTransform.position).normalized;
                playerRigidbody.AddForce(pushDirection * details.pushForce, ForceMode.Impulse);
            }
        }
    }
}
