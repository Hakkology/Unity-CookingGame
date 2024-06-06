using UnityEngine;

[CreateAssetMenu(fileName = "IceCreamLauncherDetails", menuName = "Mechanisms/IceCreamLauncherDetails", order = 1)]
public class IceCreamLauncherDetails : MechanismDetails
{
    public override MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.IceCreamLauncher;
    [Header("Projectile Prefab")]
    [Tooltip("The prefab of the ice cream projectile to be launched.")]
    public GameObject projectilePrefab;
    [Header("Projectile Details")]
    [Tooltip("The interval in seconds between launching projectiles.")]
    public float launchInterval = 2.0f;
    [Tooltip("The force to throw the icecream towards the player.")]
    public float launchForce;

    [Tooltip("The range within which the launcher can detect the player and activate.")]
    public float detectionRange = 10.0f;
    [Header("Shake Attributes for Fridge")]
    [Tooltip("Duration of the shake effect when a projectile is launched.")]
    public float shakeDuration = 0.5f;

    [Tooltip("Strength of the shake effect when a projectile is launched.")]
    public float shakeStrength = 1.0f;

    [Tooltip("Number of vibrato (oscillations) during the shake.")]
    public int shakeVibrato = 10;

    [Tooltip("Randomness of the shake movement to make it less predictable.")]
    public float shakeRandomness = 90.0f;
}
