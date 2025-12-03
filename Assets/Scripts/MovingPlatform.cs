using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; 
        rb.useFullKinematicContacts = true;

        startPos = rb.position;
    }

    void FixedUpdate()
    {
        // Move platform using kinematic velocity
        Vector2 velocity = new Vector2(direction * speed, 0f);
        rb.linearVelocity = velocity;

        // Flip direction at patrol bounds
        float offset = rb.position.x - startPos.x;

        if (direction > 0 && offset >= distance)
            direction = -1;

        if (direction < 0 && offset <= -distance)
            direction = 1;
    }
}
