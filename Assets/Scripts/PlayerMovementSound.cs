using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    public AudioSource moveAudio;   // Assign the AudioSource
    public float moveThreshold = 0.1f; // Movement threshold

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isMoving = rb.linearVelocity.magnitude > moveThreshold;
        bool isJumping = Mathf.Abs(rb.linearVelocity.y) > 0.1f;

        if (isMoving && !isJumping)
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




