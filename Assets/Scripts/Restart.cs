using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject gameOverScreen; // Объект экрана окончания игры

    private void Start()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false); // Скрываем экран окончания игры в начале
        }
    }

    // Метод для отображения экрана окончания игры
    public void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Показываем экран окончания игры
        }
        else
        {
            Debug.LogError("GameOverScreen is not assigned in the inspector.");
        }
    }

    public void RestartGame()
    {
        // Ваша логика для рестарта игры
    }
}
