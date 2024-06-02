using DG.Tweening;
using UnityEngine;

public class PinballSpoon : IMechanism
{
    private bool isActive;
    private PinballSpoonDetails details;
    private Transform selfTransform;
    private MechanismTimedBehaviour timedBehaviour;

    public PinballSpoon(MechanismTimedBehaviour timedBehaviour)
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
        this.details = details as PinballSpoonDetails;
        this.selfTransform = selfTransform;
        IsActive = this.details.isActiveAtStart;
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            timedBehaviour.StartDOTweenAction(DOTween.Sequence().AppendInterval(delay).AppendCallback(() => SwingAction()));
        }
        else
        {
            SwingAction();
        }
    }

    private void SwingAction()
    {
        isActive = true;
        // 90 derece dönüş, orijinal pozisyondan başlar
        selfTransform.DOLocalRotate(new Vector3(0, -90, 0), details.swingDuration)
            .SetRelative(true)
            .OnComplete(ResetSpoon);
    }

    private void ResetSpoon()
    {
        // Başlangıç rotasyonuna dönüş
        selfTransform.DOLocalRotate(Vector3.zero, details.resetDuration).OnComplete(() => isActive = false);
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (!isActive)
        {
            Debug.Log("Player has contacted the pinball spoon. Activating swing.");
            ActivateMechanism(0);  // Activate the swing without delay
        }
    }

    public void DeactivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            timedBehaviour.StartDOTweenAction(DOTween.Sequence().AppendInterval(delay).AppendCallback(() => isActive = false));
        }
        else
        {
            isActive = false;
        }
    }

    public void UpdateMechanism()
    {
        // Gerektiğinde güncelleme mantığı ekleyin
    }
}
