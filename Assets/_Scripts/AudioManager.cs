using UnityEngine;
using static PlayerDataManager;

/// <summary>
/// Singleton who manages the game music.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;


    private void Awake()
    {
        // AudioManager is a Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }


}
