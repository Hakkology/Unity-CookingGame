using UnityEngine;

[CreateAssetMenu(fileName = "JumpingPotatoesDetails", menuName = "Mechanisms/JumpingPotatoesDetails", order = 5)]
public class JumpingPotatoesDetails : MechanismDetails
{
    [Tooltip("The height to which the potatoes jump.")]
    public float jumpHeight;

    [Tooltip("The interval between jumps in seconds.")]
    public float jumpInterval;

    [Tooltip("Force applied to the player when making contact.")]
    public float pushForce;
}