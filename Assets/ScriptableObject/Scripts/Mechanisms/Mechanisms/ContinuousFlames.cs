using UnityEngine;
using System.Collections;

public class ContinuousFlames : IMechanism
{
    private bool isActive;
    private ContinuousFlamesDetails details;
    
    private Transform selfTransform;
    private Rigidbody rigidBody;
    private BallHealthBehaviour playerHealth; 
    private MechanismTimedBehaviour timedBehaviour;

    private GameObject flameEffect;  // GameObject for the visual flame effect
    private float timer;
    private bool isOpen = false;

    public bool IsActive
    { 
        get => isActive; 
        set => isActive = value;
    }

    public ContinuousFlames(BallHealthBehaviour playerHealth, MechanismTimedBehaviour timedBehaviour, Rigidbody rigidBody)
    {
        this.playerHealth = playerHealth;
        this.timedBehaviour = timedBehaviour;
        this.rigidBody = rigidBody;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as ContinuousFlamesDetails;
        this.selfTransform = selfTransform;

        // Instantiate the flame effect from prefab
        flameEffect = GameObject.Instantiate(this.details.fireEffect, selfTransform.position, Quaternion.identity, selfTransform);
        flameEffect.SetActive(false);

        timer = this.details.closedDuration;
        isOpen = false;
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            timedBehaviour.StartTimedAction(ActivateAfterDelay(delay));
        }
        else
        {
            SetActive();
        }
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetActive();
    }

    private void SetActive()
    {
        IsActive = true;
        isOpen = true;
        timer = details.openDuration;
        flameEffect.SetActive(true);  // Activate the flame effect
    }

    public void DeactivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            timedBehaviour.StartTimedAction(DeactivateAfterDelay(delay));
        }
        else
        {
            SetInactive();
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetInactive();
    }

    private void SetInactive()
    {
        IsActive = false;
        isOpen = false;
        flameEffect.SetActive(false);  // Deactivate the flame effect
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (isOpen && isActive)
        {
            playerHealth.TakeDamage((int)details.damage);

            if (details.pushForce > 0 && playerCollider.attachedRigidbody != null)
            {
                Vector3 forceDirection = (playerCollider.transform.position - selfTransform.position).normalized;
                playerCollider.attachedRigidbody.AddForce(forceDirection * details.pushForce, ForceMode.Impulse);
            }
        }
    }

    public void UpdateMechanism()
    {
        if (!IsActive) return;

        // Countdown timer
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            if (isOpen)
            {
                // Switch to closed state
                isOpen = false;
                timer = details.closedDuration;
                flameEffect.SetActive(false);  // Deactivate the flame effect
            }
            else
            {
                // Switch to open state
                isOpen = true;
                timer = details.openDuration;
                flameEffect.SetActive(true);  // Activate the flame effect
            }
        }
    }
}
