using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the player's input to control the player's spaceship.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Spaceship spaceship;

    [SerializeField]
    private TMP_Text playerNameLabel;

    private GameManager gameManager;
    
    private InputAction actionMove;
    private InputAction actionShot;
    private InputAction actionSpecial1;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();

        playerNameLabel.text = PlayerDataManager.Instance.PlayerName;

        actionMove = InputSystem.actions.FindAction("Move");
        actionShot = InputSystem.actions.FindAction("Shot");
        actionSpecial1 = InputSystem.actions.FindAction("Special1");
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.isGameActive)
        {
            Shoot();        // ABSTRACTION
            Activate1();    // ABSTRACTION
        }    
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            spaceship.Move(actionMove.ReadValue<Vector2>());    // ABSTRACTION
        }
    }

    /// <summary>
    /// Player shoot with weapon(s).
    /// </summary>
    private void Shoot()
    {
        if (actionShot.IsPressed())
        {
            spaceship.Shoot();
        }
    }

    /// <summary>
    /// Player active special power 1 (if there are any).
    /// </summary>
    private void Activate1()
    {
        if (actionSpecial1.WasPressedThisFrame())
        {
            spaceship.Activate1();
        }
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}
