using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject gameOverScreen; // Экран окончания игры
    public Button[] restartButtons; // Массив кнопок перезапуска
    public CustomCharacterControllerRosti characterController; // Ссылка на контроллер игрока
    public CheckpointSystem checkpointSystem; // Ссылка на систему чекпоинтов
    public AudioSource gameOverSound; // Звук для экрана окончания игры

    private void Awake()
    {
        gameOverScreen.SetActive(false); // Скрываем экран окончания игры

        // Подписка на событие нажатия кнопок
        foreach (Button button in restartButtons)
        {
            button.onClick.AddListener(RestartGame);
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); // Показываем экран окончания игры
        Time.timeScale = 0f; // Останавливаем время

        // Воспроизводим звук окончания игры
        if (gameOverSound != null)
        {
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlaySound(gameOverSound); // Воспроизводим звук
            }
            else
            {
                Debug.LogWarning("SoundManager не найден!");
            }
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f; // Запускаем время

        // Здесь вы можете добавить логику, чтобы сбросить состояния вашего персонажа
        if (characterController != null)
        {
            characterController.ResetCharacter(); // Вызываем метод сброса состояния персонажа, если он есть
            characterController.StartGame(); // Запуск игры
        }

        if (checkpointSystem != null)
        {
            checkpointSystem.LoadCheckpoint(); // Загружаем последний сохранённый чекпоинт
        }
        else
        {
            Debug.LogError("CheckpointSystem is not assigned! Please assign it in the inspector.");
        }

        gameOverScreen.SetActive(false); // Скрываем экран окончания игры
    }
}

