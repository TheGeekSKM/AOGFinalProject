using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class TutorialManager : MonoBehaviour
{
    [Header("Main Panel")]
    [SerializeField] RectTransform _tutorialPanel;

    [Header("Tutorial Panels")]
    [SerializeField] RectTransform _ndaPanel;
    [SerializeField] RectTransform _uiPanel;
    [SerializeField] RectTransform _movementPanel;
    [SerializeField] RectTransform _combatPanel;
    [SerializeField] RectTransform _inventoryPanel;
    [SerializeField] RectTransform _variablePanel;

    [Header("Additional Panels")]
    [SerializeField] RectTransform _screenBlocker; 

    bool _canBeClosed = false;
    public void CloseButton()
    {
        if (_canBeClosed)
        {
            _tutorialPanel.DOAnchorPosY(-1080f, 0.5f).SetEase(Ease.OutBack);
        }
        else
        {
            _tutorialPanel.DOShakeScale(0.5f, 0.1f, 10, 90);
            // WarningManager.Instance.ShowWarning("You can't close the tutorial yet!", 2f);
        }
    }

    public void ShowNDAPanel() => _ndaPanel.gameObject.SetActive(true);
    public void ShowUIPanel() {
        _uiPanel.gameObject.SetActive(true);
        _ndaPanel.gameObject.SetActive(false);
        _canBeClosed = true;
    }
    public void ShowMovementPanel() {
        _movementPanel.gameObject.SetActive(true);
        _uiPanel.gameObject.SetActive(false);
    }
    public void ShowCombatPanel() {
        _combatPanel.gameObject.SetActive(true);
        _movementPanel.gameObject.SetActive(false);
    }
    public void ShowInventoryPanel() {
        _inventoryPanel.gameObject.SetActive(true);
        _combatPanel.gameObject.SetActive(false);
    } 
    public void ShowVariablePanel() {
        _variablePanel.gameObject.SetActive(true);
        _inventoryPanel.gameObject.SetActive(false);
    }

    public void BlockScreen() => _screenBlocker.DOAnchorPosY(0f, 0.5f).SetEase(Ease.InBack);
    public void UnblockScreen() => _screenBlocker.gameObject.SetActive(false);
}
