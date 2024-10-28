using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Ссылка на AudioSource с фоновой музыкой
    private bool isMusicPlaying = true; // Переменная для отслеживания состояния музыки

    void Start()
    {
        // Убедитесь, что музыка играет при старте
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    // Метод для переключения состояния музыки
    public void ToggleMusic()
    {
        if (backgroundMusic != null)
        {
            if (isMusicPlaying)
            {
                backgroundMusic.Pause(); // Остановить музыку
            }
            else
            {
                backgroundMusic.UnPause(); // Возобновить музыку
            }

            isMusicPlaying = !isMusicPlaying; // Переключаем состояние
        }
    }
}
