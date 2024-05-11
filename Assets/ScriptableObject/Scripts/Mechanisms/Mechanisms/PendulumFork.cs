using System.Collections;
using UnityEngine;

public class PendulumFork : IMechanism
{
    private bool isActive;
    private PendulumForkDetails details;
    private Transform selfTransform;
    private Rigidbody rigidbody;
    private MechanismTimedBehaviour timedBehaviour;
    private HingeJoint hingeJoint;

    public PendulumFork(MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.timedBehaviour = timedBehaviour;
        this.rigidbody = rigidbody;
    }

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as PendulumForkDetails;
        this.selfTransform = selfTransform;

        // Ensure the Rigidbody and HingeJoint are correctly configured
        SetupHingeJoint();

        IsActive = this.details.isActiveAtStart;
    }

    private void SetupHingeJoint()
    {
        hingeJoint = selfTransform.GetComponent<HingeJoint>();
        if (hingeJoint == null)
        {
            hingeJoint = selfTransform.gameObject.AddComponent<HingeJoint>();
        }

        hingeJoint.connectedBody = null; // The fork is hanging from a fixed point in space
        hingeJoint.axis = Vector3.forward; // Rotate around the forward axis
        hingeJoint.anchor = new Vector3(0, 1, 0); // Set the anchor to the top part of the fork
        hingeJoint.useSpring = true;
        hingeJoint.spring = new JointSpring { spring = details.springForce, damper = details.damper, targetPosition = 0 };
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            // Start the pendulum movement after a delay
            timedBehaviour.StartTimedAction(WaitAndActivate(delay));
        }
        else
        {
            StartPendulumMovement();
        }
    }

    private IEnumerator WaitAndActivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartPendulumMovement();
    }

    private void StartPendulumMovement()
    {
        // Initial force to start the pendulum
        rigidbody.AddForce(selfTransform.right * details.initialForce, ForceMode.Impulse);
        IsActive = true;
    }

    public void DeactivateMechanism(float delay = 0)
    {
        IsActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        // Handle what happens when the player contacts the pendulum fork
    }

    public void UpdateMechanism()
    {
        // Update logic if necessary
    }
}
