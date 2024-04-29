using UnityEngine;

[CreateAssetMenu(fileName = "PinballSpoonDetails", menuName = "Mechanisms/PinballSpoonDetails", order = 8)]
public class PinballSpoonDetails : MechanismDetails
{
    [Tooltip("The force with which the spoon pushes the player.")]
    public float pushForce = 10.0f;

    [Tooltip("The range within which the spoon activates when the player approaches.")]
    public float activationRange = 5.0f;

    [Tooltip("Cooldown duration between activations in seconds.")]
    public float cooldown = 2.0f;
}
