using UnityEngine;

[CreateAssetMenu(fileName = "ExplodingFlourBagsDetails", menuName = "Mechanisms/ExplodingFlourBagsDetails", order = 3)]
public class ExplodingFlourBagsDetails : MechanismDetails
{
    public override MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.ExplosiveFlourBags;
    [Header("Explosion Attributes")]
    [Tooltip("Radius of Force applied when triggering the exploding bags.")]
    public float explosionRadius;
    [Tooltip("Force applied to the player when triggering the exploding bags.")]
    public float explosionForce;
    [Header("Explosion Effect")]
    [Tooltip("Particle system used to imitate explosion effect.")]
    public ParticleSystem explosionEffect;
}
