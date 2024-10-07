using System;
using System.Collections.Generic;
using SaiUtils.Extensions;
using SaiUtils.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;

public class PawnAttackController : MonoBehaviour
{
    [SerializeField, ReadOnly] List<EnemyBrain> _target = new();
    public List<EnemyBrain> Targets => _target;
    [SerializeField] Transform _closestTarget;
    [SerializeField] float _targetUpdateRate = 0.5f;
    [SerializeField] WeaponItemData _equippedWeapon;
    [SerializeField] EnemyEvent _onTargetFound;

    public WeaponItemData EquippedWeapon
    {
        get => _equippedWeapon;
        set
        {
            _equippedWeapon = value;
            Debug.Log($"Equipped weapon changed to {_equippedWeapon.ItemName}");
        }
    }
    float counter = 0;

    public Action OnTargetFound;

    public void AddTarget(GameObject target)
    {
        var enemyBrain = target.GetComponent<EnemyBrain>();
        if (!enemyBrain) return;

        if (!_target.Contains(enemyBrain)) _target.Add(enemyBrain);
        target.GetComponent<EnemyBrain>().SetEnemyIndex(_target.Count - 1);
        _onTargetFound?.Raise(enemyBrain);
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

        // _closestTarget = transform.GetClosestEntity(_target);
        // OnTargetFound?.Invoke();
    }
}
