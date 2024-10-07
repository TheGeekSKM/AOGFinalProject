using System.Collections.Generic;
using DG.Tweening;
using SaiUtils.Singleton;
using TMPro;
using UnityEngine;

public class GameCanvasManager : Singleton<GameCanvasManager>
{
    [Header("Inventory")]
    [SerializeField] InventoryDisplayController _inventoryCanvas;
    [SerializeField] RectTransform _inventoryCanvasRectTransform;

    [Header("Targets")]
    [SerializeField] RectTransform _targetPanelParent;
    [SerializeField] TargetPanelController _targetPanelPrefab;
    [SerializeField] List<Transform> _targets = new List<Transform>();
    [SerializeField] List<RectTransform> _targetPanels = new List<RectTransform>();

    public void SetInventoryVisible() 
    {
        _inventoryCanvasRectTransform.DOAnchorPosX(0, 0.2f).SetEase(Ease.OutQuart);
        _inventoryCanvas.Initialize();
    }

    public void SetInventoryInvisible()
    {
        _inventoryCanvasRectTransform.DOAnchorPosX(-1920, 0.2f).SetEase(Ease.InQuart);
    }

    public void AddSelfToTargetList(EnemyBrain target)
    {
        if (!_targets.Contains(target.gameObject.transform)) _targets.Add(target.gameObject.transform);

        var targetPanel = Instantiate(_targetPanelPrefab, _targetPanelParent);
        targetPanel.Initialize(target);

    }

}
