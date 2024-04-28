using UnityEngine;

public interface IMechanism
{
    void MechanismStart(Transform selfTransform, Transform playerTransform = null);  
    void MechanismUpdate(); 
    void MechanismActivate();
    void MechanismDeactivate();
}