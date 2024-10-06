using DG.Tweening;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] RectTransform _rectTransform;
    public void HoverEnter()
    {
        _rectTransform.DOScale(1.1f, 0f);
        // _rectTransform.DORotate(new Vector3(0, 0, 10), 0.2f);
    }

    public void HoverExit()
    {
        _rectTransform.DOScale(1f, 0f);
        // _rectTransform.DORotate(new Vector3(0, 0, 0), 0.2f);
    }

    public void Click()
    {
        _rectTransform.DOScale(0.9f, 0.1f).OnComplete(() => _rectTransform.DOScale(1f, 0.1f));
    }
}
