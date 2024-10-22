using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointIndex; // ������ ����� ���������
    public CheckpointSystem checkpointSystem; // ������ �� CheckpointSystem

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �������� �� ������
        {
            // �������� ������� ���������� � �����������
            checkpointSystem.SaveCheckpoint(checkpointIndex);
            Debug.Log("Player reached checkpoint: " + checkpointIndex); // ��� ��� �������
        }
    }
}
