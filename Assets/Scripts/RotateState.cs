using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RotateState
{
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _minAngle, _maxAngle;

    private Quaternion _target;
    private bool _canUpRotate = true;

    public void Init() => _target = GetTarget();

    public IEnumerator RotateProcess()
    {
        while (true)
        {
            while (_muzzleTransform.localRotation != _target)
            {
                _muzzleTransform.localRotation = Quaternion.RotateTowards(_muzzleTransform.localRotation, _target, Time.deltaTime * _speedRotate);
                yield return null;
            }

            _target = GetTarget();
        }
    }

    private Quaternion GetTarget()
    {
        _canUpRotate = !_canUpRotate;
        return Quaternion.Euler(0, 0, _canUpRotate ? _maxAngle : _minAngle);
    }
}