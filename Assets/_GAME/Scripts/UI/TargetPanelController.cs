using TMPro;
using UnityEngine;

public class TargetPanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _targetIndex;
    EnemyBrain _target;

    public void Initialize(EnemyBrain target)
    {
        _target = target;
        _targetIndex.text = target.EnemyIndex.ToString();
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
        }

        transform.position = Camera.main.WorldToScreenPoint(_target.transform.position);
    }
}