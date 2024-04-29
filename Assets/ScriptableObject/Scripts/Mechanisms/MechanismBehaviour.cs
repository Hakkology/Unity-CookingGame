using UnityEngine;
using UnityEngine.Events;

public class MechanismBehaviour : MonoBehaviour
{
    public MechanismDetails details;
    private IMechanism mechanism;

    // Unity Events
    public UnityEvent onActivateTrigger;
    public UnityEvent onDeactivateTrigger;

    private void Awake()
    {
        mechanism = MechanismFactory.CreateMechanism(details, details.mechanismType, transform);
        if (mechanism != null)
        {
            mechanism.Initialize(details, transform);
        }
    }

    private void OnEnable()
    {
        onActivateTrigger.AddListener(Activate);
        onDeactivateTrigger.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        onActivateTrigger.RemoveListener(Activate);
        onDeactivateTrigger.RemoveListener(Deactivate);
    }

    void Update()
    {
        if (mechanism == null) return;

        if (!mechanism.IsActive)
        { 
            if (mechanism.CheckActivationConditions())
                Activate();
        }
        else
        {
            if (mechanism.CheckDeactivationConditions())
                Deactivate();
        }
    }

    public void Activate()
    {
        if (mechanism == null) return;

        CancelInvoke(nameof(DoMechanismActivate));
        CancelInvoke(nameof(DoMechanismDeactivate));

        if (details.activationDelay > 0)
            Invoke(nameof(DoMechanismActivate), details.activationDelay);
        else
            DoMechanismActivate();
    }

    public void Deactivate()
    {
        if (mechanism == null) return;

        CancelInvoke(nameof(DoMechanismActivate));
        CancelInvoke(nameof(DoMechanismDeactivate));

        if (details.deactivationDelay > 0)
            Invoke(nameof(DoMechanismDeactivate), details.deactivationDelay);
        else
            DoMechanismDeactivate();
    }

    private void DoMechanismActivate()
    {
        mechanism.MechanismActivate();
    }

    private void DoMechanismDeactivate()
    {
        mechanism.MechanismDeactivate();
    }
}
