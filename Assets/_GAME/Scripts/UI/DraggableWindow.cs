using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] Button _closeButton;
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas _ownerCanvas;
    RectTransform _rectTransform;

    void OnEnable() {
        _closeButton.onClick.AddListener(() => {
            if (_ownerCanvas != canvas)
            {
                _ownerCanvas.gameObject.SetActive(false);
            }
        });
    }

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void OnDisable() {
        _closeButton.onClick.RemoveAllListeners();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }



}
