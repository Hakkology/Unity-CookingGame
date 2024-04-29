using UnityEngine;

public interface IMechanism
{
    bool IsActive { get; set; }
    void Initialize(MechanismDetails details, Transform selfTransform, Transform playerTransform = null);
    void MechanismStart();  
    void MechanismUpdate(); 
    void MechanismActivate();
    void MechanismDeactivate();
    bool CheckActivationConditions();
    bool CheckDeactivationConditions();
}
