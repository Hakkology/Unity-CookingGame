using UnityEngine;

public class ContinuousFlames : IMechanism
{
    private bool isActive;
    private ContinuousFlamesDetails details;
    
    private Transform selfTransform;
    private Transform playerTransform;

    private float timer;
    private bool isOpen = false;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null)
    {
        this.details = details as ContinuousFlamesDetails;;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;

        IsActive = details.isActiveAtStart;
        timer = this.details.openDuration;
        isOpen = true;

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart() => MechanismActivate();
    
    public void MechanismUpdate()
    {
        if (!IsActive) return;

        timer -= Time.deltaTime;

        if (isOpen && timer <= 0)
        {
            isOpen = false;
            timer = details.closedDuration;
        }
        else if (!isOpen && timer <= 0)
        {
            isOpen = true;
            timer = details.openDuration;
        }
    }

    public void MechanismActivate()
    {
        IsActive = true;
        isOpen = true;
        timer = details.openDuration;
    }

    public void MechanismDeactivate()
    {
        IsActive = false;
        isOpen = false;
    }

    public bool CheckActivationConditions()
    {
        // Define conditions for activation
        return true; // Placeholder
    }

    public bool CheckDeactivationConditions()
    {
        // Define conditions for deactivation
        return true; // Placeholder
    }

    public void HandlePlayerContact()
    {
        if (isOpen)
        {
            Debug.Log("Player took damage by flames: " + details.damage);
            if (playerTransform != null)
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
}
