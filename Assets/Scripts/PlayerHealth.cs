using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Starting health value for the Player
    public int health = 100;
    // Amount of damage the Player takes when hit
    public int damageAmount = 25;

    public Image healthImage;

    // Reference to the Player's SpriteRenderer (used for flashing red)
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component attached to the Player
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthBar();
    }

    // Method to reduce health when damage is taken
    public void TakeDamage()
    {
        health -= damageAmount; // subtract damage amount
        UpdateHealthBar();
        StartCoroutine(BlinkRed()); // briefly flash red
        SoundManager.Instance.PlaySFX("HURT");

        // If health reaches zero or below, call Die()
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthImage != null)
        {
            healthImage.fillAmount = health / 100f;
        }
    }

    // Coroutine to flash the Player red for 0.1 seconds
    private System.Collections.IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    // Reload the scene when the Player dies
    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            // Get the DoubleJump script attached to the same Player
            DoubleJump dj = GetComponent<DoubleJump>();

            if (dj != null)
            {
                dj.extraJumpsValue = 2; // sets max jumps to 2 (triple jump)
            }

            Destroy(collision.gameObject);
        }
    }

    public void Update()
    {
        if(transform.position.y < -15f)
        {
            Die();
        }
    }
}
