using System.Collections;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    private int damage = 0;

    public void SetDamage(int a_damage)
    {
        damage = a_damage;
    }

    void Update()
    {
        Vector3 forward = Env.EnemyBulletVelocity * Time.deltaTime * transform.forward;
        transform.position = transform.position + forward;

        StartCoroutine(BulletLifetime());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Env.TagPlayer))
        {
            other.GetComponent<PlayerManager>().PlayerIsHit(damage);
            DestroyBullet();
        }

        if (other.CompareTag(Env.TagLevel))
            DestroyBullet();
    }

    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(Env.BulletLifetime);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
