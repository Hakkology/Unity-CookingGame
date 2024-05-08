using UnityEngine;

public interface IMechanism
{
    bool IsActive { get; set; }

    // Initializes the mechanism with all necessary parameters
    void InitializeMechanism(MechanismDetails details, Transform selfTransform);
    void ActivateMechanism(float delay = 0.0f);
    void DeactivateMechanism(float delay = 0.0f);
    void HandlePlayerContact(Collider playerCollider);
    void UpdateMechanism();
}
