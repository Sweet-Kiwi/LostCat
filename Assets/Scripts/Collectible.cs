using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 1; // How many points this collectible is worth

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // You could add points to a score manager here
            Debug.Log("Collected!");

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}
