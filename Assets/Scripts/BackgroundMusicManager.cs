using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // ������ �� AudioSource � ������� �������
    private bool isMusicPlaying = true; // ���������� ��� ������������ ��������� ������

    void Start()
    {
        // ���������, ��� ������ ������ ��� ������
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    // ����� ��� ������������ ��������� ������
    public void ToggleMusic()
    {
        if (backgroundMusic != null)
        {
            if (isMusicPlaying)
            {
                backgroundMusic.Pause(); // ���������� ������
            }
            else
            {
                backgroundMusic.UnPause(); // ����������� ������
            }

            isMusicPlaying = !isMusicPlaying; // ����������� ���������
        }
    }
}
