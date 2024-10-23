using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointIndex; // Индекс этого чекпоинта
    private CheckpointSystem checkpointSystem; // Ссылка на систему чекпоинтов

    private void Start()
    {
        // Ищем систему чекпоинтов на сцене
        checkpointSystem = FindObjectOfType<CheckpointSystem>();
        if (checkpointSystem == null)
        {
            Debug.LogError("CheckpointSystem is not assigned in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Если игрок входит в триггер
        {
            // Сохраняем текущий чекпоинт
            checkpointSystem.SaveCheckpoint(checkpointIndex);

            // Открываем чекпоинт, если он ещё не был открыт
            if (!checkpointSystem.IsCheckpointUnlocked(checkpointIndex))
            {
                checkpointSystem.UnlockCheckpoint(checkpointIndex);
            }
        }
    }
}
