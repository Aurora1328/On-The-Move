using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Transform[] checkpoints; // ������ ���� ����������
    private int currentCheckpointIndex; // ������ �������� ���������
    private Transform player; // ������ �� ������

    private void Start()
    {
        // ������� ������ � �����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ��������� ���������� �������� ��� ������ ����
        LoadCheckpoint();
    }

    // ����� ��� ���������� ���������
    public void SaveCheckpoint(int checkpointIndex)
    {
        Debug.Log("Saving checkpoint: " + checkpointIndex); // ��� ��� �������
        PlayerPrefs.SetInt("Checkpoint", checkpointIndex);
        PlayerPrefs.Save(); // ��������� PlayerPrefs
    }

    // ����� ��� �������� ���������� ����������� ���������
    private void LoadCheckpoint()
    {
        // ���������, ���� �� ���������� ������
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            currentCheckpointIndex = PlayerPrefs.GetInt("Checkpoint"); // ��������� ���������� ��������
            Debug.Log("Loaded checkpoint: " + currentCheckpointIndex); // ��� ��� �������
        }
        else
        {
            currentCheckpointIndex = 0; // ���� ������ ���, �������� � ������� ���������
            Debug.Log("No saved checkpoint found, starting from the first one.");
        }

        // ���������� ������ � ������������ ���������
        TeleportToCheckpoint();
    }

    // ����� ��� ����������� ������ � ���������� ���������
    public void TeleportToCheckpoint()
    {
        // ���������, ��� ������ ��������� ��������� � �������� �������
        if (currentCheckpointIndex >= 0 && currentCheckpointIndex < checkpoints.Length)
        {
            Transform checkpoint = checkpoints[currentCheckpointIndex];
            player.position = checkpoint.position;
            player.rotation = checkpoint.rotation;
            Debug.Log("Teleporting player to checkpoint: " + currentCheckpointIndex); // ��� ��� �������
        }
        else
        {
            Debug.LogError("Checkpoint index is out of bounds: " + currentCheckpointIndex);
        }
    }

    // ���������� ��� ����������� ��������� �������
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // ��� ��� �������
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached checkpoint: " + currentCheckpointIndex);
            SaveCheckpoint(currentCheckpointIndex);
        }
    }



    // ����� ��� �������� � ���������� ������ (���������)
    public void NextLevel()
    {
        if (currentCheckpointIndex < checkpoints.Length - 1)
        {
            currentCheckpointIndex++;
            SaveCheckpoint(currentCheckpointIndex); // ��������� ����� ������
            TeleportToCheckpoint();
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    // ����� ��� ������ "Play", ������� ������������� ������ � ����������� ���������
    public void OnPlayButtonClick()
    {
        Debug.Log("Play button clicked");
        LoadCheckpoint(); // ��������� ��������� ���������� �������� � ������������� ������
    }
}
