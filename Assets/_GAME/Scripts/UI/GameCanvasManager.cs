using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] Canvas _inventoryCanvas;

    public void SetInventoryVisibility() => _inventoryCanvas.gameObject.SetActive(!_inventoryCanvas.gameObject.activeSelf);
}
