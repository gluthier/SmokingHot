using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private int health;
    private int maxHealth;
    private int strenght;
    private float fireRate;

    private const float SPAWN_OFFSET = 1.5f;
    private bool lost1ThirdHp = false, lost2ThirdHp = false;
    public Transform target;

    private GameObject enemyBulletPrefab;
    private GameObject beerPrefab;

    public void Init(GameManager a_gameManager)
    {
        gameManager = a_gameManager;
        target = a_gameManager.playerManager.transform;
    }
     
    void Start()
    {
        maxHealth = health = 200;
        strenght = 2;
        fireRate = 1.5f;
        enemyBulletPrefab = Resources.Load(Env.EnemyBulletPrefab) as GameObject;
        beerPrefab = Resources.Load(Env.BeerPrefab) as GameObject;

        StartCoroutine(AutoFire());
    }
    
    public void Update()
    {
        transform.LookAt(target);
    }

    public void EnemyIsHit(int damage)
    {
        health -= damage;

        if (health <= 2 * maxHealth/3 && !lost1ThirdHp)
        {
            lost1ThirdHp = true;
            DropAlcool();
        }else if(health <= maxHealth/3 && !lost2ThirdHp)
        {
            lost2ThirdHp = true;
            DropAlcool();
        }else if (health <= 0)
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

    private void DropAlcool()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 rotationBeer = new Vector3(30, 160, 30);

        // Calculate the positions for the health potions
        Vector3 positionAbove = enemyPosition + new Vector3(0, 0, SPAWN_OFFSET);   // Above the enemy
        Vector3 positionBelow = enemyPosition + new Vector3(0, 0, -SPAWN_OFFSET);  // Below the enemy
        Vector3 positionLeft = enemyPosition + new Vector3(-SPAWN_OFFSET, 0, 0);   // To the left of the enemy
        Vector3 positionRight = enemyPosition + new Vector3(SPAWN_OFFSET, 0, 0);   // To the right of the enemy

        Quaternion rotation = Quaternion.Euler(rotationBeer);

        // Spawn health potions at the calculated positions
        Instantiate(beerPrefab, positionAbove, rotation);  // Spawn above
        Instantiate(beerPrefab, positionBelow, rotation);  // Spawn below
        Instantiate(beerPrefab, positionLeft, rotation);
        Instantiate(beerPrefab, positionRight, rotation);
       
    }
}
