using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollow : MonoBehaviour
{
    [SerializeField] Transform _target = null;

    [SerializeField] bool _followPosition = true;
    [SerializeField] bool _followRotation = true;

    [SerializeField] Vector3 _offset = Vector3.zero;


    void Update()
    {
        if (_target == null) return;

        if (_followPosition) transform.position = _target.position + _offset;
        if (_followRotation) transform.rotation = _target.rotation;
    }
}
