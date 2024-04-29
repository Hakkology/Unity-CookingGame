using UnityEngine;

[CreateAssetMenu(fileName = "GreaseDetails", menuName = "Mechanisms/GreaseDetails", order = 4)]
public class GreaseDetails : MechanismDetails
{
    [Tooltip("Radius of the grease area.")]
    public float radius = 5.0f;

    [Tooltip("Physics material to apply to the grease area for sliding effect.")]
    public PhysicMaterial slidingMaterial;
}
