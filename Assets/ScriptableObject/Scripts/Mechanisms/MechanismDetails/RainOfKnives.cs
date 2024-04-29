using UnityEngine;

[CreateAssetMenu(fileName = "RainOfKnivesDetails", menuName = "Mechanisms/RainOfKnivesDetails", order = 9)]
public class RainOfKnivesDetails : MechanismDetails
{
    [Tooltip("Damage inflicted by each knife.")]
    public float knifeDamage = 10.0f;

    [Tooltip("Radius of the area affected by falling knives.")]
    public float effectRadius = 5.0f;

    [Tooltip("Cooldown time in seconds before the trap can be activated again.")]
    public float cooldownTime = 5.0f;

    [Tooltip("The trapdoor prefab with attached MechanismTrapdoorBehaviour.")]
    public GameObject trapdoorPrefab;

    [Tooltip("The knife effect which shall be triggered with trapdoor.")]
    public ParticleSystem knifeEffectPrefab;
}
