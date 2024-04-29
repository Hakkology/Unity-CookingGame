using UnityEngine;

[CreateAssetMenu(fileName = "MechanismDetails", menuName = "Mechanisms/MechanismDetails", order = 1)]
public class MechanismDetails : ScriptableObject
{
    public MechanismFactory.MechanismType mechanismType;
    public bool isActiveAtStart = false;
    public float activationDelay = 0.0f;
    public float deactivationDelay = 0.0f;

    public virtual void ApplyDetailsToMechanism(MechanismBehaviour mechanism)
    {
        
    }
}