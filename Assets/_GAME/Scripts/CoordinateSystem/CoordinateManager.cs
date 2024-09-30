using SaiUtils;
using SaiUtils.GameEvents;
using UnityEngine;

public class CoordinateManager : MonoBehaviour
{
    [SerializeField] private CoordinateSpawn _coordinate;

    void Update()
    {
        if (SaiStaticUtils.IsPointerOverUI()) return;


        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

            _coordinate.transform.position = worldPosition;
            _coordinate.Initialize();
        }
    }
}
