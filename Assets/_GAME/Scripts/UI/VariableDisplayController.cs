using SaiUtils.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableDisplayController : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Components")]
    [SerializeField] TextMeshProUGUI _variableName;
    public string Key => _variableName.text;
    [SerializeField] TextMeshProUGUI _variableValue;
    public string Value => _variableValue.text;

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

    public void UpdateValue(string value)
    {
        _variableValue.text = value;
    }
}
