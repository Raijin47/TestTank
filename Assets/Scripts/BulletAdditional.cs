using UnityEngine;

public class BulletAdditional : PoolMember
{
    private Rigidbody2D _rigidbody;

    public override void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Gap(Vector2 direction)
    {
        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
    }
}