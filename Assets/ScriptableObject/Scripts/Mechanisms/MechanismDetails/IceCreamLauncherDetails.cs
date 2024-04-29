using UnityEngine;

[CreateAssetMenu(fileName = "IceCreamLauncherDetails", menuName = "Mechanisms/IceCreamLauncherDetails", order = 6)]
public class IceCreamLauncherDetails : MechanismDetails
{
    [Tooltip("Prefab of the ice cream projectile.")]
    public GameObject iceCreamPrefab;

    [Tooltip("Prefab of the fridge from which ice creams are launched.")]
    public GameObject fridgePrefab;

    [Tooltip("Range within which the fridge activates and starts launching ice creams.")]
    public float activationRange = 10f;

    [Tooltip("Damage dealt to the player on contact with the ice cream.")]
    public float damage = 10f;

    [Tooltip("Interval between spawned Icecreams.")]
    public float launchInterval = 2f; // Interval in seconds between launches
}
