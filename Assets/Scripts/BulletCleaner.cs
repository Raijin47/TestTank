using UnityEngine;

public class BulletCleaner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PoolMember obj))
            obj.ReturnToPool();
    }
}