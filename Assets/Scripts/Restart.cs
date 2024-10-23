using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject gameOverScreen; // ����� ��������� ����
    public Button restartButton; // ������ �����������
    public CustomCharacterControllerRosti characterController; // ������ �� ���������� ������
    public CheckpointSystem checkpointSystem; // ������ �� ������� ����������

    private void Awake()
    {
        gameOverScreen.SetActive(false); // �������� ����� ��������� ����
        restartButton.onClick.AddListener(RestartGame); // �������� �� ������� ������� ������
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); // ���������� ����� ��������� ����
        Time.timeScale = 0f; // ������������� �����
    }

    private void RestartGame()
    {
        Time.timeScale = 1f; // ��������� �����

        // ����� �� ������ �������� ������, ����� �������� ��������� ������ ���������
        if (characterController != null)
        {
            characterController.ResetCharacter(); // �������� ����� ������ ��������� ���������, ���� �� ����
            characterController.StartGame(); // ������ ����
        }

        if (checkpointSystem != null)
        {
            checkpointSystem.LoadCheckpoint(); // ��������� ��������� ���������� ��������
        }
        else
        {
            Debug.LogError("CheckpointSystem is not assigned! Please assign it in the inspector.");
        }

        gameOverScreen.SetActive(false); // �������� ����� ��������� ����
    }
}
