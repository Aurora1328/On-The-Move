using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject gameOverScreen; // Экран окончания игры
    public Button restartButton; // Кнопка перезапуска
    public CustomCharacterControllerRosti characterController; // Ссылка на контроллер игрока
    public CheckpointSystem checkpointSystem; // Ссылка на систему чекпоинтов

    private void Awake()
    {
        gameOverScreen.SetActive(false); // Скрываем экран окончания игры
        restartButton.onClick.AddListener(RestartGame); // Подписка на событие нажатия кнопки
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); // Показываем экран окончания игры
        Time.timeScale = 0f; // Останавливаем время
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
