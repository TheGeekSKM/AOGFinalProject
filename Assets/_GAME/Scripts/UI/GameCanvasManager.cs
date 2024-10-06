using DG.Tweening;
using SaiUtils.Singleton;
using UnityEngine;

public class GameCanvasManager : Singleton<GameCanvasManager>
{
    [SerializeField] InventoryDisplayController _inventoryCanvas;
    [SerializeField] RectTransform _inventoryCanvasRectTransform;

    public void SetInventoryVisible() 
    {
        _inventoryCanvasRectTransform.DOAnchorPosX(0, 0.2f).SetEase(Ease.OutQuart);
        _inventoryCanvas.Initialize();
    }

    public void SetInventoryInvisible()
    {
        _inventoryCanvasRectTransform.DOAnchorPosX(-1920, 0.2f).SetEase(Ease.InQuart);
    }
}
