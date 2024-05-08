using UnityEngine;

public class MechanismTrapdoorBehaviour : MonoBehaviour
{
    [SerializeField]
    private MechanismBehaviour mechanismBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) mechanismBehaviour.HandlePlayerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) mechanismBehaviour.HandlePlayerExit();
    }
}
