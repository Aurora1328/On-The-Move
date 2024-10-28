using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject gameOverScreen; // ����� ��������� ����
    public Button[] restartButtons; // ������ ������ �����������
    public CustomCharacterControllerRosti characterController; // ������ �� ���������� ������
    public CheckpointSystem checkpointSystem; // ������ �� ������� ����������
    public AudioSource gameOverSound; // ���� ��� ������ ��������� ����

    private void Awake()
    {
        gameOverScreen.SetActive(false); // �������� ����� ��������� ����

        // �������� �� ������� ������� ������
        foreach (Button button in restartButtons)
        {
            button.onClick.AddListener(RestartGame);
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true); // ���������� ����� ��������� ����
        Time.timeScale = 0f; // ������������� �����

        // ������������� ���� ��������� ����
        if (gameOverSound != null)
        {
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlaySound(gameOverSound); // ������������� ����
            }
            else
            {
                Debug.LogWarning("SoundManager �� ������!");
            }
        }
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

