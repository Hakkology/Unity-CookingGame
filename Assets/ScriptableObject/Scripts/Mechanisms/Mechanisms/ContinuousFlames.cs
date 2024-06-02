using UnityEngine;
using System.Collections;

public class ContinuousFlames : IMechanism
{
    private bool isActive;
    private ContinuousFlamesDetails details;
    private BallHealthBehaviour healthBehaviour;
    private Transform selfTransform;


    private ParticleSystem flameEffect;
    private float timer;
    private bool isOpen = false;

    public bool IsActive
    { 
        get => isActive; 
        set => isActive = value;
    }

    public ContinuousFlames()
    {

    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as ContinuousFlamesDetails;
        this.selfTransform = selfTransform;
        flameEffect = selfTransform.GetComponentInChildren<ParticleSystem>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        healthBehaviour = player.GetComponent<BallHealthBehaviour>();
        
        SetInactive();
    }

    public void ActivateMechanism(float delay = 0) => SetActive();
    private void SetActive()
    {
        isActive = true;
        isOpen = true;
        timer = details.openDuration;
        flameEffect.Play(); 
    }

    public void DeactivateMechanism(float delay = 0) => SetInactive();
    private void SetInactive()
    {
        isOpen = false;
        timer = details.closedDuration;
        flameEffect.Stop();
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (isOpen && isActive)
        {
            healthBehaviour.TakeDamage((int)details.damage);

            if (details.pushForce > 0 && playerCollider.attachedRigidbody != null)
            {
                Vector3 forceDirection = (playerCollider.transform.position - selfTransform.position).normalized;
                playerCollider.attachedRigidbody.AddForce(forceDirection * details.pushForce, ForceMode.Impulse);
            }
        }
    }

    public void UpdateMechanism()
    {
        if (!isActive) return;
        // Countdown timer
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            if (isOpen)
                DeactivateMechanism(0);
            
            else
                ActivateMechanism(0);
        }
    }
}
