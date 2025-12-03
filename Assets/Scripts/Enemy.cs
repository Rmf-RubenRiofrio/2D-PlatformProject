using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 2f;   // patrol distance

    private Rigidbody2D rb;
    private float leftBoundary;
    private float rightBoundary;
    private int direction = -1;        // 1 = right, -1 = left

    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        // Try to grab the SpriteRenderer from this object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        float x = transform.position.x;
        leftBoundary = x - moveDistance;
        rightBoundary = x + moveDistance;
    }

    void FixedUpdate()
    {
        // Move left / right
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        float x = rb.position.x;

        // Check patrol limits
        if (direction > 0 && x >= rightBoundary)
        {
            direction = -1;
            Flip();
        }
        else if (direction < 0 && x <= leftBoundary)
        {
            direction = 1;
            Flip();
        }
    }

    void Flip()
    {
        // If for some reason there is no SpriteRenderer, just bail
        if (spriteRenderer == null) return;

        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;   // face right = flipX false, left = true
    }
}
