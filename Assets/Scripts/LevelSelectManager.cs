using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Для загрузки уровней

public class LevelSelectManager : MonoBehaviour
{
    public Button[] levelButtons;    // Массив кнопок для выбора уровня (должен содержать 9 кнопок)
    public CheckpointSystem checkpointSystem; // Ссылка на систему чекпоинтов
    public int totalLevels = 9;      // Количество уровней (чекпоинтов)

    private void Start()
    {
        // Проверяем, открыт ли уровень, и блокируем кнопки для закрытых уровней
        UpdateLevelButtons();
    }

    // Метод для обновления состояния кнопок в зависимости от открытых уровней (чекпоинтов)
    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Проверяем, открыт ли чекпоинт
            bool isCheckpointOpen = checkpointSystem.IsCheckpointUnlocked(i);

            // Если чекпоинт открыт, разблокируем кнопку, иначе — блокируем
            levelButtons[i].interactable = isCheckpointOpen;
        }
    }

    // Метод, который вызывается при нажатии на кнопку выбора уровня
    public void OnLevelButtonClicked(int levelIndex)
    {
        // Проверяем, открыт ли уровень (чекпоинт)
        if (checkpointSystem.IsCheckpointUnlocked(levelIndex))
        {
            // Загружаем чекпоинт (уровень)
            LoadLevel(levelIndex);
        }
        else
        {
            Debug.Log("Level " + levelIndex + " is not unlocked yet.");
        }
    }

    // Метод загрузки уровня
    private void LoadLevel(int checkpointIndex)
    {
        // Здесь мы можем выполнить логику загрузки сцены или чекпоинта
        Debug.Log("Loading Level: " + checkpointIndex);

        // Пример загрузки сцены через SceneManager, если уровни — это отдельные сцены
        // SceneManager.LoadScene("Level" + checkpointIndex);

        // Если у вас уровни — это чекпоинты в одной сцене, используем чекпоинты
        checkpointSystem.LoadCheckpoint(checkpointIndex);
    }
}
