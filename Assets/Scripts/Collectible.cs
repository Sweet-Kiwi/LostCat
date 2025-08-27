using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 1; // How many points this collectible is worth
    public ParticleSystem collectEffect; // Assign the particle system in Inspector

    private bool collected = false; // prevents double trigger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return; // stop if already collected
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Update the fish counter UI
        var collector = FindFirstObjectByType<FishCollector>();
        if (collector != null)
        {
            collector.AddFish(points);
        }

        // Debug message
        Debug.Log("Collected! +" + points + " points");

        // Play particle effect if assigned
        if (collectEffect != null)
        {
            // Detach so it survives after the fish is destroyed
            collectEffect.transform.SetParent(null);
            collectEffect.Play();
            Destroy(collectEffect.gameObject, collectEffect.main.duration);
        }

        // Finally, destroy the collectible itself
        Destroy(gameObject);
    }
}



