using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AttackType
{
    public GameObject throwable;
    public float firePower = 10.0f;

    public override void Attack(Transform target)
    {
        ShootProjectile(target);
    }

    private void ShootProjectile(Transform target)
    {
        if (target != null)
        {
            GameObject newObject = Instantiate(throwable, transform.position, Quaternion.identity);

            Vector2 direction = (target.position - transform.position).normalized;

            Rigidbody2D rb2d = newObject.GetComponent<Rigidbody2D>();

            rb2d.AddForce(direction * firePower, ForceMode2D.Impulse);
        }
    }
}
