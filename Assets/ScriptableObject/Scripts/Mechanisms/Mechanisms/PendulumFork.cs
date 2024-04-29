using UnityEngine;

public class PendulumFork : IMechanism
{
    private bool isActive;
    private PendulumForkDetails details;
    private Transform selfTransform;
    private Rigidbody rigidbody; // Using the passed Rigidbody
    private Transform playerTransform;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null)
    {
        this.details = details as PendulumForkDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;
        this.rigidbody = rigidBody; // Set the Rigidbody from the parameter

        IsActive = this.details.isActiveAtStart;

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart()
    {
        SetupPendulum();
        MechanismActivate();
    }
    private void SetupPendulum()
    {
        // Setup hinge joint to allow realistic pendulum motion
        HingeJoint hinge = selfTransform.GetComponent<HingeJoint>();
        if (!hinge) hinge = selfTransform.gameObject.AddComponent<HingeJoint>();
        
        hinge.connectedBody = null;  // Assuming no connected body above
        hinge.autoConfigureConnectedAnchor = false;
        hinge.anchor = Vector3.zero;
        hinge.connectedAnchor = new Vector3(selfTransform.position.x, selfTransform.position.y + details.armLength, selfTransform.position.z);

        // Setting limits to make it swing only to a realistic angle
        JointLimits limits = new JointLimits();
        limits.min = -30;  // Min angle
        limits.max = 30;   // Max angle
        hinge.limits = limits;
        hinge.useLimits = true;

        // Configuring the rigidbody for pendulum dynamics
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.mass = 2;  // Set appropriate mass
        rigidbody.angularDrag = 0.5f;  // Adjust drag to control damping

        // Set the initial push
        rigidbody.AddForce(new Vector3(100, 0, 0));  // Adjust force direction and magnitude as needed
    }

    public void MechanismUpdate()
    {
        // Update behavior can include physics interactions or visual updates
    }

    public void MechanismActivate()
    {
        IsActive = true;
        // Activation could adjust physical properties if needed
    }

    public void MechanismDeactivate()
    {
        IsActive = false;
        // Clean up or reset the mechanism
    }

    public bool CheckActivationConditions()
    {
        // Check specific conditions for activation
        return false; // Placeholder
    }

    public bool CheckDeactivationConditions()
    {
        // Check specific conditions for deactivation
        return false; // Placeholder
    }

    public void HandlePlayerContact()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - selfTransform.position).normalized;
            Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(-direction * details.impactForce, ForceMode.Impulse);
            }
        }
    }
}
