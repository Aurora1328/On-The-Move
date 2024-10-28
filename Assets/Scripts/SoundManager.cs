using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] soundSources; // Массив AudioSource для всех звуков
    private bool areSoundsPlaying = true; // Переменная для отслеживания состояния звуков

    public bool IsMuted { get { return !areSoundsPlaying; } } // Добавлено свойство IsMuted

    void Start()
    {
        // Убедитесь, что звуки воспроизводятся при старте
        SetSoundsState(true);
    }

    // Метод для переключения состояния звуков
    public void ToggleSounds()
    {
        areSoundsPlaying = !areSoundsPlaying; // Переключаем состояние
        SetSoundsState(areSoundsPlaying);
    }

    private void SetSoundsState(bool play)
    {
        foreach (AudioSource audioSource in soundSources)
        {
            audioSource.mute = !play; // Включаем или отключаем звук
        }
    }
    public void PlaySound(AudioSource audioSource)
    {
        if (!IsMuted && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Воспроизводим звук, если он не играет и не отключен
        }
    }

}
