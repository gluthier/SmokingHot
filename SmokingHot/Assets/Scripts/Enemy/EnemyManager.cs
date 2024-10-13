using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;

    private int health;
    private int strenght;
    private float fireRate;

    private GameObject enemyBulletPrefab;

    public void Init(GameManager a_gameManager)
    {
        gameManager = a_gameManager;
    }
     
    void Start()
    {
        health = 200;
        strenght = 2;
        fireRate = 1.5f;

        enemyBulletPrefab = Resources.Load(Env.EnemyBulletPrefab) as GameObject;

        StartCoroutine(AutoFire());
    }

    public void EnemyIsHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, transform.rotation, gameManager.GetBulletHolder());

        bullet.GetComponent<EnemyBulletManager>()
            .SetDamage(strenght * Env.EnemyBaseDamage);
    }
}
