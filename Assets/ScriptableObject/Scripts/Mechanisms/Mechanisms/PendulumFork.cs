using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PendulumFork : IMechanism
{
    private bool isActive;
    private PendulumForkDetails details;
    private Transform selfTransform;
    private Transform childTransform;
    private MechanismTimedBehaviour timedBehaviour;

    public PendulumFork(MechanismTimedBehaviour timedBehaviour)
    {
        this.timedBehaviour = timedBehaviour;
    }

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as PendulumForkDetails;
        this.selfTransform = selfTransform;

        // Çocuk objeyi tanımla, çocuk objenin index'i 0 varsayılarak alınmıştır.
        childTransform = selfTransform.GetChild(0);

        IsActive = this.details.isActiveAtStart;
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            timedBehaviour.StartTimedAction(WaitAndActivate(delay));
        }
        else
        {
            StartPendulumMovement();
        }
    }

    private IEnumerator WaitAndActivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartPendulumMovement();
    }

    private void StartPendulumMovement()
    {
        isActive = true;
        float duration = 2f; 
        Vector3 startAngle = new Vector3(0, 0, 0); 
        Vector3 endAngle = new Vector3(0, 0, 90); 

        childTransform.localRotation = Quaternion.Euler(startAngle);

        childTransform.DOLocalRotate(endAngle, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
        childTransform.DOKill(); // DOTween animasyonunu durdur
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        // Oyuncu ile teması ele alın
    }

    public void UpdateMechanism()
    {
        // Gerektiğinde güncelleme mantığı ekleyin
    }
}
