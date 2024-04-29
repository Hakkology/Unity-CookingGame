using UnityEngine;

public interface IMechanism
{
    bool IsActive { get; set; }
    void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null, Rigidbody rigidBody = null);
    void MechanismStart();  
    void MechanismUpdate(); 
    void MechanismActivate();
    void MechanismDeactivate();
    void HandlePlayerContact();
    bool CheckActivationConditions();
    bool CheckDeactivationConditions();

}
