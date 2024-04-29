using UnityEngine;

public class RainOfKnives : IMechanism
{
    private bool isActive;
    private RainOfKnivesDetails details;
    private Transform selfTransform;
    private Transform playerTransform;
    private float cooldownTimer;
    private GameObject trapdoorInstance;
    private ParticleSystem knifeEffect;  // Particle system to simulate falling knives

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null)
    {
        this.details = details as RainOfKnivesDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;

        IsActive = this.details.isActiveAtStart;
        cooldownTimer = 0;

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart()
    {
        SetupTrapdoor();
        if (details.knifeEffectPrefab)
        {
            var effectObject = GameObject.Instantiate(details.knifeEffectPrefab, selfTransform.position, Quaternion.identity, selfTransform);
            knifeEffect = effectObject.GetComponent<ParticleSystem>();
        }
        MechanismActivate();
    }

    private void SetupTrapdoor()
    {
        if (details.trapdoorPrefab)
        {
            var trapdoorBehavior = trapdoorInstance.GetComponent<MechanismTrapdoorBehaviour>();
            if (trapdoorBehavior) trapdoorBehavior.Initialize(this);
        }
    }

    public void MechanismUpdate()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0 && !isActive)
            {
                MechanismActivate();
            }
        }
    }

    public void MechanismActivate()
    {
        if (cooldownTimer <= 0)
        {
            isActive = true;
            cooldownTimer = details.cooldownTime; // Reset cooldown timer
            if (knifeEffect)
            {
                knifeEffect.Play();
            }
        }
    }

    public void MechanismDeactivate()
    {
        isActive = false;
        if (knifeEffect)
        {
            knifeEffect.Stop();
        }
    }

    public bool CheckActivationConditions()
    {
        return isActive && cooldownTimer <= 0;
    }

    public bool CheckDeactivationConditions()
    {
        return !isActive;
    }

    public void HandlePlayerContact()
    {
        if (isActive)
        {
            Debug.Log($"Player takes {details.knifeDamage} damage from knives.");
            MechanismDeactivate();
        }
    }
}
