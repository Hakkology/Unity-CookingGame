using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BallStateController : MonoBehaviour
{
    public LayerMask wallLayer; 
    public bool isSqueezed = false;
    public Vector3 lastCollisionNormal;

    public void ExecuteCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopAllCoroutinesSafe()
    {
        StopAllCoroutines();
    }

    public void DoScale(Transform target, Vector3 targetScale, float duration)
    {
        target.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
    }

    public void DoScaleAdjustment(Transform target, Vector3 targetScale, float duration)
    {
        DOVirtual.DelayedCall(0.2f, () =>
        {
            target.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
        });
    }

    public void ChangeScale(Transform target, Vector3 targetScale, float duration) {
        if (target.localScale != targetScale) {
            target.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0 && !isSqueezed)
        {
            lastCollisionNormal = collision.contacts[0].normal;
            isSqueezed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            isSqueezed = false;
        }
    }
}
