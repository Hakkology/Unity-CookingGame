using UnityEngine;
using DG.Tweening;
using System.Collections;

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
        if (delay > 0)
            timedBehaviour.StartDOTweenAction(DOTween.Sequence().AppendInterval(delay).AppendCallback(() => StartLaunching()));
        else
            StartLaunching();
    }

    private void StartLaunching()
    {
        isActive = true;
        timedBehaviour.StartTimedAction(LaunchProjectileRoutine());
    }

    private IEnumerator LaunchProjectileRoutine()
    {
        while (isActive)
        {
            LaunchProjectile();
            yield return new WaitForSeconds(details.launchInterval);
        }
    }

    private void LaunchProjectile()
    {
        GameObject projectile = GameObject.Instantiate(details.projectilePrefab, selfTransform.position, Quaternion.identity);
        projectile.GetComponent<IceCreamBehaviour>().Initialize(playerHealth.transform);
        
        // Add a shake effect to the fridge after launching an ice cream
        selfTransform.DOShakePosition(details.shakeDuration, details.shakeStrength, details.shakeVibrato, details.shakeRandomness);
    }

    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        // Optionally handle player contact here
    }

    public void UpdateMechanism()
    {
        if (!isActive && Vector3.Distance(selfTransform.position, playerHealth.transform.position) <= details.detectionRange)
        {
            ActivateMechanism();
        }
    }
}
