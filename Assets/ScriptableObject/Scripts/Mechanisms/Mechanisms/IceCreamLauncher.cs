using UnityEngine;
using DG.Tweening;

public class IceCreamLauncher : IMechanism
{
    private bool isActive;
    private IceCreamLauncherDetails details;
    
    private Transform selfTransform;
    private Transform playerTransform;
    private float launchCooldownTimer = 0f;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null)
    {
        this.details = details as IceCreamLauncherDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;

        IsActive = false;
        launchCooldownTimer = this.details.launchInterval;
    }

    public void MechanismStart()
    {
        // Optionally perform setup tasks here
    }

    public void MechanismUpdate()
    {
        if (IsActive)
        {
            launchCooldownTimer -= Time.deltaTime;
            if (launchCooldownTimer <= 0)
            {
                LaunchIceCream();
                launchCooldownTimer = details.launchInterval; // Reset cooldown timer after launching
            }
        }

        if (CheckActivationConditions())
        {
            if (!IsActive)
            {
                IsActive = true;
                selfTransform.DOShakePosition(1.0f, 0.5f, 10, 90, false); // Shake animation using DOTween
            }
        }
        else if (CheckDeactivationConditions())
        {
            IsActive = false;
        }
    }

    private void LaunchIceCream()
    {
        GameObject iceCream = GameObject.Instantiate(details.iceCreamPrefab, selfTransform.position, Quaternion.identity);
        IceCreamBehaviour iceCreamBehaviour = iceCream.AddComponent<IceCreamBehaviour>();
        iceCreamBehaviour.Initialize(playerTransform);
    }

    public void MechanismActivate()
    {
        IsActive = true; // Manually activate if needed
    }

    public void MechanismDeactivate()
    {
        IsActive = false; // Manually deactivate if needed
    }

    public bool CheckActivationConditions()
    {
        return playerTransform != null && Vector3.Distance(selfTransform.position, playerTransform.position) <= details.activationRange;
    }

    public bool CheckDeactivationConditions()
    {
        return playerTransform != null && Vector3.Distance(selfTransform.position, playerTransform.position) > details.activationRange;
    }

    public void HandlePlayerContact()
    {
        // Not required for the fridge itself
    }
}
