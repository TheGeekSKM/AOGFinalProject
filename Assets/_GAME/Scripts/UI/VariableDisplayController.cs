using SaiUtils.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableDisplayController : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Components")]
    [SerializeField] TextMeshProUGUI _variableName;
    [SerializeField] TextMeshProUGUI _variableValue;

    [Header("Events")]
    [SerializeField] StringEvent _onVariableClick;
    public void Initialize(string variableName, string variableValue)
    {
        _variableName.text = variableName;
        _variableValue.text = variableValue;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click event here
        _onVariableClick?.Raise(_variableValue.text);
    }
}
