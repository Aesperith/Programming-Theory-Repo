using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the UI Main Scene.
/// </summary>
public class UIMainScene : MonoBehaviour
{
    /// <summary>
    /// Return to the Main Menu.
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
