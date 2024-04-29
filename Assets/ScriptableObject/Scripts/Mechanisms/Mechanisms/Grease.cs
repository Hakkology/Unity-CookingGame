using System;
using UnityEngine;

public class Grease : IMechanism
{

    private bool isActive;
    private GreaseDetails details;
    private Transform selfTransform;
    private Transform playerTransform;
    private GameObject greaseArea;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null)
    {
        this.details = details as GreaseDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;

        IsActive = details.isActiveAtStart;

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart()
    {
        CreateGreaseArea();
        MechanismActivate();
    }

    private void CreateGreaseArea()
    {
        // Create a new GameObject to represent the grease area
        greaseArea = new GameObject("GreaseArea");
        greaseArea.transform.position = selfTransform.position;
        greaseArea.transform.localScale = new Vector3(details.radius, 0.1f, details.radius);
        greaseArea.layer = LayerMask.NameToLayer("Sliding");

        // Add a collider and set it to be a trigger
        var collider = greaseArea.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = 1; // Since the scale is already set

        // Apply the sliding material to the collider
        if (details.slidingMaterial != null)
        {
            var rb = greaseArea.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            collider.material = details.slidingMaterial;
        }
    }

    public void MechanismUpdate()
    {
        // Update behavior like moving, growing, or collision checks
    }

    public void MechanismActivate()
    {
        IsActive = true;
        // Implement activation logic, e.g., turning on particles or effects
    }

    public void MechanismDeactivate()
    {
        IsActive = false;
        if (greaseArea != null)
        {
            GameObject.Destroy(greaseArea);
        }
    }

    public bool CheckActivationConditions()
    {
        // Define conditions for activation
        return false; // Placeholder
    }

    public bool CheckDeactivationConditions()
    {
        // Define conditions for deactivation
        return false; // Placeholder
    }

    public void HandlePlayerContact()
    {
        throw new System.NotImplementedException();
    }
}
