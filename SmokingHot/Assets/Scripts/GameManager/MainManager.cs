using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public string companyName;

    void Awake()
    {
        // Check if the GameManager Singleton exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy this instance if one already exists
        }
    }
}