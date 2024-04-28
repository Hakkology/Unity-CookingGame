using UnityEngine;

public class MagnetsInKitchen : MonoBehaviour, IMechanism
{
    public void MechanismStart(Transform selfTransform, Transform playerTransform = null)
    {
        // Initialize flame position and properties
    }

    public void MechanismUpdate()
    {
        // Flame behavior like moving, growing, or collision checks
    }
    public void MechanismActivate()
    {
        throw new System.NotImplementedException();
    }

    public void MechanismDeactivate()
    {
        throw new System.NotImplementedException();
    }


}
