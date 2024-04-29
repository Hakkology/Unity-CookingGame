using UnityEngine;

public class MechanismTrapdoorBehaviour : MonoBehaviour
{
    private RainOfKnives mechanism;

    public void Initialize(RainOfKnives mechanism)
    {
        this.mechanism = mechanism;
    }

    private void OnTriggerEnter(Collider other)
    {
        mechanism.HandlePlayerContact();
    }

    private void OnTriggerExit(Collider other)
    {
        mechanism.MechanismActivate();
    }
}
