using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 dragOffset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Calculate the offset between mouse position and window position
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out mousePos);

        dragOffset = rectTransform.localPosition - (Vector3)mousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert mouse position to local coordinates
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out mousePos);

        // Update position while maintaining the initial offset
        rectTransform.localPosition = (Vector3)mousePos + dragOffset;
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }
}
