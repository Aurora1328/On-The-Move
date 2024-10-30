using UnityEngine;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    public GameObject winScreenCanvas; // ������ ������ ������
    public Button exitToMenuButton;    // ������ ������ � ������� ����
    public Button levelSelectButton;   // ������ ������ ������

    private void Start()
    {
        // ���������, ��������� �� ������ � ������
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(false); // ��������� ������ ������ � ������
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

    // ����� ��� ������ ������ ������
    public void ShowWinScreen()
    {
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(true); // �������� ������ ������
            Time.timeScale = 0f; // ������������� ����� � ����
        }
    }

    // ����� ��� ������ � ������� ����
    private void ExitToMainMenu()
    {
        //Time.timeScale = 1f; // ���������� ���������� �������� �������
        // ������ �������� � ������� ����
        Debug.Log("Returning to main menu...");
        // ����� ����� ��������� winScreenCanvas � �������� mainMenuCanvas (���� � ��� ���� �������� ������ ����)
    }

    // ����� ��� �������� � ���� ������ �������
    private void SelectLevel()
    {
        //Time.timeScale = 1f; // ���������� ���������� �������� �������
        // ������ ��� �������� � ������ ������
        Debug.Log("Opening level select menu...");
        // ����� ��������� winScreenCanvas � �������� levelSelectCanvas
    }
}
