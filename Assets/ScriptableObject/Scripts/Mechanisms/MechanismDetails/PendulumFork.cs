using UnityEngine;

[CreateAssetMenu(fileName = "PendulumForkDetails", menuName = "Mechanisms/PendulumForkDetails", order = 7)]
public class PendulumForkDetails : MechanismDetails
{
    public override MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.PendulumFork;
    [Header("Pendulum Attributes")]
    [Tooltip("The initial force applied to start the pendulum motion.")]
    public float initialForce;
}
