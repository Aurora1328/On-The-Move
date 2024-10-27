using UnityEngine;

public class JumpSoundManager : MonoBehaviour
{
    public AudioClip jumpSound; // Звук прыжка
    private AudioSource audioSource; // Компонент AudioSource

    private void Awake()
    {
        // Получаем компонент AudioSource, если он есть
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to " + gameObject.name);
        }
    }

    public void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null && !audioSource.mute)
        {
            audioSource.PlayOneShot(jumpSound); // Воспроизводим звук прыжка
        }
        else
        {
            Debug.LogWarning("AudioSource or jumpSound is not assigned.");
        }
    }
}
