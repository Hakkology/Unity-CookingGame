using DG.Tweening;
using UnityEngine;

public class PinballSpoon : IMechanism
{
    private bool isActive;
    private PinballSpoonDetails details;
    private Transform selfTransform;
    private Transform playerTransform;
    private Rigidbody rigidbody;
    private float cooldownTimer;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null)
    {
        this.details = details as PinballSpoonDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;
        this.rigidbody = rigidBody;

        IsActive = this.details.isActiveAtStart;
        cooldownTimer = this.details.cooldown;  // Initialize the cooldown timer

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart()
    {
        SetupSpoon();
        MechanismActivate();
    }

    private void SetupSpoon()
    {
        HingeJoint hinge = selfTransform.GetComponent<HingeJoint>();
        if (!hinge) hinge = selfTransform.gameObject.AddComponent<HingeJoint>();

        hinge.useMotor = true;
        JointMotor motor = hinge.motor;
        motor.force = 1000;
        motor.targetVelocity = 90;
        motor.freeSpin = false;
        hinge.motor = motor;
        hinge.useMotor = false;  // Start with the motor off
    }

    public void MechanismUpdate()
    {
        if (IsActive)
        {
            cooldownTimer -= Time.deltaTime;
            if (Vector3.Distance(playerTransform.position, selfTransform.position) <= details.activationRange && cooldownTimer <= 0)
            {
                cooldownTimer = details.cooldown;
                ActivateSpoon();
            }
        }
    }

    private void ActivateSpoon()
    {
        HingeJoint hinge = selfTransform.GetComponent<HingeJoint>();
        hinge.useMotor = true;
        selfTransform.DOComplete(); 
        selfTransform.DORotate(new Vector3(0, 0, -90), 0.5f).OnComplete(() =>
        {
            hinge.useMotor = false;
        });
    }

    public void MechanismActivate()
    {
        IsActive = true;
    }

    public void MechanismDeactivate()
    {
        IsActive = false;
    }

    public bool CheckActivationConditions()
    {
        return cooldownTimer <= 0 && Vector3.Distance(playerTransform.position, selfTransform.position) <= details.activationRange;
    }

    public bool CheckDeactivationConditions()
    {
        return false; // Always active, does not deactivate unless manually done so
    }

    public void HandlePlayerContact()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - selfTransform.position).normalized;
            Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(direction * details.pushForce, ForceMode.Impulse);
            }
        }
    }
}
