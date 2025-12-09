using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    private TMP_Text coinText;
    public int coinsToGive = 1;

    private void Start()
    {
        // Automatically find ANY TMP_Text in the scene
        // OR use the specific name if needed
        coinText = GameObject.Find("CoinText")?.GetComponent<TMP_Text>();

        
        
            Debug.Log("Found CoinText? " + (coinText != null));
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFX("COIN", 0.4f);

            PlayerController player = collision.GetComponent<PlayerController>();
            player.coins += coinsToGive;

            if (coinText != null)
                coinText.text = player.coins.ToString();

            Destroy(gameObject);
        }
    }
}
