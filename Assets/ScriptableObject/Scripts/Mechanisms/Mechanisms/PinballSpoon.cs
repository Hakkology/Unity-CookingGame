using DG.Tweening;
using UnityEngine;

public class PinballSpoon : IMechanism
{
    private bool isActive;
    private PinballSpoonDetails details;
    private Transform selfTransform;
    private Rigidbody rigidbody;
    private HingeJoint hingeJoint;
    private MechanismTimedBehaviour timedBehaviour;

    public PinballSpoon(MechanismTimedBehaviour timedBehaviour, Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
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
        this.hingeJoint = selfTransform.GetComponent<HingeJoint>();
        if (hingeJoint == null)
        {
            hingeJoint = selfTransform.gameObject.AddComponent<HingeJoint>();
            hingeJoint.axis = Vector3.up; // Örnek olarak Y ekseni etrafında dönüş
            hingeJoint.useSpring = true;
        }
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
        // Hinge hareketini simüle etmek için DOTween kullanılabilir.
        // Örnek olarak 90 derece dönme:
        selfTransform.DOLocalRotate(new Vector3(0, 0, 90), details.swingDuration).SetRelative(true).OnComplete(() => ResetSpoon());
    }

    private void ResetSpoon()
    {
        // Kaşık orijinal pozisyonuna yavaşça döner
        selfTransform.DOLocalRotate(Vector3.zero, details.resetDuration);
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (isActive)
        {
            Rigidbody playerRigidbody = playerCollider.attachedRigidbody;
            if (playerRigidbody != null)
            {
                // Topa bir kuvvet uygula
                playerRigidbody.AddForce(selfTransform.up * details.forceMagnitude, ForceMode.Impulse);
                ActivateMechanism();
            }
        }
    }

    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
        if (delay > 0)
        {
            timedBehaviour.StartDOTweenAction(DOTween.Sequence().AppendInterval(delay).AppendCallback(() => isActive = true));
        }
    }

    public void UpdateMechanism()
    {
        // Gerekirse güncelleme mantığı ekleyin
    }
}
