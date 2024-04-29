using UnityEngine;

[CreateAssetMenu(fileName = "ContinuousFlamesDetails", menuName = "Mechanisms/ContinuousFlamesDetails", order = 2)]
public class ContinuousFlamesDetails : MechanismDetails
{
    [Header("Flame Timing")]
    [Tooltip("Duration for which flames are open and can cause damage.")]
    public float openDuration = 1.0f; 

    [Tooltip("Duration for which flames are closed and safe to pass through.")]
    public float closedDuration = 1.0f;

    [Header("Flame Effects")]
    [Tooltip("Damage dealt to the player on contact with the flames.")]
    public float damage = 10.0f; 

    [Tooltip("Force applied to the player when making contact with the flames.")]
    public float pushForce = 5.0f;
}
