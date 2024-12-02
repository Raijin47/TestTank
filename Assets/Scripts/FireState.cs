using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class FireState
{
    public event Action OnEndFire;
    [SerializeField] private BulletBase _bullet;
    [SerializeField] private BulletAdditional _bulletAdditional;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Transform _content;

    [Space(10)]
    [SerializeField] private float _force;

    private Pool _bulletPool;
    private Pool _additionalPool;

    private readonly WaitForSeconds Dalay = new(0.15f);

    public void Init()
    {
        _bulletPool = new(_bullet, _content);
        _additionalPool = new(_bulletAdditional, _content);
    }

    public IEnumerator FireProcess()
    {
        var bullet = _bulletPool.Spawn(_bulletPoint.position) as BulletBase;
        bullet.transform.localRotation = _bulletPoint.rotation;
        bullet.Shot(_force);

        yield return Dalay;

        OnEndFire?.Invoke();
    }

    public void SpawnAdditional(Vector2 position, Vector2 direction)
    {
        for (int i = 0; i < 3; i++)
        {
            direction += Vector2.up * 3 + Vector2.right;
            var bullet = _additionalPool.Spawn(position) as BulletAdditional;
            bullet.Gap(direction);
        }
    }
}