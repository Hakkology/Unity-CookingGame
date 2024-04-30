using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollClickResolver : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging;
    private float pointerDownTime;

    void Start(){
        //LayoutRebuilder.ForceRebuildLayoutImmediate();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging && Time.time - pointerDownTime < 0.2f)
        {
            OnClick();
        }
        isDragging = false; // Her ihtimale karşı burada sıfırla
    }

    private void OnClick()
    {
        Debug.Log("Panel clicked without dragging!");
    }
}
