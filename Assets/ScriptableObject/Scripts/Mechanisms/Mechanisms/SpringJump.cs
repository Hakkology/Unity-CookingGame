using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SpringJump : IMechanism
{
    private bool isActive;
    private SpringJumpDetails details;
    private Transform selfTransform;
    private Rigidbody playerRigidbody; 
    private MechanismTimedBehaviour timedBehaviour;

    public SpringJump(MechanismTimedBehaviour timedBehaviour)
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
        this.details = details as SpringJumpDetails;
        this.selfTransform = selfTransform;
    }

    public void ActivateMechanism(float delay = 0)
    {
        if (delay > 0)
        {
            timedBehaviour.StartDOTweenAction(DOTween.Sequence().AppendInterval(delay).AppendCallback(() => SpringAction()));
        }
        else
        {
            SpringAction();
        }
    }

    private IEnumerator ActivateWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpringAction();
    }

    private void SpringAction()
    {
        // Scale the spring model up and move it upward
        Vector3 originalScale = selfTransform.localScale;
        Vector3 originalPosition = selfTransform.position;
        float scaleFactor = details.scaleFactor;
        float jumpForce = details.jumpForce;

        selfTransform.DOScaleY(originalScale.y * scaleFactor, details.scaleAnimationDuration)
            .OnComplete(() => {
                ResetSpring(originalScale, originalPosition);
            });

        selfTransform.DOLocalMoveY(originalPosition.y + (originalScale.y * scaleFactor) / 2, details.scaleAnimationDuration);

        // Add force to the rigidbody to simulate jump
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isActive = false; // Spring needs to reset before it can be used again
        timedBehaviour.StartTimedAction(ReactivateAfterDelay(details.reactivationDelay));
    }

    private void ResetSpring(Vector3 originalScale, Vector3 originalPosition)
    {
        selfTransform.DOScale(originalScale, details.resetAnimationDuration);
        selfTransform.DOMove(originalPosition, details.resetAnimationDuration);
    }

    private IEnumerator ReactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isActive = true;
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

    private IEnumerator DeactivateWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isActive = false;
    }

    public void HandlePlayerContact(Collider playerCollider)
    {
        if (isActive && playerCollider.attachedRigidbody != null)
        {
            playerRigidbody = playerCollider.attachedRigidbody;
            ActivateMechanism();
        }
    }

    public void UpdateMechanism()
    {
        // Update logic if necessary
    }
}
