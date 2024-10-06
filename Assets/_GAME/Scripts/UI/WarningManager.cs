using System.Collections;
using SaiUtils.Singleton;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class WarningManager : Singleton<WarningManager>
{
    [SerializeField] TextMeshProUGUI _warningText;
    [SerializeField] RectTransform _warningPanel;
    Coroutine _hideWarningCoroutine;

    public void ShowWarning(string message, float duration)
    {
        Debug.Log(message);
        _warningText.text = message;

        _warningPanel.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);
        if (_hideWarningCoroutine != null) StopCoroutine(_hideWarningCoroutine);
        StartCoroutine(HideWarning(duration));
    }

    private IEnumerator HideWarning(float duration = 3f)
    {
        yield return new WaitForSeconds(duration);
        // move underneath the screen
        _warningPanel.DOAnchorPosY(-1080, 0.5f).SetEase(Ease.InBack);
    }
}
