using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonClickSound; // ���������� AudioSource ��� ����� ������ � Inspector

    private void Start()
    {
        // ���������, ��� � ��� ���� ��������� Button �� ���� �������
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // ��������� ���������� ������� �� ������
        }
    }

    public void OnButtonClick()
    {
        // ������������� ���� ������
        if (buttonClickSound != null)
        {
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlaySound(buttonClickSound); // ������������� ���� ������
            }
            else
            {
                Debug.LogWarning("SoundManager �� ������!");
            }
        }
    }
}
