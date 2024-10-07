using System.Collections.Generic;
using DG.Tweening;
using SaiUtils.Singleton;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameCanvasManager : SerializedMonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] InventoryDisplayController _inventoryCanvas;
    [SerializeField] RectTransform _inventoryCanvasRectTransform;

    [Header("Targets")]
    [SerializeField] RectTransform _targetPanelParent;
    [SerializeField] TargetPanelController _targetPanelPrefab;
    [SerializeField] List<Transform> _targets = new List<Transform>();
    [SerializeField] List<RectTransform> _targetPanels = new List<RectTransform>();

    [Header("Map")]
    [SerializeField] RectTransform _mapCanvasRectTransform;

    [Header("Help")]
    [SerializeField] RectTransform _helpCanvasRectTransform;

    [Header("Variables")]
    [SerializeField] Dictionary<string, string> variables = new();
    [SerializeField] RectTransform _variablePanelCanvas;
    [SerializeField] RectTransform _variablePanelParent;
    [SerializeField] VariableDisplayController _variablePanelPrefab;
    List<VariableDisplayController> _variablePanels = new List<VariableDisplayController>();

    public static GameCanvasManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetVariable(string key, string value)
    {
        if (variables.ContainsKey(key)) variables[key] = value;
        else variables.Add(key, value);

        if (_variablePanels.Exists(panel => panel.Key == key))
        {
            _variablePanels.Find(panel => panel.Key == key).UpdateValue(value);
            return;
        }
        var variablePanel = Instantiate(_variablePanelPrefab, _variablePanelParent);
        variablePanel.Initialize(key, value);
        _variablePanels.Add(variablePanel);
    }

    public void SetInventoryVisible() 
    {
        _inventoryCanvasRectTransform.DOAnchorPosX(0, 0.2f).SetEase(Ease.OutQuart);
        _inventoryCanvas.Initialize();
    }

    public void SetInventoryInvisible()
    {
        _inventoryCanvasRectTransform.DOAnchorPosX(-1920 * 2, 0.2f).SetEase(Ease.InQuart);
    }

    public void SetMapVisible()
    {
        _mapCanvasRectTransform.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutQuart);
    }

    public void SetMapInvisible()
    {
        _mapCanvasRectTransform.DOAnchorPosY(-1080 * 2, 0.2f).SetEase(Ease.InQuart);
    }

    public void SetHelpVisible()
    {
        Debug.Log("Help visible");
        _helpCanvasRectTransform.DOAnchorPosX(0, 0.2f).SetEase(Ease.OutQuart);
    }

    public void SetHelpInvisible()
    {
        Debug.Log("Help invisible");
        _helpCanvasRectTransform.DOAnchorPosX(1920 * 2, 0.2f).SetEase(Ease.InQuart);
    }

    public void SetVariablePanelVisible()
    {
        _variablePanelCanvas.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutQuart);
    }

    public void SetVariablePanelInvisible()
    {
        _variablePanelCanvas.DOAnchorPosY(1080 * 2, 0.2f).SetEase(Ease.InQuart);
    }

    public void AddSelfToTargetList(EnemyBrain target)
    {
        if (!_targets.Contains(target.gameObject.transform)) _targets.Add(target.gameObject.transform);

        var targetPanel = Instantiate(_targetPanelPrefab, _targetPanelParent);
        targetPanel.Initialize(target);

    }

}
