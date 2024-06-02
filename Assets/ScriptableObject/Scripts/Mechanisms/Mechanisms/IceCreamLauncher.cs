using UnityEngine;
using DG.Tweening;
using System.Collections;

public class IceCreamLauncher : IMechanism
{
    private bool isActive;
    private IceCreamLauncherDetails details;
    
    private Transform selfTransform;
    private BallHealthBehaviour playerHealth; 
    private MechanismTimedBehaviour timedBehaviour;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public IceCreamLauncher(BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour)
    {
        this.playerHealth = playerHealth;
        this.timedBehaviour = timedBehaviour;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as IceCreamLauncherDetails;
        this.selfTransform = selfTransform;
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (isActive)
            return;
        
        timedBehaviour.StartDOTweenAction(DOTween.Sequence().AppendInterval(delay).AppendCallback(() => StartLaunching()));
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
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            Vector3 direction = (playerHealth.transform.position - selfTransform.position).normalized;
            projectileRigidbody.AddForce(direction * details.launchForce);
        }

        projectile.GetComponent<IceCreamBehaviour>().Initialize(playerHealth.transform);
        
        selfTransform.DOShakePosition(details.shakeDuration, details.shakeStrength, details.shakeVibrato, details.shakeRandomness);
    }

    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        Debug.Log("Player detected by Ice Cream Launcher. Activating mechanism.");
        ActivateMechanism(0); 
    }

    public void UpdateMechanism()
    {

    }
}
