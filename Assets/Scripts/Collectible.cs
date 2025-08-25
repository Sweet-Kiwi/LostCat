using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 1; // How many points this collectible is worth
    public ParticleSystem collectEffect; // Assign the particle system in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // You could add points to a score manager here
            Debug.Log("Collected! +" + points + " points");

            // Play particle effect if assigned
            if (collectEffect != null)
            {
                // Detach the particle system so it can play after destroying the collectible
                collectEffect.transform.parent = null;
                collectEffect.Play();
                Destroy(collectEffect.gameObject, collectEffect.main.duration);
            }

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}
