using System.Collections;
using UnityEngine;

public class ExplodingFlourBags : IMechanism
{
    private bool isActive;
    private ExplodingFlourBagsDetails details;
    private Transform selfTransform;
    private MechanismTimedBehaviour timedBehaviour;
    private ParticleSystem explosionEffect;
    public bool IsActive
    { 
        get => isActive; 
        set => isActive = value;
    }

    public ExplodingFlourBags(MechanismTimedBehaviour timedBehaviour)
    {
        this.timedBehaviour = timedBehaviour;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as ExplodingFlourBagsDetails;
        this.selfTransform = selfTransform;
        InitializeParticleSystem();
    }

    private void InitializeParticleSystem()
    {
        explosionEffect = details.explosionEffect;
        if (explosionEffect == null)
        {
            Debug.LogError("Explosion effect not found on the mechanism!");
        }
    }

    public void ActivateMechanism(float delay = 0)
    {
        isActive = true;
        if (delay > 0)
        {
            timedBehaviour.StartTimedAction(ExplodeAfterDelay(delay));
        }
        else
        {
            Explode();
        }
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        explosionEffect.Play();
        ApplyForceToNearbyObjects();
        timedBehaviour.StartTimedAction(DestroyMechanismAfterEffect());
    }

    private IEnumerator DestroyMechanismAfterEffect()
    {
        while (explosionEffect.isPlaying)
        {
            yield return null;
        }
        GameObject.Destroy(selfTransform.gameObject);
    }

    private void ApplyForceToNearbyObjects()
    {
        float radius = details.explosionRadius;
        float force = details.explosionForce;
        Collider[] colliders = Physics.OverlapSphere(selfTransform.position, radius);
        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, selfTransform.position, radius);
            }
        }
    }

    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (isActive)
        {
            Explode();
            isActive = false; 
        }
    }

    public void UpdateMechanism()
    {
        // Add update logic if needed
    }
}
