using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int coins = 0;
    // Public variables appear in the Inspector, so you can tweak them without editing code.
    public float moveSpeed = 4f;       // How fast the player moves left/right

    //Jump realated variables for the Jump Feature (later)
    public float jumpForce = 8f;      // How strong the jump is (vertical speed)
    public Transform groundCheck;      // Empty child object placed at the player's feet
    public float groundCheckRadius = 0.2f; // Size of the circle used to detect ground
    public LayerMask groundLayer;      // Which layer counts as "ground" (set in Inspector)

    // Private variables are used internally by the script.
    private Rigidbody2D rb;            // Reference to the Rigidbody2D component
    private bool isGrounded;           // True if player is standing on ground
    private Animator animator;         // a reference to the player's animator

    void Start()
    {
        // Grab the Rigidbody2D attached to the Player object once at the start.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); //getting the animator component on the player
    }

    void Update()
    {
        // --- Horizontal movement ---
        // Get input from keyboard (A/D or Left/Right arrows).
        float moveInput = Input.GetAxis("Horizontal");
        // Apply horizontal speed while keeping the current vertical velocity.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump realated code for the Jump Feature (later)
        // --- Ground check ---
        // Create an invisible circle at the GroundCheck position.
        // If this circle overlaps any collider on the "Ground" layer, player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Debug.Log($"IsGrounded: {isGrounded}, Can Jump: {isGrounded && Input.GetButtonDown("Jump")}");

        // --- Jump ---
        // If player is grounded AND the Jump button (Spacebar by default) is pressed:
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Set vertical velocity to jumpForce (launch upward).
            // Horizontal velocity stays the same.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            SoundManager.Instance.PlaySFX("JUMP", 0.4f);
        }
        SetAnimation(moveInput); // call animation logic based on movement & jump state
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)                              // on the ground
        {
            if (moveInput == 0)                     // not moving
            {
                animator.Play("Player_Idle");     // play idle animation
            }
            else                                  // moving
            {
                animator.Play("Player_Run");      // play run animation
            }
        }                                         // in the air (not grounded)
        else
        {                                         // going upward
            if (rb.linearVelocity.y > 0)
            {
                animator.Play("Player_Jump");     // play jump animation
            }
            else
            {
                animator.Play("Player_Fall");     // play fall animation
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BouncePad") {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 1.75f);
        }
    }

}
