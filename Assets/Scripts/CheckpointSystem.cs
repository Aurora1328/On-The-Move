using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Transform[] checkpoints; // Массив всех чекпоинтов
    private int currentCheckpointIndex; // Индекс текущего чекпоинта
    private Transform player; // Ссылка на игрока

    private void Start()
    {
        // Находим игрока в сцене
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Загружаем сохранённый чекпоинт при старте игры
        LoadCheckpoint();
    }

    // Метод для сохранения чекпоинта
    public void SaveCheckpoint(int checkpointIndex)
    {
        Debug.Log("Saving checkpoint: " + checkpointIndex); // Лог для отладки
        PlayerPrefs.SetInt("Checkpoint", checkpointIndex);
        PlayerPrefs.Save(); // Сохраняем PlayerPrefs
    }

    // Метод для загрузки последнего сохранённого чекпоинта
    private void LoadCheckpoint()
    {
        // Проверяем, есть ли сохранённые данные
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            currentCheckpointIndex = PlayerPrefs.GetInt("Checkpoint"); // Загружаем сохранённый чекпоинт
            Debug.Log("Loaded checkpoint: " + currentCheckpointIndex); // Лог для отладки
        }
        else
        {
            currentCheckpointIndex = 0; // Если данных нет, начинаем с первого чекпоинта
            Debug.Log("No saved checkpoint found, starting from the first one.");
        }

        // Перемещаем игрока к загруженному чекпоинту
        TeleportToCheckpoint();
    }

    // Метод для перемещения игрока к последнему чекпоинту
    public void TeleportToCheckpoint()
    {
        // Убедитесь, что индекс чекпоинта находится в пределах массива
        if (currentCheckpointIndex >= 0 && currentCheckpointIndex < checkpoints.Length)
        {
            Transform checkpoint = checkpoints[currentCheckpointIndex];
            player.position = checkpoint.position;
            player.rotation = checkpoint.rotation;
            Debug.Log("Teleporting player to checkpoint: " + currentCheckpointIndex); // Лог для отладки
        }
        else
        {
            Debug.LogError("Checkpoint index is out of bounds: " + currentCheckpointIndex);
        }
    }

    // Вызывается при пересечении чекпоинта игроком
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // Лог для отладки
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached checkpoint: " + currentCheckpointIndex);
            SaveCheckpoint(currentCheckpointIndex);
        }
    }



    // Метод для перехода к следующему уровню (чекпоинту)
    public void NextLevel()
    {
        if (currentCheckpointIndex < checkpoints.Length - 1)
        {
            currentCheckpointIndex++;
            SaveCheckpoint(currentCheckpointIndex); // Сохраняем новый индекс
            TeleportToCheckpoint();
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    // Метод для кнопки "Play", которая телепортирует игрока к сохранённому чекпоинту
    public void OnPlayButtonClick()
    {
        Debug.Log("Play button clicked");
        LoadCheckpoint(); // Загружаем последний сохранённый чекпоинт и телепортируем игрока
    }
}
