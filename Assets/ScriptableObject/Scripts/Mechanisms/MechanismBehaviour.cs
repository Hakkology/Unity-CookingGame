using UnityEngine;

public class MechanismBehaviour : MonoBehaviour
{
    public MechanismDetails details; // Assigned via the Unity Editor
    private IMechanism mechanism;

    private void Start()
    {
        mechanism = GetComponent<IMechanism>();

        if (details != null)
        {
            details.ApplyDetailsToMechanism(this);
        }

        if (details.isActiveAtStart)
        {
            mechanism.MechanismActivate();
        }
    }

    public void SetActivationState(bool isActive)
    {
        if (isActive)
            mechanism.MechanismActivate();
        else
            mechanism.MechanismDeactivate();
    }

    public void Activate()
    {
        mechanism.MechanismActivate();
    }

    public void Deactivate()
    {
        mechanism.MechanismDeactivate();
    }

    private void Update()
    {
        if (details != null && details.isActiveAtStart)
        {
            mechanism.MechanismUpdate();
        }
    }
}
