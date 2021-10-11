using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 _pointerOffset;
    private RectTransform _canvas;

    [SerializeField]
    private RectTransform _panel;


    void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            _canvas = canvas.transform as RectTransform;
            if (_panel == null)
            {
                _panel = transform.parent as RectTransform;
            }
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        _panel.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_panel, data.position, data.pressEventCamera, out _pointerOffset);
    }

    public void OnDrag(PointerEventData data)
    {
        if (_panel == null)
            return;

        Vector2 pointerPostion = ClampToWindow(data);

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas, pointerPostion, data.pressEventCamera, out localPointerPosition
            ))
        {
            _panel.localPosition = localPointerPosition - _pointerOffset;
        }
    }

    Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        _canvas.GetWorldCorners(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }
}
