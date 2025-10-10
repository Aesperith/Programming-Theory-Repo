using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Spaceship spaceship;

    private GameManager gameManager;
    private InputAction actionMove;
    private InputAction actionShot;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        actionMove = InputSystem.actions.FindAction("Move");
        actionShot = InputSystem.actions.FindAction("Shot");
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.isGameActive)
        {
            Shoot();    // ABSTRACTION
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
}
