using System.Collections;
using System.Collections.Generic;
using SaiUtils.Extensions;
using TMPro;
using UnityEngine;

public class CoordinateSpawn : MonoBehaviour
{
    [SerializeField] TextMeshPro _coordinateText;

    public void Initialize()
    {
        Vector2Int coordinate = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        _coordinateText.text = $"({coordinate.x}, {coordinate.y})";
        transform.SetPositionY(0);
    }
}
