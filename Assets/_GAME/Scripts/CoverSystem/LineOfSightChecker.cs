using System.Collections;
using System.Collections.Generic;
using SaiUtils.Extensions;
using UnityEngine;

// [RequireComponent(typeof(SphereCollider))]
public class LineOfSightChecker : MonoBehaviour
{
    [SerializeField] SphereCollider _sphereCollider;
    [SerializeField] float _fieldOfView = 90f;
    [SerializeField] LayerMask _lineOfSightLayers;

    public delegate void GainSightEvent(Transform enemy);
    public GainSightEvent OnGainSight;

    public delegate void LoseSightEvent(Transform enemy);
    public LoseSightEvent OnLoseSight;

    Coroutine _checkLineOfSightCoroutine;

    void OnValidate()
    {
        if (_sphereCollider == null) _sphereCollider = gameObject.GetOrAdd<SphereCollider>();
        _sphereCollider.isTrigger = true;
    }

    public void FindCover(Transform enemy)
    {
        if (!CheckLineOfSight(enemy))
        {
            _checkLineOfSightCoroutine = StartCoroutine(CheckLineOfSightCoroutine(enemy));
        }
    }

    void LeaveCover(Transform enemy)
    {
        OnLoseSight?.Invoke(enemy);
        if (_checkLineOfSightCoroutine != null) StopCoroutine(_checkLineOfSightCoroutine);
    }

    bool CheckLineOfSight(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        var dotProduct = Vector3.Dot(transform.forward, direction);
        if (dotProduct >= Mathf.Cos(_fieldOfView))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, _sphereCollider.radius, _lineOfSightLayers))
            {
                if (hit.transform == target)
                {
                    OnGainSight?.Invoke(target);
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator CheckLineOfSightCoroutine(Transform target)
    {
       WaitForSeconds wait = new WaitForSeconds(0.5f);

       while (!CheckLineOfSight(target))
       {
           yield return wait;
       }
    }
}   
