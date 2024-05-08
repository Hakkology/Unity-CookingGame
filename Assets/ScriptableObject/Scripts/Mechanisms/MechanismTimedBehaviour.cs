using UnityEngine;
using System.Collections;

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
}
