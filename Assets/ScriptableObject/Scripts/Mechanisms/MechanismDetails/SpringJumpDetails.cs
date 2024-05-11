using UnityEngine;

[CreateAssetMenu(fileName = "SpringJumpDetails", menuName = "Mechanisms/SpringJumpDetails", order = 6)]
public class SpringJumpDetails : MechanismDetails
{
    public override MechanismFactory.MechanismType MechanismType => MechanismFactory.MechanismType.SpringJump;
    [Header("Spring Factors")]
    [Tooltip("The scale factor to which the spring will stretch vertically.")]
    public float scaleFactor = 2.0f;

    [Tooltip("The vertical force applied to the player when activated.")]
    public float jumpForce = 10.0f;
    [Header("Spring Time Factors")]
    [Tooltip("Duration in seconds for the spring scaling animation.")]
    public float scaleAnimationDuration = 0.5f;

    [Tooltip("Duration in seconds for the spring to return to its original scale and position.")]
    public float resetAnimationDuration = 0.5f;

    [Tooltip("Time in seconds before the spring can be reactivated after being used.")]
    public float reactivationDelay = 3.0f;
}
