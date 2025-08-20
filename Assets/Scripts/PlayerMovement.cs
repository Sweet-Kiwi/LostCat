using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;

    public int moveDirection { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // -1, 0, 1

        // Move player
        rb.linearVelocity = new Vector2(input * speed, rb.linearVelocity.y);

        // Save movement direction
        moveDirection = (int)input;

        // Running animation
        animator.SetBool("IsRunning", input != 0);

        // Flip sprite
        if (input < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (input > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // --- ANIMATION STATES BASED ON VELOCITY ---
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


