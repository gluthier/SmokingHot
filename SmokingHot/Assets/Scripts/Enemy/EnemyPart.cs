using UnityEngine;

public class EnemyPart : MonoBehaviour
{
    public void EnemyIsHit(int damage)
    {
        transform.parent.GetComponent<EnemyManager>().EnemyIsHit(damage);
    }
}
