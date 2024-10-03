using System;
using System.Collections.Generic;
using SaiUtils.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class PawnAttackController : MonoBehaviour
{
    [SerializeField, ReadOnly] List<Transform> _target = new();
    [SerializeField] Transform _closestTarget;
    [SerializeField] float _targetUpdateRate = 0.5f;
    float counter = 0;

    public Action OnTargetFound;

    public void AddTarget(Transform target)
    {
        if (!_target.Contains(target)) _target.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        if (_target.Contains(target)) _target.Remove(target);
    }

    void Update()
    {
        if (_target.Count == 0) return;
        if (counter < _targetUpdateRate)
        {
            counter += Time.deltaTime;
            return;
        }
        counter = 0;

        _closestTarget = transform.GetClosestEntity(_target);
        OnTargetFound?.Invoke();
    }
}
