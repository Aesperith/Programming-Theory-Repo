using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Spaceship spaceship;

    private InputAction actionMove;
    private InputAction actionShot;

    // Start is called once before the first execution of Update after
    // the MonoBehaviour is created
    private void Start()
    {
        actionMove = InputSystem.actions.FindAction("Move");
        actionShot = InputSystem.actions.FindAction("Shot");
    }

    // Update is called once per frame
    private void Update()
    {
        Shoot();    // ABSTRACTION
    }

    private void FixedUpdate()
    { 
        spaceship.Move(actionMove.ReadValue<Vector2>());    // ABSTRACTION
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
