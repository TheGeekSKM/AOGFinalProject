using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCoordinateDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _image;
    [SerializeField] RectTransform _rectTransform;
    // [SerializeField] float _displayTime = 10f;
    [SerializeField] Vector2 _offScreenAnchoredPosition;

    Transform _coordinateParent;


    void Awake()
    {
        if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        _rectTransform.anchoredPosition = _offScreenAnchoredPosition;
        _coordinateParent = null;
    }
    public void Initialize(Transform coordinateParent)
    {
        _coordinateParent = coordinateParent;
        var coords = Vector2Int.RoundToInt(new Vector2(coordinateParent.position.x, coordinateParent.position.z));
        _text.text = $"[{coords.x}, {coords.y}]";
        GameCanvasManager.Instance.SetVariable("click", $"{coords.x}, {coords.y}");
        
    }

    void Update()
    {
        if (_coordinateParent == null) return;
        _rectTransform.position = Camera.main.WorldToScreenPoint(_coordinateParent.position);
    }

    public void Hide()
    {
        _rectTransform.anchoredPosition = _offScreenAnchoredPosition;
        _coordinateParent = null;
    }

    
     
}
