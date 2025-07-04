using UnityEngine;

[CreateAssetMenu(fileName = "MechanismDetails", menuName = "Mechanisms/MechanismDetails", order = 1)]
public class MechanismDetails : ScriptableObject
{
    public enum ActivationType
    {
        ActivateOnEnter,
        DeactivateOnExit 
    }

    
    [Tooltip("Type of mechanism this configuration pertains to.")]
    
    public virtual MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.None;
    [Header("Mechanism Configuration")]
    [Tooltip("Type of activation mechanism this configuration pertains to.")]
    public ActivationType activationType;

    [Tooltip("Should the mechanism be active immediately upon start.")]
    public bool isActiveAtStart = false;
    public virtual void ApplyDetailsToMechanism(MechanismBehaviour mechanism)
    {
        // Apply base mechanism configurations
    }
}