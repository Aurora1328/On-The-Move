using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false;

    // ѕубличное поле дл€ AudioSource, которое можно установить в инспекторе
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            if (uiManager != null)
            {
                uiManager.AddCoin();
            }

            // ¬оспроизводим звук, если AudioSource установлен и звуки включены
            if (audioSource != null && !audioSource.mute)
            {
                audioSource.Play();
            }

            // ”даление монеты через небольшую задержку, чтобы звук успел воспроизвестись
            Destroy(gameObject, 0.1f);
        }
    }
}
