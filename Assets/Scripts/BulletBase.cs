using System.Collections;
using UnityEngine;

public class BulletBase : PoolMember
{
    private Rigidbody2D _rigidbody;
    private Coroutine _coroutine;

    private readonly WaitForSeconds Dalay = new(0.5f);

    public override void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void ResetData()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public void Shot(float force)
    {
        _rigidbody.AddForce(transform.right * force, ForceMode2D.Impulse);
        _coroutine = StartCoroutine(FlyProcess());
    }


    private IEnumerator FlyProcess()
    {
        while(_rigidbody.velocity.y >= 0)        
            yield return null;

        yield return Dalay;

        MuzzleBase.Instance.FireState.SpawnAdditional(transform.position, _rigidbody.velocity);
        ReturnToPool();
    }
}