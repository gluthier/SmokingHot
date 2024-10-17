using UnityEngine;

public class alcool : MonoBehaviour
{
    
    public void Awake()
    {
    }

    // This function is called when another object enters the trigger collider attached to this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player (assuming the player has a tag "Player")
        if (other.CompareTag(Env.TagPlayer))
        {
            other.GetComponent<PlayerManager>().GetAlcool();
            DestroyBottle();
        }
    }

    private void DestroyBottle()
    {
        Destroy(gameObject);
    }
}
