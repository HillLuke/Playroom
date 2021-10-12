using Assets.Scripts.Inventory;
using Assets.Scripts.UI;
using System.Collections;
using TMPro;
using UnityEngine;


public class UITooltip : UIBase
{
    [Header("Tooltip Objects")]
    public GameObject Inner;
    public TextMeshProUGUI Title;

    private bool _isShown;
    private RectTransform _rectTransform;
    private Canvas _canvas; 
    private Vector2 _positionOffset = Vector2.zero;
    private CanvasGroup _canvasGroup;

    protected override void Setup()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _canvas = gameObject.GetComponentInParent<Canvas>();
        _canvasGroup = gameObject.GetComponentInParent<CanvasGroup>();

        _canvasGroup.alpha = 0f;

        _uIManager.ActionTooltipItem += TooltipItem;

        base.Setup();
    }

    private void TooltipItem(Item item)
    {
        if (item != null)
        {
            Title.SetText(item.ItemData.ItemName);
            _isShown = true;
            SetPosition();
            _canvasGroup.alpha = 1f;
        }
        else
        {
            _canvasGroup.alpha = 0f;
            _isShown = false;
        }
    }

    private void Update()
    {
        if (_isShown)
        {
            SetPosition();
        }
    }

    private void SetPosition()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out pos);
        Vector2 offset = Vector2.zero;

        if (Input.mousePosition.x < _rectTransform.sizeDelta.x)
        {
            offset += new Vector2(_rectTransform.sizeDelta.x * 0.5f, 0);
        }
        else
        {
            offset += new Vector2(-_rectTransform.sizeDelta.x * 0.5f, 0);
        }
        if (Screen.height - Input.mousePosition.y > _rectTransform.sizeDelta.y)
        {
            offset += new Vector2(0, _rectTransform.sizeDelta.y * 0.5f);
        }
        else
        {
            offset += new Vector2(0, -_rectTransform.sizeDelta.y * 0.5f);
        }
        pos = pos + offset + _positionOffset;
        transform.position = _canvas.transform.TransformPoint(pos);
    }
}
