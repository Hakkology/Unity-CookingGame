using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CousineBehaviour : MonoBehaviour
{
    public Button button;
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);
    public CousineHandler handler;

    private void Start()
    {
        if (button == null)
        {
            Debug.LogError("Button is not assigned in the Inspector");
            return;
        }

        button.onClick.AddListener(() => handler.HandleCousineSelection(this));
    }

    public void ResetScale()
    {
        StartCoroutine(ScaleOverTime(normalScale));
    }

    public void EnlargeScale()
    {
        StartCoroutine(ScaleOverTime(enlargedScale));
    }

    IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0;

        while (timeElapsed < .5f)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / .5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
