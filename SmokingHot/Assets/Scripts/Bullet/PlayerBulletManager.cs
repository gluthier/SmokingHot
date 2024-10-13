using System.Collections;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    private int damage = 0;

    public void SetDamage(int a_damage)
    {
        damage = a_damage;
    }

    void Update()
    {
        Vector3 forward = Env.PlayerBulletVelocity * Time.deltaTime * transform.forward;
        transform.position = transform.position + forward;
        
        StartCoroutine(BulletLifetime());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Env.TagEnemy))
        {
            other.GetComponent<EnemyPart>().EnemyIsHit(damage);
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
