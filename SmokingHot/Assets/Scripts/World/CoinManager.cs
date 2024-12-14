using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public GameObject coinGameObject;
    public GameObject smoke;
    public List<GameObject> coins = new List<GameObject>();
    public float moneyWorldRatio;
    public float shiftRange;
    public AudioClip coinDestroySound;
    private float previousCapital = 0;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }    

    public void SpawnOrDestroy(float capital)
    {
        float capitalDiff = capital - previousCapital;
    
        if (capitalDiff == 0)
        {
            return;
        }

        Debug.Log($"Capital Difference: {capitalDiff}");

        if (capitalDiff > 0)
        {
            SpawnCoins(capitalDiff);
        }
        else if (capitalDiff < 0)
        {
            DestroyCoins(-capitalDiff);
        }

        // Update previous capital, while setting 0 as the minimum to ensure that the coins reflect real situation.
        if (capital > 0)
        {
            previousCapital = capital;
        }
        else
        {
            previousCapital = 0f;
        }
    }

    public void SpawnCoins(float moneyGained)
    {
        for (int coinSpawned = 0; coinSpawned < (int)(moneyGained * moneyWorldRatio); ++coinSpawned)
        {
            SpawnCoin();
        }
    }

    public void DestroyCoins(float moneyLost)
    {
        for (int coinDestroyed = 0; coinDestroyed < (int)(moneyLost * moneyWorldRatio); ++coinDestroyed)
        {
            if (coins.Count > 0)
            {
                int lastIndex = coins.Count - 1;
                DestroyCoin(lastIndex);
            }
        }

        audioSource.PlayOneShot(coinDestroySound);
    }

    public void DestroyCoin(int coinIndex)
    {
        GameObject coin = coins[coinIndex];
        coins.RemoveAt(coinIndex);
        
        // Make smoke appear
        GameObject newSmoke = Instantiate(smoke, coin.transform.position, Quaternion.identity);
        newSmoke.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        Destroy(coin);
        Destroy(newSmoke, 2);
    }

    public void ClearAllCoins()
    {
        for (int coinIndex = coins.Count - 1; coinIndex >= 0; --coinIndex)
        {
            DestroyCoin(coinIndex);
        }
    }

    private void SpawnCoin()
    {
        var shift_x = UnityEngine.Random.Range(-shiftRange, shiftRange);
        var shift_y = UnityEngine.Random.Range(-shiftRange, shiftRange);
        var shift_z = UnityEngine.Random.Range(-shiftRange, shiftRange);
        var shift = new Vector3(shift_x, shift_y, shift_z);
        
        GameObject coin = Instantiate(coinGameObject, transform.position + shift, UnityEngine.Random.rotation, transform);
        coins.Add(coin);
    }    
}
