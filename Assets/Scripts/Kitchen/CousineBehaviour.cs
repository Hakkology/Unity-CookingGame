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
        transform.localScale = normalScale;
    }

    // Scale'ı büyüt
    public void EnlargeScale()
    {
        transform.localScale = enlargedScale;
    }
}
