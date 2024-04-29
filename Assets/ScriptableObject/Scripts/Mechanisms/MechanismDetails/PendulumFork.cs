using UnityEngine;

[CreateAssetMenu(fileName = "PendulumForkDetails", menuName = "Mechanisms/PendulumForkDetails", order = 7)]
public class PendulumForkDetails : MechanismDetails
{
    [Tooltip("The force with which the pendulum hits the player.")]
    public float impactForce = 10.0f;

    [Tooltip("Length of the pendulum arm.")]
    public float armLength = 5.0f;
}