using System.Collections;
using UnityEngine;

public class JumpingPotatoes : IMechanism
{
    private bool isActive;
    private JumpingPotatoesDetails details;
    
    private Transform selfTransform;
    private Transform playerTransform;
    private Rigidbody potatoRigidbody;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null)
    {
        this.details = details as JumpingPotatoesDetails;
        this.selfTransform = selfTransform;
        this.playerTransform = playerTransform;

        IsActive = this.details.isActiveAtStart;
        potatoRigidbody = selfTransform.GetComponent<Rigidbody>();

        if (IsActive)
        {
            MechanismStart();
        }
    }

    public void MechanismStart()
    {
        if (selfTransform != null)
        {
            potatoRigidbody = selfTransform.gameObject.AddComponent<Rigidbody>();
            potatoRigidbody.isKinematic = true; // Prevent gravity influence while not jumping
        }
        MechanismActivate();
    }

    public void MechanismUpdate()
    {
        if (IsActive)
        {
            
        }
    }

    public void MechanismActivate()
    {
        IsActive = true;
        selfTransform.GetComponent<MonoBehaviour>().StartCoroutine(JumpRoutine());
    }

    public void MechanismDeactivate()
    {
        IsActive = false;
        if (potatoRigidbody != null)
        {
            GameObject.Destroy(potatoRigidbody);
        }
    }

    private IEnumerator JumpRoutine()
    {
        while (IsActive)
        {
            potatoRigidbody.isKinematic = false;
            potatoRigidbody.AddForce(Vector3.up * details.jumpHeight, ForceMode.Impulse);
            yield return new WaitForSeconds(details.jumpInterval);
            potatoRigidbody.isKinematic = true;
        }
    }

    public bool CheckActivationConditions()
    {
        return false; // Always active unless deactivated
    }

    public bool CheckDeactivationConditions()
    {
        return false; // No specific deactivation condition
    }

    public void HandlePlayerContact()
    {
        if (playerTransform != null)
        {
            Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 pushDirection = (playerTransform.position - selfTransform.position).normalized;
                playerRigidbody.AddForce(-pushDirection * details.pushForce, ForceMode.Impulse);
            }
        }
    }
}
