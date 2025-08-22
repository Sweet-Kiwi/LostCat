using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision : MonoBehaviour
{
    // If you also want hazards to work with non-trigger spikes,
    // keep the collision callbacks too.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flag"))
            GameController.Instance.SetGameState(GameState.Complete);

        if (other.CompareTag("Hazard"))
            GameController.Instance.SetGameState(GameState.Dead);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Hazard"))
            GameController.Instance.SetGameState(GameState.Dead);

        // If your flag uses a non-trigger collider instead of trigger:
        if (other.collider.CompareTag("Flag"))
            GameController.Instance.SetGameState(GameState.Complete);
    }
}