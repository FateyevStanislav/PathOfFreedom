using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource musicSource; // Добавьте это поле

    private void Awake()
    {
        // Если Instance уже существует и это НЕ текущий объект
        if (Instance != null && Instance != this)
        {
            // Останавливаем старую музыку перед уничтожением
            if (Instance.musicSource != null && Instance.musicSource.isPlaying)
            {
                Instance.musicSource.Stop();
            }
            Destroy(Instance.gameObject); // Уничтожаем старый SoundManager
        }

        // Делаем текущий объект единственным Instance
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}