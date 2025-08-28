using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem dustParticles;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 particlesStartPos;

    private float input;
    public int moveDirection { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particlesStartPos = dustParticles.transform.localPosition;
    }

    void Update()
    {
        // Get input every frame
        input = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        moveDirection = (int)input;

        // Running animation
        animator.SetBool("IsRunning", input != 0);

        // Flip player + particles
        // Flip sprite
        if (input < 0)      transform.localScale = new Vector3(-1, 1, 1);
        else if (input > 0) transform.localScale = new Vector3( 1, 1, 1);

// Always put dust BEHIND the move direction
        float behindX = -Mathf.Abs(particlesStartPos.x); // behind when facing/moving right
        Vector3 posRight = new Vector3(behindX,              particlesStartPos.y, particlesStartPos.z);
        Vector3 posLeft  = new Vector3(-behindX,             particlesStartPos.y, particlesStartPos.z);

        if (input > 0)      dustParticles.transform.localPosition = posRight; // moving right → dust to the left
        else if (input < 0) dustParticles.transform.localPosition = posLeft;  // moving left  → dust to the right


        // Handle dust particles (only play when moving on ground)
        if (isGrounded && input != 0)
        {
            if (!dustParticles.isPlaying)
                dustParticles.Play();
        }
        else
        {
            if (dustParticles.isPlaying)
                dustParticles.Stop();
        }

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Animation states
        if (!isGrounded && rb.linearVelocity.y > 0.1f)
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        else if (!isGrounded && rb.linearVelocity.y < -0.1f)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
    }

    void FixedUpdate()
    {
        // Apply movement with physics
        rb.linearVelocity = new Vector2(input * speed, rb.linearVelocity.y);

        // Debug logging
        Debug.Log("Input: " + input + " | Velocity: " + rb.linearVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

