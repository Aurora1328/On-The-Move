using UnityEngine;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    public GameObject winScreenCanvas; // Канвас экрана победы
    public Button exitToMenuButton;    // Кнопка выхода в главное меню
    public Button levelSelectButton;   // Кнопка выбора уровня

    private void Start()
    {
        // Проверяем, назначены ли кнопки и канвас
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(false); // Отключаем канвас победы в начале
        }
        else
        {
            Debug.LogError("Win screen canvas is not assigned in the inspector.");
        }

        if (exitToMenuButton != null)
        {
            exitToMenuButton.onClick.AddListener(ExitToMainMenu);
        }
        else
        {
            Debug.LogError("Exit to Menu button is not assigned in the inspector.");
        }

        if (levelSelectButton != null)
        {
            levelSelectButton.onClick.AddListener(SelectLevel);
        }
        else
        {
            Debug.LogError("Level Select button is not assigned in the inspector.");
        }
    }

    // Метод для показа экрана победы
    public void ShowWinScreen()
    {
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(true); // Включаем канвас победы
            Time.timeScale = 0f; // Останавливаем время в игре
        }
    }

    // Метод для выхода в главное меню
    private void ExitToMainMenu()
    {
        //Time.timeScale = 1f; // Возвращаем нормальную скорость времени
        // Логика перехода в главное меню
        Debug.Log("Returning to main menu...");
        // Здесь можно отключить winScreenCanvas и включить mainMenuCanvas (если у вас есть основной канвас меню)
    }

    // Метод для перехода в меню выбора уровней
    private void SelectLevel()
    {
        //Time.timeScale = 1f; // Возвращаем нормальную скорость времени
        // Логика для перехода к выбору уровня
        Debug.Log("Opening level select menu...");
        // Можно отключить winScreenCanvas и включить levelSelectCanvas
    }
}
