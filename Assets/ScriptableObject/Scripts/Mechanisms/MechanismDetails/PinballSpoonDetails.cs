using UnityEngine;

[CreateAssetMenu(fileName = "PinballSpoonDetails", menuName = "Mechanisms/PinballSpoonDetails", order = 5)]
public class PinballSpoonDetails : MechanismDetails
{
    public override MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.PinballSpoon;
    [Header("Pinball Force")]
    [Tooltip("The magnitude of the force applied to the ball when the spoon swings.")]
    public float forceMagnitude = 10.0f;
    [Header("Pinball Swing Details")]
    [Tooltip("The duration in seconds for the spoon to complete its swing motion.")]
    public float swingDuration = 0.5f;

    [Tooltip("The duration in seconds for the spoon to return to its original position.")]
    public float resetDuration = 0.5f;
    [Header("Rotation Axis For Hinge")]
    [Tooltip("The hinge axis around which the spoon will rotate. Typically set to Vector3.up or Vector3.forward depending on the setup.")]
    public Vector3 hingeAxis = Vector3.up;
}
