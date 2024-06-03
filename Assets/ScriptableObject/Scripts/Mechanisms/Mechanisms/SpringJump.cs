using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SpringJump : IMechanism
{
    private bool isActive;
    private bool isTriggered;
    private SpringJumpDetails details;
    private Transform selfTransform;
    private Rigidbody playerRigidbody;
    private MechanismTimedBehaviour timedBehaviour;

    public SpringJump(MechanismTimedBehaviour timedBehaviour)
    {
        this.timedBehaviour = timedBehaviour;
        playerRigidbody = BallManager.Instance.PlayerRigidbody;
        isTriggered = false;
    }

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public void InitializeMechanism(MechanismDetails details, Transform selfTransform)
    {
        this.details = details as SpringJumpDetails;
        this.selfTransform = selfTransform;
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (!isTriggered)
        {
            SpringAction();
        }
    }

    private void SpringAction()
    {
        isTriggered = true;

        Vector3 originalScale = selfTransform.localScale;
        Vector3 originalPosition = selfTransform.position;
        float scaleFactor = details.scaleFactor;
        float jumpForce = details.jumpForce;

        selfTransform.DOScaleY(originalScale.y * scaleFactor, details.scaleAnimationDuration).OnComplete(() =>
        {
            ResetSpring(originalScale, originalPosition);
        });

        selfTransform.DOLocalMoveY(originalPosition.y + (originalScale.y * scaleFactor) / 2, details.scaleAnimationDuration);
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetSpring(Vector3 originalScale, Vector3 originalPosition)
    {
        selfTransform.DOScale(originalScale, details.resetAnimationDuration);
        selfTransform.DOMove(originalPosition, details.resetAnimationDuration).OnComplete(() =>
        {
            ReactivateAfterDelay(details.reactivationDelay);
        });
    }

    private void ReactivateAfterDelay(float delay)
    {
        timedBehaviour.StartCoroutine(DelayedReactivation(delay));
    }

    private IEnumerator DelayedReactivation(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTriggered = false;
    }

    public void DeactivateMechanism(float delay = 0)
    {
        isActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (!isTriggered)  // Sadece isTriggered false ise tetikle
        {
            ActivateMechanism();
        }
    }

    public void UpdateMechanism()
    {
        // Optional: Add any update logic if necessary
    }
}
