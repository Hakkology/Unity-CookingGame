using UnityEngine;
using UnityEngine.Events;

public class MechanismBehaviour : MonoBehaviour
{
    public MechanismDetails details;
    public GameObject player;
    private IMechanism mechanism;
    private Collider playerCollider;
    private Rigidbody rigidBody;

    // Unity Events
    public UnityEvent onActivateTrigger;
    public UnityEvent onDeactivateTrigger;

    private void Awake()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player"); 
        playerCollider = player.GetComponent<Collider>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        mechanism = MechanismFactory.CreateMechanism(details, details.mechanismType, transform);
        if (mechanism != null) mechanism.Initialize(details, player?.transform, player.transform, rigidBody);
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

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) mechanism.HandlePlayerContact();  // A method to be implemented based on your mechanism's logic
    }  
        
    

    void Update()
    {
        if (mechanism == null || !mechanism.IsActive) return;
        if (mechanism.CheckActivationConditions()) Activate();
        else if (mechanism.CheckDeactivationConditions()) Deactivate();
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
    private void DoMechanismActivate() => mechanism.MechanismActivate();
    private void DoMechanismDeactivate() => mechanism.MechanismDeactivate();

}
