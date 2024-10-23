using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Transform[] checkpoints; // Массив всех чекпоинтов
    private int currentCheckpointIndex = 0; // Индекс текущего чекпоинта
    private Transform player; // Ссылка на игрока
    private bool gameStarted = false; // Флаг, показывающий, началась ли игра

    private void Start()
    {
        // Находим игрока в сцене
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Изначально телепортируем игрока на 0-й чекпоинт БЕЗ СОХРАНЕНИЯ прогресса
        TeleportToCheckpoint(0, saveProgress: false);
    }

    // Метод для сохранения чекпоинта
    public void SaveCheckpoint(int checkpointIndex)
    {
        // Проверяем, что мы не на 0-м чекпоинте
        if (checkpointIndex > 0)
        {
            Debug.Log("Saving checkpoint: " + checkpointIndex); // Лог для отладки
            PlayerPrefs.SetInt("Checkpoint", checkpointIndex);
            PlayerPrefs.Save(); // Сохраняем PlayerPrefs
        }
        else
        {
            Debug.Log("Checkpoint 0, progress not saved.");
        }

        currentCheckpointIndex = checkpointIndex;
    }

    // Метод для загрузки последнего сохранённого чекпоинта
    public void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            currentCheckpointIndex = PlayerPrefs.GetInt("Checkpoint");
            Debug.Log("Loaded checkpoint: " + currentCheckpointIndex); // Лог для отладки
        }
        else
        {
            currentCheckpointIndex = 0; // Если данных нет, начинаем с первого чекпоинта
            Debug.Log("No saved checkpoint found, starting from the first one.");
        }

        // Телепортируем игрока к загруженному чекпоинту
        TeleportToCheckpoint(currentCheckpointIndex);
    }

    // Метод для перемещения игрока к чекпоинту
    public void TeleportToCheckpoint(int checkpointIndex, bool saveProgress = true)
    {
        if (checkpointIndex >= 0 && checkpointIndex < checkpoints.Length)
        {
            Transform checkpointTransform = checkpoints[checkpointIndex];
            player.position = checkpointTransform.position;
            player.rotation = checkpointTransform.rotation;
            Debug.Log("Teleporting player to checkpoint: " + checkpointIndex);

            // Сохраняем прогресс, если это необходимо
            if (saveProgress)
            {
                SaveCheckpoint(checkpointIndex);
            }
        }
        else
        {
            Debug.LogError("Checkpoint index is out of bounds: " + checkpointIndex);
        }
    }

    // Метод для кнопки "Старт", который начинает игру и телепортирует на сохранённый чекпоинт
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadCheckpoint(); // Загружаем последний сохранённый чекпоинт и телепортируем игрока
        gameStarted = true; // Игра началась
    }
}
