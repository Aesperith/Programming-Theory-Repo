using UnityEngine;

/// <summary>
/// The Pause System of the game.
/// </summary>
public class PauseSystem : MonoBehaviour
{
    public static PauseSystem Instance;

    public static bool isPaused;


    private void Awake()
    {
        // PauseSystem is a singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
