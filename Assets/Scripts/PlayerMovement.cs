using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        movement.x = input * speed * Time.deltaTime;

        
        transform.Translate(new Vector3(movement.x, 0f, 0f));

        
        animator.SetBool("isRunning", input != 0);
        Debug.Log("IsRunning = " + (input != 0));
        
        if (input < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}



