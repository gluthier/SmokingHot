using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    void Update()
    {
        Vector3 forward = Env.BulletVelocity * Time.deltaTime * transform.forward;
        transform.position = transform.position + forward;
        
        StartCoroutine(BulletLifetime());
    }

    void OnTriggerEnter(Collider other)
    {
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
