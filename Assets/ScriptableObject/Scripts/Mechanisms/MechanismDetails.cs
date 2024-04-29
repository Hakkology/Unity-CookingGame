using UnityEngine;

[CreateAssetMenu(fileName = "MechanismDetails", menuName = "Mechanisms/MechanismDetails", order = 1)]
public class MechanismDetails : ScriptableObject
{
    [Header("Mechanism Configuration")]
    [Tooltip("Type of mechanism this configuration pertains to.")]
    public MechanismFactory.MechanismType mechanismType;

    [Tooltip("Should the mechanism be active immediately upon start.")]
    public bool isActiveAtStart = false;

    [Tooltip("Delay before the mechanism activates after being enabled.")]
    public float activationDelay = 0.0f;

    [Tooltip("Delay before the mechanism deactivates after being disabled.")]
    public float deactivationDelay = 0.0f;

    public virtual void ApplyDetailsToMechanism(MechanismBehaviour mechanism)
    {
        // Apply base mechanism configurations
    }
}