using UnityEngine;
using UnityEngine.Events;

public class MechanismBehaviour : MonoBehaviour
{
    public MechanismDetails details;
    private IMechanism mechanism;
    private Collider playerCollider;
    private BallHealthBehaviour playerHealth;
    private MechanismTimedBehaviour timedBehaviour;
    private Rigidbody rigidBody;

    // Unity Events for external triggers
    public UnityEvent onActivateTrigger;
    public UnityEvent onDeactivateTrigger;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        timedBehaviour = GetComponent<MechanismTimedBehaviour>();
        LevelManager.SceneHandler.OnPlayerSpawned += SetupPlayer;
    }

    private void SetupPlayer(GameObject player)
    {
        playerCollider = player.GetComponentInChildren<Collider>();
        playerHealth = player.GetComponentInChildren<BallHealthBehaviour>();
        mechanism = MechanismFactory.CreateMechanism(details, transform, playerHealth, timedBehaviour, rigidBody);
        if (mechanism != null && details.isActiveAtStart) mechanism.ActivateMechanism();
    }

    private void OnDestroy() => LevelManager.SceneHandler.OnPlayerSpawned -= SetupPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider) mechanism.HandlePlayerContact(other);
    }

    private void Update()
    {
        if (mechanism == null || !mechanism.IsActive) return;
        mechanism.UpdateMechanism();
    }
    public IMechanism GetMechanism() => mechanism;
    public void Activate() => mechanism?.ActivateMechanism();
    public void Deactivate() => mechanism?.DeactivateMechanism();
    public void HandlePlayerEnter()
    {
        if (details.activationType == MechanismDetails.ActivationType.ActivateOnEnter) mechanism.ActivateMechanism();
    }

    public void HandlePlayerExit()
    {
        if (details.activationType == MechanismDetails.ActivationType.DeactivateOnExit) mechanism.DeactivateMechanism();
    }
}
