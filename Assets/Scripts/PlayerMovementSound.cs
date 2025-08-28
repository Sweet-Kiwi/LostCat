using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    public AudioSource moveAudio;   // Assign the AudioSource with your sound
    public float moveThreshold = 0.1f; // Small threshold to detect movement

    private Rigidbody2D rb; // Assuming 2D player movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if player is moving (using new API)
        if (rb.linearVelocity.magnitude > moveThreshold)
        {
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
        else
        {
            if (moveAudio.isPlaying)
                moveAudio.Stop();
        }
    }
}



