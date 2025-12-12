using UnityEngine;
using System.Collections;

public class TimeSlowClock : MonoBehaviour
{
    public float slowDuration = 3f;   // How long time will be slowed
    public float slowFactor = 0.5f;   // How much slower everything gets

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Start slow-motion effect
            StartCoroutine(SlowTimeCoroutine());

            // Optional: Play sound effect
            // SoundManager.Instance.PlaySFX("POWERUP");

            // Destroy the clock object
            Destroy(gameObject);
        }
    }

    private IEnumerator SlowTimeCoroutine()
    {
        // Slow down time
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // Wait in real time
        yield return new WaitForSecondsRealtime(slowDuration);

        // Reset time
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
