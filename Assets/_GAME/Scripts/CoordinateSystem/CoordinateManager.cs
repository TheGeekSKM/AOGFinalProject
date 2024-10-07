using SaiUtils;
using SaiUtils.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoordinateManager : MonoBehaviour
{
    [SerializeField] private CoordinateSpawn _coordinate;
    [SerializeField] bool _useScreenCoordinateSpawn = true;
    [SerializeField] TransformEvent _screenCoordinateEvent;
    [SerializeField] Transform _coordinateParent;
    [SerializeField] VoidEvent _coordinateDisableEvent;

    void Update()
    {
        if (SaiStaticUtils.IsPointerOverUI()) return;


        if (Input.GetMouseButtonDown(0))
        {
            if (!_useScreenCoordinateSpawn) HandleWorldCoordinateSpawn();
            else HandleScreenCoordinateSpawn();
        }
    }

    void HandleWorldCoordinateSpawn()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        _coordinate.transform.position = worldPosition;
        _coordinate.Initialize();
    }

    void HandleScreenCoordinateSpawn()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        _coordinateParent.position = new Vector3(worldPosition.x, 0, worldPosition.z);

        _screenCoordinateEvent?.Raise(_coordinateParent);
        GameCanvasManager.Instance.SetVariable("click", $"{mousePos.x}, {mousePos.y}");
    }

    public void DisableCoordinate() => _coordinateDisableEvent?.Raise();
}
