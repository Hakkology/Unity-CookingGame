using System.Collections;
using UnityEngine;

public class BallCoroutineController : MonoBehaviour
{
    public void ExecuteCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopAllCoroutinesSafe()
    {
        StopAllCoroutines();
    }
}