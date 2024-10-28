using UnityEngine;

public class JumpSoundManager : MonoBehaviour
{
    public AudioClip jumpSound; // ���� ������
    private AudioSource audioSource; // ��������� AudioSource

    private void Awake()
    {
        // �������� ��������� AudioSource, ���� �� ����
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not attached to " + gameObject.name);
        }
    }

    public void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null && !audioSource.mute)
        {
            audioSource.PlayOneShot(jumpSound); // ������������� ���� ������
        }
        else
        {
            Debug.LogWarning("AudioSource or jumpSound is not assigned.");
        }
    }
}
