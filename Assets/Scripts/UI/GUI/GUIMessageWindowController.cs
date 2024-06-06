using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GUIMessageWindowController : MonoBehaviour
{
    public TextMeshProUGUI messageText; 
    public void Initialize(Tool toolData)
    {
        messageText.text = "You need to collect the " + toolData.toolName + " tool first!";
        ShowWithAnimation();
    }

    private void ShowWithAnimation()
    {
        // Panelin RectTransform bileşenini al
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Başlangıç konumunu ekranın alt ortasına, görünmeyen bir şekilde ayarla
        float initialY = -rectTransform.rect.height; 
        rectTransform.anchoredPosition = new Vector2(0, initialY);

        // DOTween animasyon dizisi
        float moveUpAmount = rectTransform.rect.height / 2; 
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosY(initialY + moveUpAmount, 0.5f))  
                .AppendInterval(1.5f)  
                .Append(rectTransform.DOAnchorPosY(initialY, 0.5f)) 
                .OnComplete(() => gameObject.SetActive(false));  
    }




}
