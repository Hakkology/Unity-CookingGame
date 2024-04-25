using UnityEngine;

[CreateAssetMenu(fileName = "MovementModifiers", menuName = "Player/MovementModifiers", order = 1)]
public class BallMovementModifiers : ScriptableObject
{
    public float MaxForce ;
    public float MaxMoveSpeed;
    public float MaxRotationSpeed;
    public float MaxJumpForce;

    public float WaterMoveSpeed;
    public float WaterForce;
    public float WaterRotationSpeed;
    public float WaterMinRotationSpeed;
    public float WaterJumpForce;
    public float WaterResistanceForce;

    public float SlideMoveSpeed;
    public float SlideEfficiencyConstant;
    public float SlideMinSpeed;
    public float SlideRotationSpeed;

    public float FlyingForce;
    public float FlyingDownScale;


    public float Epsilon;
    public Vector3 ballDefaultScale;
    public float ballScaleAdjustmentDuration;
    public float scaleImpactFactor;
    public float scaleRecoveryDuration;
}
