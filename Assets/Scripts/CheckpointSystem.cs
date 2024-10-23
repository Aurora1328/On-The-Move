using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Transform[] checkpoints; // ������ ���� ����������
    private int currentCheckpointIndex = 0; // ������ �������� ���������
    private Transform player; // ������ �� ������
    private bool gameStarted = false; // ����, ������������, �������� �� ����

    private void Start()
    {
        // ������� ������ � �����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ���������� ������������� ������ �� 0-� �������� ��� ���������� ���������
        TeleportToCheckpoint(0, saveProgress: false);
    }

    // ����� ��� ���������� ���������
    public void SaveCheckpoint(int checkpointIndex)
    {
        // ���������, ��� �� �� �� 0-� ���������
        if (checkpointIndex > 0)
        {
            Debug.Log("Saving checkpoint: " + checkpointIndex); // ��� ��� �������
            PlayerPrefs.SetInt("Checkpoint", checkpointIndex);
            PlayerPrefs.Save(); // ��������� PlayerPrefs
        }
        else
        {
            Debug.Log("Checkpoint 0, progress not saved.");
        }

        currentCheckpointIndex = checkpointIndex;
    }

    // ����� ��� �������� ���������� ����������� ���������
    public void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            currentCheckpointIndex = PlayerPrefs.GetInt("Checkpoint");
            Debug.Log("Loaded checkpoint: " + currentCheckpointIndex); // ��� ��� �������
        }
        else
        {
            currentCheckpointIndex = 0; // ���� ������ ���, �������� � ������� ���������
            Debug.Log("No saved checkpoint found, starting from the first one.");
        }

        // ������������� ������ � ������������ ���������
        TeleportToCheckpoint(currentCheckpointIndex);
    }

    // ����� ��� ����������� ������ � ���������
    public void TeleportToCheckpoint(int checkpointIndex, bool saveProgress = true)
    {
        if (checkpointIndex >= 0 && checkpointIndex < checkpoints.Length)
        {
            Transform checkpointTransform = checkpoints[checkpointIndex];
            player.position = checkpointTransform.position;
            player.rotation = checkpointTransform.rotation;
            Debug.Log("Teleporting player to checkpoint: " + checkpointIndex);

            // ��������� ��������, ���� ��� ����������
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

    // ����� ��� ������ "�����", ������� �������� ���� � ������������� �� ���������� ��������
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadCheckpoint(); // ��������� ��������� ���������� �������� � ������������� ������
        gameStarted = true; // ���� ��������
    }
}
