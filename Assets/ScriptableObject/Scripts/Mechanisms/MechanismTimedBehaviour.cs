using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MechanismTimedBehaviour : MonoBehaviour
{
    public Coroutine StartTimedAction(IEnumerator action)
    {
        return StartCoroutine(action);
    }

    public void StopTimedAction(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    public Coroutine StartDOTweenAction(Tween tween)
    {
        return StartCoroutine(WaitForCompletion(tween));
    }

    private IEnumerator WaitForCompletion(Tween tween)
    {
        yield return tween.WaitForCompletion();
    }
}
