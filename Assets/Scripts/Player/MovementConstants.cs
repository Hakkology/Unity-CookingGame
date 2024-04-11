public static class MovementConstants
{
    public const float MaxMoveSpeed = 12.0f;
    public const float MaxRotationSpeed = 720.0f;

    public const float WaterMoveSpeed = MaxMoveSpeed * 0.6f;  // 60% of maximum speed on water
    public const float WaterRotationSpeed = MaxRotationSpeed * 0.6f;  // Adjusted rotation speed on water
    public const float WaterMinRotationSpeed = 100.0f; // Water Minimal Rotation

    public const float SlideMoveSpeed = MaxMoveSpeed * 0.1f;  // 10% of maximum speed on water
    public const float SlideRotationSpeed = MaxRotationSpeed * 0.1f;  // Adjusted rotation speed on water
    public const float SlideEfficiencyConstant = 0.1f;

    public const float Epsilon = 0.001f;
}
