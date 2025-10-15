using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the Start Menu.
/// </summary>
public class UIStartMenuHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerName;

    [SerializeField]
    private Button startButton;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(PlayerDataManager.Instance.PlayerName))
        {
            playerName.text = PlayerDataManager.Instance.PlayerName;
        }
    }

    /// <summary>
    /// Start the Main Game.
    /// </summary>
    public void StartNew()
    {
        if (string.IsNullOrWhiteSpace(playerName.text))
        {
            Debug.Log("Player's name cannot be empty.");
            playerName.text = "";
            startButton.interactable = false;
            return;
        }
        else
        {
            PlayerDataManager.Instance.PlayerName = playerName.text;
            SceneManager.LoadScene(1);
        }
    }

    /// <summary>
    /// Exit the game.
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
        PlayerDataManager.Instance.SavePlayerData();
    }
}
