using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float time = 0f;
    public float damage;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == gameObject)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.angularVelocity = 0f;
            }

            collision.gameObject.GetComponent<PlayerStats>().UpdateHealth(-(ProjectileDamage()));
        }

        if (collision.gameObject.CompareTag("Mob"))
        {
            GameObject mob = collision.gameObject;
            mob.GetComponent<MobStats>().UpdateHealth(-damage);
        }

        Destroy(gameObject);
    }

    private float ProjectileDamage()
    {
        float dmg = (damage - (GameManager.instance.player.stats.Protection * 0.5f));

        return dmg < 1 ? 1 : dmg;
    }
}
