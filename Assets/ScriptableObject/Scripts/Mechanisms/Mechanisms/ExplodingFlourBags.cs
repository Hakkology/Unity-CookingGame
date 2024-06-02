using System.Collections;
using UnityEngine;

public class ExplodingFlourBags : IMechanism
{
    private bool isActive;
    private ExplodingFlourBagsDetails details;
    private Transform selfTransform;
    private MechanismTimedBehaviour timedBehaviour;
    private GameObject explosionEffectPrefab;  // ParticleSystem prefab'ını saklamak için

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
        if (this.details != null)
        {
            explosionEffectPrefab = this.details.explosionEffect;
            if (explosionEffectPrefab == null)
            {
                Debug.LogError("Explosion effect prefab is not assigned in ExplodingFlourBagsDetails.");
            }
        }
        else
        {
            Debug.LogError("Initialization failed. Details could not be cast to ExplodingFlourBagsDetails.");
        }
    }



    private void Explode()
    {
        Debug.Log("Attempting to explode at: " + selfTransform.position);
        GameObject explosionInstance = GameObject.Instantiate(explosionEffectPrefab, selfTransform.position, Quaternion.identity);
        Debug.Log("Explosion instantiated at: " + explosionInstance.transform.position); // Log the position of the instantiated explosion

        ParticleSystem ps = explosionInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Debug.Log("Playing particle system.");
            ps.Play();
            Debug.Log($"Scheduled destruction of explosion instance in {ps.main.duration} seconds.");
            GameObject.Destroy(explosionInstance, ps.main.duration);  // Ensure duration is calculated correctly
        }
        else
        {
            Debug.LogError("ParticleSystem component not found in the explosion effect instance.");
        }

        Debug.Log("Applying force to nearby objects.");
        ApplyForceToNearbyObjects();
    }


    private void ApplyForceToNearbyObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(selfTransform.position, details.explosionRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player")) 
            {
                Rigidbody rb = collider.GetComponentInChildren<Rigidbody>();
                if (rb != null) rb.AddExplosionForce(details.explosionForce, selfTransform.position, details.explosionRadius);
            }
        }
    }

    public void ActivateMechanism(float delay = 0)
    {
        isActive = true;
        Debug.Log("Mechanism activated.");
        Explode();
    }
    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        ActivateMechanism(0);
        GameObject.Destroy(selfTransform.gameObject, 1f);
    }

    public void UpdateMechanism()
    {
        // Gerektiğinde güncelleme mantığı ekleyin
    }
}
