using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnPoint;  // Current respawn location
    public Transform startPoint;   // Starting position (assign in Inspector)

    private void Start()
    {
        // Set initial respawn point to the start point
        respawnPoint = startPoint.position;
    }

    private void Update()
    {
        // Example: if player falls below -10 on Y axis, respawn
        if (transform.position.y < -20f)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        // Reset other stuff if needed, like health, velocity, etc.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // stop momentum
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player touches a checkpoint, update respawn point
        if (collision.CompareTag("Checkpoint"))
        {
            respawnPoint = collision.transform.position;
        }
    }
}

