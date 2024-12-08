using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;
    public float moneyGainedRatio;
    public float shiftRange;

    public void spawnCoins(float moneyGained)
    {
        for (int coinSpawned = 0; coinSpawned < (int)(moneyGained * moneyGainedRatio); ++coinSpawned)
        {
            spawnCoin();
        }
    }

    public void ClearAllCoins()
    {
        foreach (Transform coin in transform)
        {
            Destroy(coin.gameObject);
        }
    }

    private void spawnCoin()
    {
        var shift_x = UnityEngine.Random.Range(-shiftRange, shiftRange);
        var shift_y = UnityEngine.Random.Range(-shiftRange, shiftRange);
        var shift_z = UnityEngine.Random.Range(-shiftRange, shiftRange);
        var shift = new Vector3(shift_x, shift_y, shift_z);
        Instantiate(coin, transform.position + shift, UnityEngine.Random.rotation, transform);
    }    
}
