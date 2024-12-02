using System;
using UnityEngine;

public abstract class PoolMember : MonoBehaviour
{
    public event Action<PoolMember> Die;

    public virtual void Init()
    {

    }

    public virtual void ResetData()
    {

    }

    public virtual void Release()
    {

    }

    public void ReturnToPool() => Die?.Invoke(this);
}