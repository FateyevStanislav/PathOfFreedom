using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource musicSource; // �������� ��� ����

    private void Awake()
    {
        // ���� Instance ��� ���������� � ��� �� ������� ������
        if (Instance != null && Instance != this)
        {
            // ������������� ������ ������ ����� ������������
            if (Instance.musicSource != null && Instance.musicSource.isPlaying)
            {
                Instance.musicSource.Stop();
            }
            Destroy(Instance.gameObject); // ���������� ������ SoundManager
        }

        // ������ ������� ������ ������������ Instance
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}