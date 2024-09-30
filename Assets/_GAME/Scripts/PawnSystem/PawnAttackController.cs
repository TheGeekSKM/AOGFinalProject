using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PawnAttackController : MonoBehaviour
{
    [SerializeField, ReadOnly] List<Transform> _target = new();

    public void AddTarget(Transform target)
    {
        if (!_target.Contains(target)) _target.Add(target);
    }
}
