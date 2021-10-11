using Assets.Scripts.UI.ItemCollections;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : Singleton<DragManager>
{
    public DragObject DragObject { get { return _dragObject; } }

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private RectTransform _rectTransform;
    private DragObject _dragObject;

    public void SetDragObject(UIItemSlot itemSlot)
    {
        if (itemSlot != null)
        {
            _dragObject = new DragObject(itemSlot);
            _rectTransform.SetAsLastSibling();
            _image.sprite = itemSlot.Item.ItemData.Icon;
            var rect = itemSlot.SlotIcon.GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(rect.rect.width, rect.rect.height);
            _canvasGroup.alpha = 1f;
            Debug.Log("SetDragObject");
        }
        else
        {
            ClearDragObject();
        }
    }

    public void ClearDragObject()
    {
        _canvasGroup.alpha = 0f;
        _dragObject = null;
        Debug.Log("ClearDragObject");
    }

    protected override void Start()
    {
        _image = GetComponent<Image>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        base.Start();
    }

    private void Update()
    {
        if (_dragObject != null)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out pos);
            transform.position = _canvas.transform.TransformPoint(pos);
        }
    }
}
