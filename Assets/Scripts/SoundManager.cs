using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] soundSources; // ������ AudioSource ��� ���� ������
    private bool areSoundsPlaying = true; // ���������� ��� ������������ ��������� ������

    public bool IsMuted { get { return !areSoundsPlaying; } } // ��������� �������� IsMuted

    void Start()
    {
        // ���������, ��� ����� ��������������� ��� ������
        SetSoundsState(true);
    }

    // ����� ��� ������������ ��������� ������
    public void ToggleSounds()
    {
        areSoundsPlaying = !areSoundsPlaying; // ����������� ���������
        SetSoundsState(areSoundsPlaying);
    }

    private void SetSoundsState(bool play)
    {
        foreach (AudioSource audioSource in soundSources)
        {
            audioSource.mute = !play; // �������� ��� ��������� ����
        }
    }
    public void PlaySound(AudioSource audioSource)
    {
        if (!IsMuted && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // ������������� ����, ���� �� �� ������ � �� ��������
        }
    }

}
