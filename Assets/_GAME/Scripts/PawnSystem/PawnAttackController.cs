using System;
using System.Collections.Generic;
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
    [SerializeField] Transform _firePointPivot;


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

    void Start()
    {
        if (EquippedWeapon) EquippedWeapon.UseItem();
    }

    public void SetEquippedWeapon(WeaponItemData weapon)
    {
        EquippedWeapon?.Unequip();
        EquippedWeapon = weapon;
    }

    public void AddTarget(GameObject target)
    {
        var enemyBrain = target.GetComponent<EnemyBrain>();
        if (!enemyBrain) return;

        if (!_target.Contains(enemyBrain)) _target.Add(enemyBrain);
        target.GetComponent<EnemyBrain>().SetEnemyIndex(_target.Count - 1);
        _onTargetFound?.Raise(enemyBrain);
    }

    public void Shoot(int index)
    {
        if (EquippedWeapon == null) return;
        if (index < 0 || index >= _target.Count) 
        {
            PawnChatManager.Instance.AddChat("uhhh...I dunno what enemy you're talkin' about, cap...", ChatterType.Pawn);
            return;
        }

        var target = _target[index];
        if (target == null) return;

        Debug.Log($"Shooting at {target.name}");
        PawnChatManager.Instance.AddChat($"Took a shot at Target: {target.EnemyIndex}", ChatterType.Pawn);
        _firePointPivot.LookAt(target.transform);
        EquippedWeapon.Shoot();
    }

    void Update()
    {
        // if (_target.Count == 0) return;
        // if (counter < _targetUpdateRate)
        // {
        //     counter += Time.deltaTime;
        //     return;
        // }
        // counter = 0;

        // _closestTarget = transform.GetClosestEntity(_target);
        // OnTargetFound?.Invoke();
    }
}
