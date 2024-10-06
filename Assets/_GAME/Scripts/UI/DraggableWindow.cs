using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] Canvas _ownerCanvas;
    RectTransform _rectTransform;


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }



}
