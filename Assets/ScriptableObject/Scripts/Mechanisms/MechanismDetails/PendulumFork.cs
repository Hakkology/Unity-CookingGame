using UnityEngine;

[CreateAssetMenu(fileName = "PendulumForkDetails", menuName = "Mechanisms/PendulumForkDetails", order = 7)]
public class PendulumForkDetails : MechanismDetails
{
    public override MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.PendulumFork;
    [Header("Pendulum Attributes")]
    [Tooltip("The initial force applied to start the pendulum motion.")]
    public float initialForce = 5.0f;

    [Tooltip("The spring force to apply in the HingeJoint.")]
    public float springForce = 20.0f;

    [Tooltip("The damping to apply to the spring in the HingeJoint.")]
    public float damper = 5.0f;
}
