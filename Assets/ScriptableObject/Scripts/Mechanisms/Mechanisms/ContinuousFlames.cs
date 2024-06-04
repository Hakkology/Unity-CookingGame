using UnityEngine;
using System.Collections;

public class ContinuousFlames : IMechanism
{
    private bool isActive;
    private ContinuousFlamesDetails details;
    private BallHealthBehaviour healthBehaviour;
    private Rigidbody playerRigidbody;
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
        
        healthBehaviour = BallManager.Instance.PlayerHealth;
        playerRigidbody = BallManager.Instance.PlayerRigidbody;

        // Başlangıçta mekanizmanın durumunu ayarla ve logla
        SetActive();
        Debug.Log($"Fire Mechanism initialized and set to active. IsActive: {IsActive}");
    }

    public void ActivateMechanism(float delay = 0) => SetActive();
    private void SetActive()
    {
        isActive = true;
        isOpen = true;
        timer = details.openDuration;
        flameEffect.Play(); 
        LevelManager.SoundManager.PlaySound(SoundEffect.FireSound);
    }

    public void DeactivateMechanism(float delay = 0) => SetInactive();
    private void SetInactive()
    {
        isOpen = false;
        timer = details.closedDuration;
        flameEffect.Stop();
        LevelManager.SoundManager.StopSound(SoundEffect.FireSound);
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        Debug.Log($"HandlePlayerContact called. isOpen: {isOpen}, isActive: {isActive}");
        if (isOpen && isActive)
        {
            healthBehaviour.TakeDamage((int)details.damage);
            Debug.Log("Damage applied");

            if (details.pushForce > 0 && playerCollider.attachedRigidbody != null)
            {
                Vector3 forceDirection = (playerCollider.transform.position - selfTransform.position).normalized;
                playerRigidbody.AddForce(forceDirection * details.pushForce, ForceMode.Impulse);
                Debug.Log("Force applied");
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
